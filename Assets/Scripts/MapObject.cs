using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapObject : MonoBehaviour
{
    protected Vector2 nextPosition = new Vector2(0,0);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void reset(Vector3 newPosition)
    {
        transform.position = newPosition;
        nextPosition.x = newPosition.x;
        nextPosition.y = newPosition.y;
    }
}
