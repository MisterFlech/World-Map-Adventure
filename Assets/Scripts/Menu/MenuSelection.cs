using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSelection : MonoBehaviour
{
    protected int cursor = 0;
    private float delay = 0.15f;
    private float delayTimer = 0f;
    public GameObject[] menuElements;
    public bool isActive = false;
    public GameObject _lastPanel = null;

    public SoundEffect soundEffect = new SoundEffect();
    [System.Serializable]
    public class SoundEffect
    {
        [SerializeField] public AudioClip moveSound;
        [SerializeField] public AudioClip validSound;
        [SerializeField] public AudioClip cancelSound;
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        //On masque tous les autres sauf le premier
        for(int i = 1; i < menuElements.Length; i++)
        {
            menuElements[i].SetActive(false);
        }
        if (!isActive)
        {
            gameObject.SetActive(false);
        }
    }

    private void updateCursor()
    {
        for (int i = 0; i < menuElements.Length; i++)
        {
            menuElements[i].SetActive(i == cursor);
        }
    }

    protected virtual void moveCursor()
    {
        AudioManager.instance.PlaySFX(soundEffect.moveSound);
        delayTimer = delay;
        updateCursor();
    }

    private void cursorUp()
    {
        if(cursor > 0)
        {
            cursor--;
            moveCursor();
        }

        
    }

    private void cursorDown()
    {
        if (cursor < menuElements.Length - 1)
        {
            cursor++;
            moveCursor();
        }
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if(delayTimer > 0)
        {
            delayTimer = Mathf.Max(delayTimer - Time.deltaTime, 0);
        } else
        {
            float movY = Input.GetAxisRaw("Vertical");
            if (movY > 0) // haut
            {
                cursorUp();
            }
            else if (movY < 0) // bas
            {
                cursorDown();
            } else if (Input.GetButtonDown("Jump"))
            {
                actionMenu();
            }
            else if (Input.GetButtonDown("Cancel"))
            {
                if (_lastPanel != null)
                {
                    lastPanel();
                }
            }
        }
    }

    protected virtual void lastPanel()
    {
        AudioManager.instance.PlaySFX(soundEffect.cancelSound);
        _lastPanel.SetActive(true);
        gameObject.SetActive(false);
    }

    protected virtual void actionMenu()
    {
        AudioManager.instance.PlaySFX(soundEffect.validSound);
    }
}
