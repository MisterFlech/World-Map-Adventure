using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSelectionTitle : MenuSelection
{

    public GameObject panelCredits;
    public GameObject panelControls;

    override protected void actionMenu()
    {
        base.actionMenu();
        if (cursor == 0)
        {
            SceneManager.LoadScene("Introduction");
        }
        else if (cursor == 1)
        {
            gameObject.SetActive(false);
            panelControls.SetActive(true);
        }
        else if (cursor == 2)
        {
            gameObject.SetActive(false);
            panelCredits.SetActive(true);
        }
        else if (cursor == 3)
        {
            Application.Quit();
        }
    }

    override protected void lastPanel()
    {
        AudioManager.instance.StopMusic();
        base.lastPanel();
    }
}
