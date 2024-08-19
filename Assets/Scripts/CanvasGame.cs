using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasGame : CutscenesInterface
{
    public static CanvasGame instance = null;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
