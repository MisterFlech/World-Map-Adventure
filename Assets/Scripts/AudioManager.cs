using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource musicSource;
    public AudioSource sfxSource;

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
                Debug.Log("same music");
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
