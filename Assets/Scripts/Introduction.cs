using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Introduction : MonoBehaviour
{

    public GameObject imageIntroduction;
    public GameObject textIntroduction;
    public GameObject _windowText;

    private Image _imageLinker;
    private TMP_Text _text;

    private int idImageIntroduction = 0;
    private int idText = 0;
    [SerializeField] public Sprite[] imagesIntroduction;

    void Start()
    {
        _windowText.SetActive(false);
        _imageLinker = imageIntroduction.GetComponent<Image>();
        _text = textIntroduction.GetComponent<TMP_Text>();
        _text.SetText(TextDatabase.getText(0));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            idText++;
            if(idText < 7)
            {
                _text.SetText(TextDatabase.getText(idText));
            } else if(idText >= 20)
            {
                SceneManager.LoadScene("MM_HeroVillage");
            }
            if (idText == 1)
            {
                nextImage();
            }
            else if (idText == 3)
            {
                nextImage();
            }
            else if (idText == 5)
            {
                nextImage();
            }
            else if (idText == 6)
            {
                nextImage();
            }
            else if (idText == 7)
            {
                _text.SetText("");
                _windowText.SetActive(true);
                _windowText.GetComponent<DialogCutscene>().startDialog(7,15,1);
                nextImage();
            }
            else if (idText == 15)
            {
                _windowText.SetActive(false);
                _text.SetText(TextDatabase.getText(idText));
                nextImage();
            }
            else if (idText == 16)
            {
                _text.SetText("");
                _windowText.SetActive(true);
                _windowText.GetComponent<DialogCutscene>().startDialog(16, 19, 1);
                nextImage();
            }
        }
    }

    private void nextImage()
    {
        idImageIntroduction++;
        _imageLinker.sprite = imagesIntroduction[idImageIntroduction];
    }
}
