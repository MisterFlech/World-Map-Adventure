using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextTranslation : MonoBehaviour
{
    public string text_fr = "";
    public string text_en = "";

    // Start is called before the first frame update
    
    void Start()
    {
        translate();
    }

    public void translate()
    {
        string text = "";
        if (GlobalVariables.language == (int)Language.French)
        {
            text = text_fr;
        }
        else if (GlobalVariables.language == (int)Language.English)
        {
            text = text_en;
        }
        GetComponent<TMP_Text>().SetText(text);
    }
}
