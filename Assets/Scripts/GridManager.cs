using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GridManager : MonoBehaviour
{
    public bool saveGrid = false;
    public bool resetGrid = false;

    private static bool saveGridPermanent;
    private static bool resetGridPermanent;

    private static int width = 10;
    private static int height = 10;
    private static Vector2Int origin;
    private static GridManager singleton;

    private static int[,] worldMapGrid;
    private static GameObject[,] worldMapObjects;
    private static int idWorld;

    //private Stack<GameObject[,]> saveWorldMapGrid = new Stack<GameObject[,]>();
    //private Stack<GameObject[,]> saveWorldMapObjects = new Stack<GameObject[,]>();

    //private static int[,] worldMapGridSave;
    //private static GameObject[,] worldMapObjectsSave;

    public int width_map = 0;
    public int height_map = 0;
    public GameObject originPoint;

    void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
        }

        GridManager.width = width_map;
        GridManager.height = height_map;
        GridManager.origin = new Vector2Int((int)originPoint.transform.position.x, (int)originPoint.transform.position.y);

        GridManager.worldMapGrid = new int[GridManager.height, GridManager.width];
        GridManager.worldMapObjects = new GameObject[GridManager.height, GridManager.width];

        //Initialisation de la map locale
        for (int i = 0; i < GridManager.height; i++)
        {
            for (int j = 0; j < GridManager.width; j++)
            {
                GridManager.worldMapGrid[i, j] = (int)TileType.Void;
                GridManager.worldMapObjects[i, j] = null;
            }
        }

        saveGridPermanent = saveGrid;
        resetGridPermanent = resetGrid;
        idWorld = SceneManager.GetActiveScene().buildIndex;
    }

    void Start()
    {
        //Chaque objet se charge automatiquement dans la carte static;
        //GridManager.debugMap(); //Map matrice
                                //GridManager.saveMap(); //On sauvegarde la map actuelle.

        if (saveGridPermanent)
        {
            if (GridDatabase.worldMapExists(idWorld))
            {
                //Si la map existe, on la charge
                loadMap();
            }
            else
            {
                //Si la map n'existe pas dans la database, on l'ajoute
                GridDatabase.addWorldMapOrigin(idWorld, worldMapGrid, worldMapObjects);
            }
        }
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
                if (resetGridPermanent && saveGridPermanent)
                {
                    GridManager.resetMap();
                }
            }
        }
    }

    public static void saveMap()
    {
        if (saveGridPermanent)
        {
            Debug.Log("save : " + idWorld);
            GridDatabase.saveWorldMap(idWorld, GridManager.worldMapGrid, GridManager.worldMapObjects);
        }
    }

    public static void resetMap()
    {
        GameObject[,] worldMapObjectsTmp = new GameObject[GridManager.height, GridManager.width];
        GridManager.worldMapGrid = GridDatabase.getWorldMapOriginGrid(idWorld);
        for (int i = 0; i < GridManager.height; i++)
        {
            for (int j = 0; j < GridManager.width; j++)
            {
                GameObject _gameObject = GridManager.getTileObject(j, i);
                int _tileType = GridManager.getTileGrid(j, i);
                if (_gameObject != null)
                {
                    if (_tileType != (int)TileType.Wall)
                    {
                        int id = _gameObject.GetComponent<MapObject>().idObject;
                        _gameObject.GetComponent<MapObject>().reset(idWorld);
                        worldMapObjectsTmp[i, j] = _gameObject;
                    }
                }
            }
        }
        GridManager.worldMapObjects = worldMapObjectsTmp;
    }

    public static void loadMap()
    {
        GridManager.worldMapGrid = GridDatabase.getWorldMapCurrentGrid(idWorld);
        for (int i = 0; i < GridManager.height; i++)
        {
            for (int j = 0; j < GridManager.width; j++)
            {
                GameObject _gameObject = GridManager.getTileObject(j, i);
                int _tileType = GridManager.getTileGrid(j, i);
                if (_gameObject != null)
                {
                    if (_tileType != (int)TileType.Wall)
                    {
                        int id = _gameObject.GetComponent<MapObject>().idObject;
                        _gameObject.GetComponent<MapObject>().load(idWorld);
                    }
                }
            }
        }
    }

    private static void debugMap()
    {
        string txtMap = "";
        string txtMapObject = "";

        for (int i = 0; i < worldMapGrid.GetLength(0); i++)
        {
            for (int j = 0; j < worldMapGrid.GetLength(1); j++)
            {
                txtMap += " " + worldMapGrid[i, j];
                if (worldMapObjects[i, j] != null)
                {
                    txtMapObject += "X";
                }
                else
                {
                    txtMapObject += "_";
                }
            }
            txtMap += "\n";
            txtMapObject += "\n";
        }
        Debug.Log(txtMap);
        Debug.Log(txtMapObject);
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
        if(_tileType == TileType.Pushable)
        {
            if (GridDatabase.worldMapExists(idWorld))
            {
                int idObject = gameObject.GetComponent<MapObject>().idObject;
                position = GridDatabase.getSaveCoordonneesObject(idWorld, idObject);
                gameObject.transform.position = position;
            }
        }
        Vector2Int coordonnees = GridManager.convertCoordonnees(position);
        //Debug.Log(coordonnees + " " + gameObject);
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

    public static bool isAtEdgeUp(Vector2Int coordonnees)
    {
        return (coordonnees.y == 0);
    }

    public static bool isAtEdgeDown(Vector2Int coordonnees)
    {
        return (coordonnees.y >= height - 1);
    }

    public static int checkDirection(int direction, Vector3 position)
    {
        if (Directions.isLeft(direction))
        {
            return checkLeft(position);
        }
        else if (Directions.isRight(direction))
        {
            return checkRight(position);
        }
        else if (Directions.isUp(direction))
        {
            return checkUp(position);
        }
        else if (Directions.isDown(direction))
        {
            return checkDown(position);
        }
        return -1;
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

    public static int checkUp(Vector3 position)
    {
        Vector2Int coordonnees = GridManager.convertCoordonnees(position);
        if (GridManager.isAtEdgeUp(coordonnees))
        {
            return -1;
        }
        return GridManager.getTileGrid(coordonnees.x, coordonnees.y - 1);
    }

    public static int checkDown(Vector3 position)
    {
        Vector2Int coordonnees = GridManager.convertCoordonnees(position);
        if (GridManager.isAtEdgeDown(coordonnees))
        {
            return -1;
        }
        return GridManager.getTileGrid(coordonnees.x, coordonnees.y + 1);
    }

    public static GameObject getObjectDirection(int direction, Vector3 position)
    {
        if (Directions.isLeft(direction))
        {
            return getObjectLeft(position);
        } else if (Directions.isRight(direction))
        {
            return getObjectRight(position);
        }
        else if(Directions.isUp(direction))
        {
            return getObjectUp(position);
        } else if (Directions.isDown(direction))
        {
            return getObjectDown(position);
        }
        return null;
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

    public static GameObject getObjectUp(Vector3 position)
    {
        Vector2Int coordonnees = GridManager.convertCoordonnees(position);
        if (GridManager.isAtEdgeUp(coordonnees))
        {
            return null;
        }
        return GridManager.getTileObject(coordonnees.x, coordonnees.y - 1);
    }

    public static GameObject getObjectDown(Vector3 position)
    {
        Vector2Int coordonnees = GridManager.convertCoordonnees(position);
        if (GridManager.isAtEdgeDown(coordonnees))
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
        if (GridManager.isAtEdgeUp(coordonnees))
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
        if (GridManager.isAtEdgeDown(coordonnees))
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
