using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayOut : Teleportable
{
    public int idVillage = 0;
    public Vector2 fixedCoordonnees = new Vector2(0, 0);
    public AudioClip songVillage;
    public AudioClip songWorldMap;

    public void teleport(int direction)
    {
        GridManager.saveMap();

        Debug.Log(teleportName + " " + teleportName.StartsWith("MM"));
        if (teleportName.StartsWith("MM"))
        {
            song = songVillage;
        }
        else if (teleportName.StartsWith("WM"))
        {
            song = songWorldMap;
        }

        if (idVillage == 0)
        {
            PlayerTeleporter.teleportPosition = fixedCoordonnees;
        } else
        {
            int idWorld = MapName.getId(teleportName);

            try
            {
                Vector2 coordonneesVillage = GridDatabase.getSaveCoordonneesObject(idWorld, idVillage);
                if (direction == (int)Direction.Right)
                {
                    PlayerTeleporter.teleportPosition = new Vector2(coordonneesVillage.x + 1, coordonneesVillage.y);
                }
                else if (direction == (int)Direction.Left)
                {
                    PlayerTeleporter.teleportPosition = new Vector2(coordonneesVillage.x - 1, coordonneesVillage.y);
                }
                else if (direction == (int)Direction.Down)
                {
                    PlayerTeleporter.teleportPosition = new Vector2(coordonneesVillage.x, coordonneesVillage.y - 1);
                }
                else if (direction == (int)Direction.Up)
                {
                    PlayerTeleporter.teleportPosition = new Vector2(coordonneesVillage.x, coordonneesVillage.y + 1);
                }
            } catch(Exception)
            {
                PlayerTeleporter.teleportPosition = fixedCoordonnees;
            }
        }
        teleporting();
    }
}
