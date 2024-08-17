using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MapObject
{
    // Start is called before the first frame update
    void Start()
    {
        GridManager.addElementInGrid(this.gameObject, TileType.Wall, transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
