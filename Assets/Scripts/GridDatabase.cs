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

    public static bool worldMapExists(int idWorld)
    {
        return worldMapsOriginSave.ContainsKey(idWorld);
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
                    GameObject currentGameObject = _worldMapObjects[i, j];
                    if (currentGameObject != null)
                    {
                        
                        int id = currentGameObject.GetComponent<MapObject>().idObject;
                        //Debug.Log("saving : " + id + " - " + idWorld);
                        if (!worldMapsCurrentSave[idWorld].ContainsKey(id))
                        {
                            Debug.Log("objet manquant");
                        } else
                        {
                            Vector3 position = currentGameObject.transform.position;
                            worldMapsCurrentSave[idWorld][id] = position;
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
                Debug.Log("objet manquant");
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
                Debug.Log("objet manquant");
            }
        }
        else
        {
            Debug.Log("Map inexistante Database");
        }
        throw new Exception();
    }
}
