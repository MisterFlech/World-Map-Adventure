using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveInterruptor : MonoBehaviour
{
    public bool active;
    public GameObject target;
    public int numInterruptor;
    private bool done = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Interrupteur.getInterrupteurCutscene(numInterruptor) && !done)
        {
            target.SetActive(active);
            done = true;
        }
    }
}
