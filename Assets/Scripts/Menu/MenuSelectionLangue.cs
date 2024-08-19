using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuSelectionLangue : MenuSelection
{
    public GameObject title;

    public GameObject nextPanel;

    public string text_fr = "";
    public string text_en = "";

    public AudioClip song;

    override protected void actionMenu()
    {
        base.actionMenu();
        if (cursor == 0)
        {
            GlobalVariables.language = (int)Language.English;
        }
        else if (cursor == 1)
        {
            GlobalVariables.language = (int)Language.French;
        }

        gameObject.SetActive(false);
        nextPanel.SetActive(true);
        GameObject[] translateTexts = GameObject.FindGameObjectsWithTag("Translate");
        foreach(GameObject text in translateTexts)
        {
            text.GetComponent<TextTranslation>().translate();
        }
        AudioManager.instance.PlayMusic(song, 0.5f);
    }

    override protected void moveCursor()
    {
        string text = "";
        if (cursor == 0)
        {
            text = text_en;
        }
        else if (cursor == 1)
        {
            text = text_fr;
        }

        title.GetComponent<TMP_Text>().SetText(text);
        base.moveCursor();
    }
}
