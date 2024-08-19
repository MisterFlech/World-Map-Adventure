using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscenes : MonoBehaviour
{
    public int numCutscene = 0;
    // Start is called before the first frame update

    private GameObject player = null;

    private bool start = false;
    private int partScene = 0;
    private float pauseTimer = 0f;
    private CanvasGame canvas;

    void Start()
    {
        canvas = CanvasGame.instance;
        if (numCutscene == 1 && !Interrupteur.getInterrupteurCutscene(numCutscene))
        {
            PlayerMovement.freeze = true;
            canvas.startFadeOut(0.75f);
            canvas.startWaiting(0.75f, () => waitCutscene());
            //StartCoroutine(FadeOut(0.75f));
            //StartCoroutine(WaitSecondes(0.75f, () => waitCutscene()));
        } else if (numCutscene == 2 && !Interrupteur.getInterrupteurCutscene(numCutscene)) {
            player = PlayerMovement._playerMovement.gameObject;
            //_windowText.SetActive(false);
        }
    }

    private void waitCutscene()
    {
        if(numCutscene == 1)
        {
            start = true;
            canvas.startDialog(2, numCutscene);
            //_windowText.SetActive(true);
            
        }
    }

    /*
    public void startFadeIn(float duration, float pause, Action endAction, bool isFadeOut)
    {
        StartCoroutine(FadeIn(duration, pause, endAction, isFadeOut));
    }

    public void startFadeOut(float duration)
    {
        StartCoroutine(FadeOut(duration));
    }
    */

    // Update is called once per frame
    void Update()
    {
        if(pauseTimer == 0)
        {
            if (start)
            {
                if (numCutscene == 1 && Interrupteur.getInterrupteurCutscene(numCutscene))
                {
                    start = false;
                    freezePlayerForDialog(false);
                }
                else if (numCutscene == 2 && Interrupteur.getInterrupteurCutscene(numCutscene))
                {
                    start = false;
                    if (partScene == 0)
                    {
                        partScene = 1;
                        Interrupteur.setInterrupteurCutscene(numCutscene, false);
                    }
                    else if (partScene == 1)
                    {
                        partScene = 2;
                        Interrupteur.setInterrupteurCutscene(numCutscene, false);
                    }
                    else if (partScene == 2)
                    {
                        partScene = 3;

                        freezePlayerForDialog(false);
                    }
                }
            }
            else
            {
                if (numCutscene == 2 && !Interrupteur.getInterrupteurCutscene(numCutscene))
                {
                    if (player.transform.position.x == 4.5f && partScene == 0)
                    {
                        start = true;
                        PlayerMovement.freeze = true;
                        canvas.startDialog(3, numCutscene);
                    }
                    else if (partScene == 1)
                    {
                        PlayerMovement._playerMovement.lookTo((int) Direction.Right);
                        canvas.showWindowText(false);
                        pauseTimer = 0.5f;
                    }
                    else if (partScene == 2)
                    {
                        start = true;
                        PlayerMovement._playerMovement.lookTo((int) Direction.Left);
                        canvas.startDialog(5, numCutscene);
                    }
                }
            }
        } else
        {
            pauseTimer = Mathf.Max(pauseTimer - Time.deltaTime, 0);

            if(pauseTimer == 0)
            {
                if (numCutscene == 2 && !Interrupteur.getInterrupteurCutscene(numCutscene) && partScene == 1)
                {
                    start = true;
                    canvas.startDialog(4, numCutscene);
                }
            }
        }
    }

    public void freezePlayerForDialog(bool val)
    {
        PlayerMovement.freeze = val;
        canvas.showWindowText(val);
    }
}
