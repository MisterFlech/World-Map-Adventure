using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscenes : MonoBehaviour
{

    public static Cutscenes singleton = null;
    public int numCutscene = 0;
    // Start is called before the first frame update

    public GameObject canvas;
    private bool start = false;

    void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
        }


    }

    void Start()
    {
        canvas.SetActive(false);
        if (numCutscene == 0 && !Interrupteur.getInterrupteurCutscene(numCutscene))
        {
            start = true;
            freezePlayer(true);
            canvas.GetComponent<DialogCutscene>().startDialog(20, 20, numCutscene);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            if (numCutscene == 0 && Interrupteur.getInterrupteurCutscene(numCutscene))
            {
                start = false;
                freezePlayer(false);
            }
        }
    }

    public void freezePlayer(bool val)
    {
        PlayerMovement.freeze = val;
        canvas.SetActive(val);
    }
}
