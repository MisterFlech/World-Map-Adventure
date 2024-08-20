using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource musicSource;
    public AudioSource sfxSource;

    public SoundEffect soundEffect = new SoundEffect();
    [System.Serializable]
    public class SoundEffect
    {
        [SerializeField] public AudioClip wizard_in;
        [SerializeField] public AudioClip wizard_out;
        [SerializeField] public AudioClip enter_in;
        [SerializeField] public AudioClip enter_out;
    }

    // Méthode pour jouer un SFX
    public void PlaySFXName(string sfxName, float volume = 0.7f)
    {
        AudioClip sfx = null;
        if (sfxName == "wizard_in")
        {
            sfx = soundEffect.wizard_in;
        } else if (sfxName == "wizard_out")
        {
            sfx = soundEffect.wizard_out;
        } else if (sfxName == "enter_in")
        {
            sfx = soundEffect.enter_in;
        } else if (sfxName == "enter_out")
        {
            sfx = soundEffect.enter_out;
        }
        if (sfx != null)
        {
            PlaySFX(sfx, volume);
        }
        
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Persiste à travers les scènes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Méthode pour jouer une musique
    public void PlayMusic(AudioClip music, float volume = 0.5f, bool loop = true)
    {
        if (music != null)
        {
            if (musicSource.clip == music && musicSource.isPlaying)
            {
                //Debug.Log("same music");
            }
            else
            {
                musicSource.volume = volume;
                musicSource.clip = music;
                musicSource.loop = loop;
                musicSource.Play();
            }
        }
        else
        {
            Debug.LogWarning("Musique non trouvée: " + name);
        }
    }

    // Méthode pour jouer un SFX
    public void PlaySFX(AudioClip sfx, float volume = 0.7f)
    {
        if (sfx != null)
        {
            sfxSource.volume = volume;
            sfxSource.PlayOneShot(sfx);
        }
        else
        {
            Debug.LogWarning("SFX non trouvé: " + name);
        }
    }

    // Méthode pour arrêter la musique
    public void StopMusic()
    {
        musicSource.Stop();
    }
}
