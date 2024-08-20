using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GridDatabase : MonoBehaviour
{
    public static Dictionary<int, Dictionary<int, Vector3>> worldMapsOriginSave = new Dictionary<int, Dictionary<int, Vector3>>();
    public static Dictionary<int, Dictionary<int, Vector3>> worldMapsCurrentSave = new Dictionary<int, Dictionary<int, Vector3>>();

    public static Dictionary<int, int[,]> worldMapsOriginGrid = new Dictionary<int, int[,]>();
    public static Dictionary<int, int[,]> worldMapsCurrentGrid = new Dictionary<int, int[,]>();

    public static Dictionary<int, Vector3> worldMapsPlayerInitialPosition = new Dictionary<int, Vector3>();

    public static bool worldMapExists(int idWorld)
    {
        return worldMapsOriginSave.ContainsKey(idWorld);
    }

    public static void addPlayerInitialPosition(int idWorld, Vector3 position)
    {
        worldMapsPlayerInitialPosition.Add(idWorld, position);
    }

    public static Vector3 getPlayerInitialPosition(int idWorld)
    {
        return worldMapsPlayerInitialPosition[idWorld];
    }

    public static void addWorldMapOrigin(int idWorld, int[,] _worldMapGrid, GameObject[,] _worldMapObjects)
    {
        if (!worldMapExists(idWorld))
        {
            worldMapsOriginSave[idWorld] = new Dictionary<int, Vector3>();
            worldMapsCurrentSave[idWorld] = new Dictionary<int, Vector3>();

            worldMapsOriginGrid.Add(idWorld, _worldMapGrid.Clone() as int[,]);
            worldMapsCurrentGrid.Add(idWorld, _worldMapGrid.Clone() as int[,]);

            for (int i = 0; i < _worldMapObjects.GetLength(0); i++)
            {
                for (int j = 0; j < _worldMapObjects.GetLength(1); j++)
                {
                    GameObject currentGameObject = _worldMapObjects[i, j];
                    if (currentGameObject != null)
                    {
                        int id = currentGameObject.GetComponent<MapObject>().idObject;
                        Vector3 position = currentGameObject.transform.position;
                        worldMapsOriginSave[idWorld].Add(id, position);
                        worldMapsCurrentSave[idWorld].Add(id, position);
                    }
                }
            }
        } else
        {
            Debug.Log("Map déjà existante Database");
        }
    }

    public static void saveWorldMap(int idWorld, int[,] _worldMapGrid, GameObject[,] _worldMapObjects)
    {

        if (worldMapExists(idWorld))
        {
            worldMapsCurrentGrid[idWorld] = _worldMapGrid.Clone() as int[,];
            for (int i = 0; i < _worldMapObjects.GetLength(0); i++)
            {
                for (int j = 0; j < _worldMapObjects.GetLength(1); j++)
                {
                    //On prend l'objet de la worldmap
                    GameObject currentGameObject = _worldMapObjects[i, j];
                    if (currentGameObject != null)
                    {
                        int id = currentGameObject.GetComponent<MapObject>().idObject;
                        //Si l'objet (par son id) existe dans le dico currentsave, on met à jour sa position
                        if(_worldMapGrid[i,j] == (int)TileType.Hero)
                        {
                            worldMapsCurrentGrid[idWorld][i, j] = (int)TileType.Void;
                        }else
                        {
                            if (!worldMapsCurrentSave[idWorld].ContainsKey(id))
                            {
                                Debug.Log("objet manquant");
                            }
                            else
                            {
                                Vector3 position = currentGameObject.transform.position;
                                worldMapsCurrentSave[idWorld][id] = position;
                            }
                        }
                    }
                }
            }
        }
        else
        {
            Debug.Log("Map inexistante Database");
        }
    }

    public static int[,] getWorldMapOriginGrid(int idWorld)
    {
        if (worldMapsOriginGrid.ContainsKey(idWorld))
        {
            return worldMapsOriginGrid[idWorld].Clone() as int[,];
        }
        return null;
    }

    public static int[,] getWorldMapCurrentGrid(int idWorld)
    {
        if (worldMapsCurrentGrid.ContainsKey(idWorld))
        {
            return worldMapsCurrentGrid[idWorld].Clone() as int[,];
        }
        return null;
    }

    public static Vector3 getSaveCoordonneesObject(int idWorld, int idObject)
    {
        if (worldMapExists(idWorld))
        {
            if (worldMapsCurrentSave[idWorld].ContainsKey(idObject))
            {
                return worldMapsCurrentSave[idWorld][idObject];
            }
            else
            {
                Debug.Log("objet manquant " + idObject);
            }
        }
        else
        {
            Debug.Log("Map inexistante Database");
        }
        throw new Exception();
    }

    public static Vector3 getOriginCoordonneesObject(int idWorld, int idObject)
    {
        if (worldMapExists(idWorld))
        {
            if (worldMapsOriginSave[idWorld].ContainsKey(idObject))
            {
                return worldMapsOriginSave[idWorld][idObject];
            }
            else
            {
                Debug.Log("objet manquant " + idObject);
            }
        }
        else
        {
            Debug.Log("Map inexistante Database");
        }
        throw new Exception();
    }
}
