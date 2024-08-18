using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interrupteur : MonoBehaviour
{

    public static Interrupteur singleton = null;
    private static bool[] cutscene_interrupteurs = new bool[10];

    public static bool getInterrupteurCutscene(int num_cutscene)
    {
        if(num_cutscene >= 0 && num_cutscene < cutscene_interrupteurs.Length)
        {
            return cutscene_interrupteurs[num_cutscene];
        }
        return false;
    }

    public static void setInterrupteurCutscene(int num_cutscene, bool val)
    {
        if (num_cutscene >= 0 && num_cutscene < cutscene_interrupteurs.Length)
        {
            Debug.Log("interrupteur activé : " + num_cutscene);
            cutscene_interrupteurs[num_cutscene] = val;
        }
    }

    void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
            for(int i = 0; i < cutscene_interrupteurs.Length; i++)
            {
                cutscene_interrupteurs[i] = false;
            }
        }
    }

}
