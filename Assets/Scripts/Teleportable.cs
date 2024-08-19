using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleportable : MonoBehaviour
{
    public string teleportName;
    public AudioClip song;

    public SoundEffect soundEffect = new SoundEffect();
    [System.Serializable]
    public class SoundEffect
    {
        [SerializeField] public AudioClip sfx;
    }

    public void teleporting()
    {
        PlayerMovement.freeze = true;
        AudioManager.instance.PlaySFX(soundEffect.sfx);
        CanvasGame.instance.startFadeIn(0.5f, 0, () => teleporting2(), false);
    }

    public void teleporting2()
    {
        SceneManager.LoadScene(teleportName);
        AudioManager.instance.PlayMusic(song);
        PlayerMovement.freeze = false;
        CanvasGame.instance.startFadeOut(0.5f);
    }
}
