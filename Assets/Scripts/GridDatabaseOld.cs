using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridDatabaseOld : MonoBehaviour
{
    public static Dictionary<int, int[,]> worldMapsOriginGrid = new Dictionary<int, int[,]>();
    public static Dictionary<int, GameObject[,]> worldMapsOriginObjects = new Dictionary<int, GameObject[,]>();

    public static Dictionary<int, int[,]> worldMapsGrid = new Dictionary<int, int[,]>();
    public static Dictionary<int, GameObject[,]> worldMapsObjects = new Dictionary<int, GameObject[,]>();

    public static bool worldMapExists(int idWorld)
    {
        return worldMapsOriginGrid.ContainsKey(idWorld);
    }

    public static void addWorldMapOrigin(int idWorld, int[,] _worldMapGrid, GameObject[,] _worldMapObjects)
    {
        worldMapsOriginGrid.Add(idWorld, _worldMapGrid);
        worldMapsOriginObjects.Add(idWorld, _worldMapObjects);

        addWorldMap(idWorld, _worldMapGrid, _worldMapObjects);
    }

    public static int[,] getWorldMapOriginGrid(int idMap)
    {
        if (worldMapsOriginGrid.ContainsKey(idMap))
        {
            return worldMapsOriginGrid[idMap];
        }
        return null;
    }

    public static GameObject[,] getWorldMapOriginObjects(int idMap)
    {
        if (worldMapsOriginObjects.ContainsKey(idMap))
        {
            return worldMapsOriginObjects[idMap];
        }
        return null;
    }

    public static void addWorldMap(int idWorld, int[,] _worldMapGrid, GameObject[,] _worldMapObjects)
    {
        worldMapsGrid.Add(idWorld, _worldMapGrid);
        worldMapsObjects.Add(idWorld, _worldMapObjects);
    }

    public static void updateWorldMap(int idWorld, int[,] _worldMapGrid, GameObject[,] _worldMapObjects)
    {
        if (worldMapsGrid.ContainsKey(idWorld) && worldMapsObjects.ContainsKey(idWorld))
        {
            worldMapsGrid[idWorld] = _worldMapGrid.Clone() as int[,];
            worldMapsObjects[idWorld] = _worldMapObjects.Clone() as GameObject[,];
        }
        else
        {
            Debug.Log("map doesn't exist in database");
        }
    }

    public static int[,] getWorldMapGrid(int idWorld)
    {
        if (worldMapsGrid.ContainsKey(idWorld))
        {
            return worldMapsGrid[idWorld];
        }
        Debug.Log("map doesn't exist in database");
        return null;
    }

    public static GameObject[,] getWorldMapObjects(int idWorld)
    {
        if (worldMapsObjects.ContainsKey(idWorld))
        {
            return worldMapsObjects[idWorld];
        }
        Debug.Log("map doesn't exist in database");
        return null;
    }

    public static void debugMap(int idWorld)
    {
        string txtMap = "";

        idWorld = 3;

        if (worldMapsGrid.ContainsKey(idWorld))
        {
            for (int i = 0; i < worldMapsGrid[idWorld].GetLength(0); i++)
            {
                for (int j = 0; j < worldMapsGrid[idWorld].GetLength(1); j++)
                {
                    txtMap += " " + worldMapsGrid[idWorld][i, j];
                }
                txtMap += "\n";
            }
            Debug.Log(txtMap);
        }

        /*
        if (worldMapsGrid.ContainsKey(idWorld))
        {
            for (int i = 0; i < worldMapsObjects[idWorld].GetLength(0); i++)
            {
                for (int j = 0; j < worldMapsObjects[idWorld].GetLength(1); j++)
                {
                    if(worldMapsObjects[idWorld][i, j] != null)
                    {
                        //Debug.Log(worldMapsObjects[idWorld][i, j]);
                        txtMap += "X";
                    } else
                    {
                        txtMap += "_";
                    }
                    
                }
                txtMap += "\n";
            }
            Debug.Log(txtMap);
        }
        */
    }
}
