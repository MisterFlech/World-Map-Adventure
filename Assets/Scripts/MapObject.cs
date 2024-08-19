using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MapObject : MonoBehaviour
{
    public int idObject;
    protected Vector2 nextPosition = new Vector2(0,0);

    public void reset(int idWorld)
    {
        try
        {
            Vector3 newPosition = GridDatabase.getOriginCoordonneesObject(idWorld, idObject);
            transform.position = newPosition;
            nextPosition.x = newPosition.x;
            nextPosition.y = newPosition.y;
            //Debug.Log("reset " + this.gameObject);
        }
        catch (Exception)
        {
            Debug.Log("coordonnées erreur");
        }
    }

    public void load(int idWorld)
    {
        try
        {
            Vector3 newPosition = GridDatabase.getSaveCoordonneesObject(idWorld, idObject);
            transform.position = newPosition;
            nextPosition.x = newPosition.x;
            nextPosition.y = newPosition.y;
            Debug.Log("reset " + this.gameObject);
        }
        catch (Exception)
        {
            Debug.Log("coordonnées erreur");
        }
    }
}
