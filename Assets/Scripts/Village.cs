using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Village : MonoBehaviour
{
    public string teleport;

    public bool doorLeft = false;
    public bool doorRight = false;
    public bool doorUp = false;
    public bool doorDown = false;

    public Vector2 teleportCoordonneesDoorLeft = new Vector2(0,0);
    public Vector2 teleportCoordonneesDoorRight = new Vector2(0, 0);
    public Vector2 teleportCoordonneesDoorUp = new Vector2(0, 0);
    public Vector2 teleportCoordonneesDoorDown = new Vector2(0, 0);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
