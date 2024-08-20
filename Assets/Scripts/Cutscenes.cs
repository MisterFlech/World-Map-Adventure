using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscenes : MonoBehaviour
{
    public int numCutscene = 0;
    // Start is called before the first frame update

    public GameObject wizard = null;

    private GameObject player = null;

    private bool start = false;
    private int partScene = 0;
    private float pauseTimer = 0f;
    private CanvasGame canvas;

    void Start()
    {
        if(wizard != null)
        {
            wizard.SetActive(false);
        }
        canvas = CanvasGame.instance;
        if (!Interrupteur.getInterrupteurCutscene(numCutscene))
        {
            if (numCutscene == 1)
            {
                PlayerMovement.freeze = true;
                canvas.startFadeOut(0.75f);
                canvas.startWaiting(0.75f, () => waitCutscene());
                //StartCoroutine(FadeOut(0.75f));
                //StartCoroutine(WaitSecondes(0.75f, () => waitCutscene()));
            }
            else
            {
                player = PlayerMovement._playerMovement.gameObject;
                //_windowText.SetActive(false);
            }
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
                if (Interrupteur.getInterrupteurCutscene(numCutscene))
                {
                    start = false;
                    if (numCutscene == 1)
                    {
                        freezePlayerForDialog(false);
                    }
                    else if (numCutscene == 2)
                    {
                        if (partScene == 0)
                        {
                            partScene = 3;
                            Interrupteur.setInterrupteurCutscene(numCutscene, false);
                        }
                        else if (partScene == 3)
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
                            partScene = -1;
                            wizardAppears(false);
                            freezePlayerForDialog(false);
                        }
                    }
                    else if (numCutscene == 3)
                    {
                        if (partScene == 0)
                        {
                            partScene = -1;
                            wizardAppears(false);
                            freezePlayerForDialog(false);
                        }
                    }
                    else if (numCutscene == 4)
                    {
                        if (partScene == 0)
                        {
                            partScene = 1;
                            Interrupteur.setInterrupteurCutscene(numCutscene, false);
                        }
                        else if (partScene == 1)
                        {
                            partScene = -1;
                            wizardAppears(false);
                            freezePlayerForDialog(false);
                        }
                    }
                }
                
            }
            else
            {
                if (!Interrupteur.getInterrupteurCutscene(numCutscene))
                {
                    if (numCutscene == 2)
                    {
                        if (player.transform.position.x == 4.5f && partScene == 0)
                        {
                            start = true;
                            PlayerMovement.freeze = true;

                            canvas.startDialog(3, numCutscene);
                        }
                        else if (partScene == 3)
                        {
                            wizardAppears(true);
                            canvas.startDialog(8, numCutscene);
                            start = true;
                        }
                        else if (partScene == 1)
                        {
                            PlayerMovement._playerMovement.lookTo((int)Direction.Right);
                            canvas.showWindowText(false);
                            pauseTimer = 0.5f;
                        }
                        else if (partScene == 2)
                        {
                            start = true;
                            PlayerMovement._playerMovement.lookTo((int)Direction.Left);
                            canvas.startDialog(5, numCutscene);
                        }
                    }
                    else if (numCutscene == 3)
                    {
                        if (player.transform.position.x == -5.5f && partScene == 0)
                        {
                            start = true;
                            PlayerMovement.freeze = true;
                            wizardAppears(true);

                            canvas.startDialog(9, numCutscene);
                        }
                    }
                    else if (numCutscene == 4)
                    {
                        if (player.transform.position.x == 6.5f && player.transform.position.y == 0.5f && partScene == 0)
                        {
                            start = true;
                            PlayerMovement.freeze = true;

                            PlayerMovement._playerMovement.lookAt();
                            canvas.startDialog(10, numCutscene);
                        } else if (partScene == 1)
                        {
                            start = true;
                            PlayerMovement._playerMovement.lookTo((int)Direction.Right);
                            wizardAppears(true);

                            canvas.startDialog(11, numCutscene);
                        }
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

    private void wizardAppears(bool appears)
    {
        canvas.startFlashOut(0.5f);
        if (appears)
        {
            AudioManager.instance.PlaySFXName("wizard_in");
        } else
        {
            AudioManager.instance.PlaySFXName("wizard_out");
        }
        wizard.SetActive(appears);
    }

    public void freezePlayerForDialog(bool val)
    {
        PlayerMovement.freeze = val;
        canvas.showWindowText(val);
    }
}
