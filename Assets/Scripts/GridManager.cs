using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    private static int width = 10;
    private static int height = 10;
    private static Vector2Int origin;
    private static GridManager singleton;

    private static int[,] worldMapGrid;
    private static GameObject[,] worldMapObjects;

    //private Stack<GameObject[,]> saveWorldMapGrid = new Stack<GameObject[,]>();
    //private Stack<GameObject[,]> saveWorldMapObjects = new Stack<GameObject[,]>();

    private static int[,] worldMapGridSave;
    private static GameObject[,] worldMapObjectsSave;

    public int width_map = 0;
    public int height_map = 0;
    public GameObject originPoint;

    void Awake()
    {
        if (singleton == null)
        {
            singleton = this;

            GridManager.width = width_map;
            GridManager.height = height_map;
            GridManager.origin = new Vector2Int((int) originPoint.transform.position.x, (int) originPoint.transform.position.y);

            //On définit l'origine haut gauche de la grille comme le nouveau 0,0
            //GridManager.origin = new Vector2Int(width/2 * -1, height/2);
            GridManager.worldMapGrid = new int[GridManager.height, GridManager.width];
            GridManager.worldMapObjects = new GameObject[GridManager.height, GridManager.width];

            //saveWorldMap.Push(worldMapGrid.Clone());

            for (int i = 0; i < GridManager.height; i++){
                for (int j = 0; j < GridManager.width; j++){
                    GridManager.worldMapGrid[i,j] = (int) TileType.Void;
                    GridManager.worldMapObjects[i,j] = null;
                }
            }
        }
    }

    void Start()
    {
        GridManager.debugMap();
        GridManager.saveMap();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            GridManager.debugMap();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (!PlayerMovement._playerMovement.isMoving)
            {
                GridManager.loadMap();
            }
        }
    }

    private static void saveMap()
    {
        GridManager.worldMapGridSave = GridManager.worldMapGrid.Clone() as int[,];
        GridManager.worldMapObjectsSave = GridManager.worldMapObjects.Clone() as GameObject[,];
    }

    private static void loadMap()
    {
        GridManager.worldMapGrid = GridManager.worldMapGridSave.Clone() as int[,];
        GridManager.worldMapObjects = GridManager.worldMapObjectsSave.Clone() as GameObject[,];
        for (int i = 0; i < GridManager.height; i++)
        {
            for (int j = 0; j < GridManager.width; j++)
            {
                GameObject _gameObject = GridManager.getTileObject(j, i);
                int _tileType = GridManager.getTileGrid(j, i);
                if (_gameObject != null)
                {
                    if(_tileType != (int)TileType.Wall)
                    {
                        Vector3 newPosition = new Vector3(origin.x + j + 0.5f, origin.y - i + 0.5f, _gameObject.transform.position.z);
                        Debug.Log("load 1 " + _gameObject.transform.position);
                        Debug.Log("load 2 " + newPosition);
                        _gameObject.GetComponent<MapObject>().reset(newPosition);
                    }
                }
            }
        }
    }

    private static void debugMap()
    {
        string txtMap = "";
        Debug.Log(height + "/" + width);
        for (int i = 0; i < GridManager.height; i++)
        {
            for (int j = 0; j < GridManager.width; j++)
            {
                txtMap += " " + GridManager.worldMapGrid[i, j];
            }
            txtMap += "\n";
        }
        Debug.Log(txtMap);
    }

    //récupère la valeur d'une case
    public static int getTileGrid(int x, int y)
    {
        return GridManager.worldMapGrid[y, x];
    }

    public static GameObject getTileObject(int x, int y)
    {
        return GridManager.worldMapObjects[y, x];
    }

    public static void setTileObject(int x, int y, int tileType, GameObject gameObject)
    {
        GridManager.worldMapGrid[y, x] = tileType;
        GridManager.worldMapObjects[y, x] = gameObject;
    }

    //Transforme une position d'objet en coordonnée 
    public static Vector2Int convertCoordonnees(Vector3 position)
    {
        int _x = (int) Mathf.Floor(position.x);
        int _y = (int)Mathf.Floor(position.y);
        return new Vector2Int(_x - GridManager.origin.x, GridManager.origin.y - _y);
    }

    //Ajoute un élément sur la grille
    public static void addElementInGrid(GameObject gameObject, TileType _tileType, Vector3 position)
    {
        Vector2Int coordonnees = GridManager.convertCoordonnees(position);
        Debug.Log("position:" + position);
        Debug.Log("x:"+coordonnees.x+"/y:"+coordonnees.y);
        GridManager.worldMapGrid[coordonnees.y, coordonnees.x] = (int) _tileType;
        GridManager.worldMapObjects[coordonnees.y, coordonnees.x] = gameObject;
    }


    //Regarde si la case est sur un bord
    public static bool isAtEdgeLeft(Vector2Int coordonnees)
    {
        return (coordonnees.x == 0);
    }

    public static bool isAtEdgeRight(Vector2Int coordonnees)
    {
        return (coordonnees.x >= width - 1);
    }

    public static bool isAtEdgeTop(Vector2Int coordonnees)
    {
        return (coordonnees.y == 0);
    }

    public static bool isAtEdgeBottom(Vector2Int coordonnees)
    {
        return (coordonnees.y >= height - 1);
    }

    //Regarde l'élément à côté d'une case.
    public static int checkLeft(Vector3 position)
    {
        Vector2Int coordonnees = GridManager.convertCoordonnees(position);
        if (GridManager.isAtEdgeLeft(coordonnees))
        {
            return -1;
        }
        return GridManager.getTileGrid(coordonnees.x-1, coordonnees.y);
    }

    public static int checkRight(Vector3 position)
    {
        Vector2Int coordonnees = GridManager.convertCoordonnees(position);
        if (GridManager.isAtEdgeRight(coordonnees))
        {
            return -1;
        }
        return GridManager.getTileGrid(coordonnees.x + 1, coordonnees.y);
    }

    public static int checkTop(Vector3 position)
    {
        Vector2Int coordonnees = GridManager.convertCoordonnees(position);
        if (GridManager.isAtEdgeTop(coordonnees))
        {
            return -1;
        }
        return GridManager.getTileGrid(coordonnees.x, coordonnees.y - 1);
    }

    public static int checkBottom(Vector3 position)
    {
        Vector2Int coordonnees = GridManager.convertCoordonnees(position);
        if (GridManager.isAtEdgeBottom(coordonnees))
        {
            return -1;
        }
        return GridManager.getTileGrid(coordonnees.x, coordonnees.y + 1);
    }

    public static GameObject getObjectLeft(Vector3 position)
    {
        Vector2Int coordonnees = GridManager.convertCoordonnees(position);
        if (GridManager.isAtEdgeLeft(coordonnees))
        {
            return null;
        }
        return GridManager.getTileObject(coordonnees.x - 1, coordonnees.y);
    }

    public static GameObject getObjectRight(Vector3 position)
    {
        Vector2Int coordonnees = GridManager.convertCoordonnees(position);
        if (GridManager.isAtEdgeRight(coordonnees))
        {
            return null;
        }
        return GridManager.getTileObject(coordonnees.x + 1, coordonnees.y);
    }

    public static GameObject getObjectTop(Vector3 position)
    {
        Vector2Int coordonnees = GridManager.convertCoordonnees(position);
        if (GridManager.isAtEdgeTop(coordonnees))
        {
            return null;
        }
        return GridManager.getTileObject(coordonnees.x, coordonnees.y - 1);
    }

    public static GameObject getObjectBottom(Vector3 position)
    {
        Vector2Int coordonnees = GridManager.convertCoordonnees(position);
        if (GridManager.isAtEdgeBottom(coordonnees))
        {
            return null;
        }
        return GridManager.getTileObject(coordonnees.x, coordonnees.y + 1);
    }

    //Bouger un objet sur la grille
    public static void MoveLeft(Vector3 position)
    {
        Vector2Int coordonnees = GridManager.convertCoordonnees(position);
        if (GridManager.isAtEdgeLeft(coordonnees))
        {
            Debug.Log("erreur");
        } else { 
            int _tileType = GridManager.getTileGrid(coordonnees.x, coordonnees.y);
            GameObject _gameObject = GridManager.getTileObject(coordonnees.x, coordonnees.y);
            GridManager.setTileObject(coordonnees.x, coordonnees.y, (int) TileType.Void, null) ;
            GridManager.setTileObject(coordonnees.x - 1, coordonnees.y, _tileType, _gameObject);
        }
    }

    public static void MoveRight(Vector3 position)
    {
        Vector2Int coordonnees = GridManager.convertCoordonnees(position);
        if (GridManager.isAtEdgeRight(coordonnees))
        {
            Debug.Log("erreur");
        }
        else
        {
            int _tileType = GridManager.getTileGrid(coordonnees.x, coordonnees.y);
            GameObject _gameObject = GridManager.getTileObject(coordonnees.x, coordonnees.y);
            GridManager.setTileObject(coordonnees.x, coordonnees.y, (int) TileType.Void, null);
            GridManager.setTileObject(coordonnees.x + 1, coordonnees.y, _tileType, _gameObject);
        }
    }

    public static void MoveUp(Vector3 position)
    {
        Vector2Int coordonnees = GridManager.convertCoordonnees(position);
        if (GridManager.isAtEdgeTop(coordonnees))
        {
            Debug.Log("erreur");
        }
        else
        {
            int _tileType = GridManager.getTileGrid(coordonnees.x, coordonnees.y);
            GameObject _gameObject = GridManager.getTileObject(coordonnees.x, coordonnees.y);
            GridManager.setTileObject(coordonnees.x, coordonnees.y, (int) TileType.Void, null);
            GridManager.setTileObject(coordonnees.x, coordonnees.y - 1, _tileType, _gameObject);
        }
    }

    public static void MoveDown(Vector3 position)
    {
        Vector2Int coordonnees = GridManager.convertCoordonnees(position);
        if (GridManager.isAtEdgeBottom(coordonnees))
        {
            Debug.Log("erreur");
        }
        else
        {
            int _tileType = GridManager.getTileGrid(coordonnees.x, coordonnees.y);
            GameObject _gameObject = GridManager.getTileObject(coordonnees.x, coordonnees.y);
            GridManager.setTileObject(coordonnees.x, coordonnees.y, (int) TileType.Void, null);
            GridManager.setTileObject(coordonnees.x, coordonnees.y + 1, _tileType, _gameObject);
        }
    }

}
