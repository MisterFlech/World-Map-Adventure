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
            DontDestroyOnLoad(gameObject); // Persiste � travers les sc�nes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // M�thode pour jouer une musique
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
            Debug.LogWarning("Musique non trouv�e: " + name);
        }
    }

    // M�thode pour jouer un SFX
    public void PlaySFX(AudioClip sfx, float volume = 0.7f)
    {
        if (sfx != null)
        {
            sfxSource.volume = volume;
            sfxSource.PlayOneShot(sfx);
        }
        else
        {
            Debug.LogWarning("SFX non trouv�: " + name);
        }
    }

    // M�thode pour arr�ter la musique
    public void StopMusic()
    {
        musicSource.Stop();
    }
}
