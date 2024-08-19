using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSelectionRead : MenuSelection
{

    override protected void Update()
    {
        if (Input.GetButtonDown("Jump") || Input.GetButtonDown("Cancel"))
        {
            lastPanel();
        }
    }
}
