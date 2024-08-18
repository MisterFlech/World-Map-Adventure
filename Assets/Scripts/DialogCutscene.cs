using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogCutscene : MonoBehaviour
{
    public bool start = false;

    public GameObject _textDialog;
    public GameObject _textName;
    public GameObject _faceset;

    [SerializeField] public Sprite[] facesets;

    private TMP_Text _dialog = null;
    private TMP_Text _name = null;
    private Image _facesetImage = null;

    // Start is called before the first frame update
    void Start()
    {
        initTMP();
    }

    public void initTMP()
    {
        if(_dialog == null)
        {
            _dialog = _textDialog.GetComponent<TMP_Text>();
            _name = _textName.GetComponent<TMP_Text>();
            _facesetImage = _faceset.GetComponent<Image>();
        }
        
    }

    private int firstDialog = 0;
    private int lastDialog = 0;
    private int cutsceneInterrupteur = -1;

    private int idDialog = 0;
    public void startDialog(int _firstDialog, int _lastDialog, int _interruptor)
    {
        initTMP();
        start = true;
        firstDialog = _firstDialog;
        idDialog = _firstDialog;
        cutsceneInterrupteur = _interruptor;

        if (_lastDialog > _firstDialog)
        {
            lastDialog = _lastDialog;
        } else
        {
            lastDialog = _firstDialog;
        }
        setText();
    }

    private void setText()
    {
        _dialog.SetText(TextDatabase.getText(idDialog));
        string characterName = TextDatabase.getName(idDialog);
        _name.SetText(characterName);
        changeFaceset(characterName);
    }

    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            if (Input.GetButtonDown("Jump"))
            {
                
                if (idDialog < lastDialog)
                {
                    idDialog++;
                    setText();
                }
                else
                {
                    Debug.Log("end");
                    start = false;

                    if(cutsceneInterrupteur > -1)
                    {
                        Interrupteur.setInterrupteurCutscene(cutsceneInterrupteur, true);
                    }
                    
                    //freezePlayer(false);
                }
            }
        }
    }

    void changeFaceset(string name)
    {
        
        if (name == "Darturo")
        {
            _facesetImage.sprite = facesets[0];
        }
        else if (name == "Zarwid")
        {
            _facesetImage.sprite = facesets[1];
        }
    }
}
