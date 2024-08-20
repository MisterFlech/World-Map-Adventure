using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneForteresse : MonoBehaviour
{
    public GameObject village;
    public int numInterruptor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!Interrupteur.getInterrupteurCutscene(numInterruptor))
        {
            Debug.Log(village.transform.position);
            if (village.transform.position.x == -3.5 && village.transform.position.y == 2.5)
            {
                Interrupteur.setInterrupteurCutscene(numInterruptor, true);
            }
        }
       
    }
}
