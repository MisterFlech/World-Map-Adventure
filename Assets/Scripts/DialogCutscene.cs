using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogCutscene : MonoBehaviour
{
    private bool start = false;

    public GameObject _textDialog;
    public GameObject _textName;
    public GameObject _faceset;

    [SerializeField] public Sprite[] facesets;

    private TMP_Text _dialog = null;
    private TMP_Text _name = null;
    private Image _facesetImage = null;

    public bool isTalking()
    {
        return start;
    }

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
    public void startDialog(int _idDialog, int _interruptor)
    {
        int _firstDialog = TextDatabase.getDialog(_idDialog).x;
        int _lastDialog = TextDatabase.getDialog(_idDialog).y;
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
                    //Debug.Log("end");
                    start = false;

                    if(cutsceneInterrupteur > -1)
                    {
                        Interrupteur.setInterrupteurCutscene(cutsceneInterrupteur, true);
                    }
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
        else if (name == "Bob" || name == "Nino" || name == "Kirio")
        {
            _facesetImage.sprite = facesets[2];
        }
        else if (name == "Zoé" || name == "Marisa" || name == "Vanessa" || name == "Thémys")
        {
            _facesetImage.sprite = facesets[3];
        }
        else if (name == "Arguadin")
        {
            _facesetImage.sprite = facesets[4];
        }
        else if (name == "Ylda")
        {
            _facesetImage.sprite = facesets[5];
        }
    }
}
