using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Introduction : CutscenesInterface
{
    public GameObject imageIntroduction;
    public GameObject textIntroduction;
    public GameObject textPanel;

    private Image _imageLinker;
    private TMP_Text _text;

    private int idImageIntroduction = 0;
    private int idText = 0;
    [SerializeField] public Sprite[] imagesIntroduction;

    public AudioClip song;

    public SoundEffect soundEffect = new SoundEffect();
    [System.Serializable]
    public class SoundEffect
    {
        [SerializeField] public AudioClip enter;
        [SerializeField] public AudioClip boom;
        [SerializeField] public AudioClip wizard;
        [SerializeField] public AudioClip spell;
        [SerializeField] public AudioClip chest;
        [SerializeField] public AudioClip lamp;
        [SerializeField] public AudioClip unpop;
        [SerializeField] public AudioClip exit;
    }

    protected new void Start()
    {
        base.Start();

        _windowText.SetActive(false);
        _imageLinker = imageIntroduction.GetComponent<Image>();
        _text = textIntroduction.GetComponent<TMP_Text>();
        _text.SetText(TextDatabase.getText(0));

        AudioManager.instance.PlayMusic(song);
        StartCoroutine(FadeOut(1.0f));
    }

    // Update is called once per frame
    void Update()
    {
        if (!pause) { 
            if (Input.GetButtonDown("Jump"))
            {
                idText++;
                Debug.Log(idText);

                if (idText == 1)
                {
                    textUpdate();
                }
                else if (idText == 3)
                {
                    AudioManager.instance.PlaySFX(soundEffect.enter);
                    StartCoroutine(FadeIn(0.5f, 0.25f, () => dialog4(), true));
                } 
                else if (idText == 5)
                {
                    AudioManager.instance.PlaySFX(soundEffect.lamp);
                    textUpdate();
                }
                else if (idText == 6)
                {
                    AudioManager.instance.PlaySFX(soundEffect.boom);
                    textUpdate();
                }
                else if (idText == 7)
                {
                    StartCoroutine(FadeIn(0.75f, 0.75f, () => dialog7(), true));
                }
                else if (idText == 15)
                {
                    _windowText.SetActive(false);
                    AudioManager.instance.PlaySFX(soundEffect.spell);
                    textUpdate();
                }
                else if (idText == 16)
                {
                    AudioManager.instance.PlaySFX(soundEffect.unpop);
                    nextImage();
                    _text.SetText("");
                    startWaiting(1f, () => dialog16());
                   
                }
                else if (idText >= 20)
                {
                    AudioManager.instance.PlaySFX(soundEffect.exit);
                    _windowText.SetActive(false);
                    StartCoroutine(FadeIn(0.75f, 0.75f, () => endTeleport(), false));
                } else
                {
                    if (idText < 7)
                    {
                        _text.SetText(TextDatabase.getText(idText));
                    }
                }
            }
        }
    }

    public void dialog4()
    {
        AudioManager.instance.PlaySFX(soundEffect.chest);
        textUpdate();
    }

    public void textUpdate()
    {
        nextImage();
        _text.SetText(TextDatabase.getText(idText));
    }

    public void dialog16()
    {
        _windowText.SetActive(true);
        _windowText.GetComponent<DialogCutscene>().startDialog(1, 0);
        nextImage();
    }

    public void dialog7()
    {
        textPanel.SetActive(false);
        _text.SetText("");
        nextImage();

        AudioManager.instance.PlaySFX(soundEffect.wizard);
        startWaiting(1.25f, () => dialog7bis());
    }

    public void dialog7bis()
    {
        _windowText.SetActive(true);
        _windowText.GetComponent<DialogCutscene>().startDialog(0, 0);
    }

    public void endTeleport()
    {
        SceneManager.LoadScene("MM_HeroVillage");
        //FadeOut ne marche pas à cause du téléport et absence de l'objet black
    }

    private void nextImage()
    {
        idImageIntroduction++;
        _imageLinker.sprite = imagesIntroduction[idImageIntroduction];
    }
}
