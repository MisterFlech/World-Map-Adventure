using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public static void PlaySFX(AudioClip effect, float volume = 0.7f, float pitch = 1f)
    {
        AudioSource source = CreateNewSource(string.Format("SFX [{0}]", effect.name));
        source.clip = effect;
        source.volume = volume;
        source.pitch = pitch;
        source.Play();

        Destroy(source.gameObject, effect.length);
    }

    public static AudioSource CreateNewSource(string _name)
    {
        AudioSource newSource = new GameObject(_name).AddComponent<AudioSource>();
        newSource.transform.SetParent(instance.transform);
        return newSource;
    }

    public AudioClip CreateAudioClip(string name, string file)
    {
        return Resources.Load<AudioClip>(file + "/" + name);
    }

    /* Playing music */

    private SONG activeSong;
    public float songTransitionSpeed = 2f;
    public bool songSmoothTransition = true;

    public void PlayMusic(string songName, float maxVolume = 1f, float pitch = 1f, float startingVolume = 0f, bool playOnStart = true, bool loop = true)
    {
        AudioClip song = CreateAudioClip(songName, "Musics");
        if (song != null)
        {
            if (activeSong == null || activeSong.clip != song)
            {
                activeSong = new SONG(song, maxVolume, pitch, startingVolume, playOnStart, loop);
            }
        }
        else
        {
            activeSong = null;
        }

        StopAllCoroutines();
        StartCoroutine(VolumeLeveling(true));
    }

    public void StopMusic()
    {
        if (activeSong != null)
        {
            StopAllCoroutines();
            StartCoroutine(VolumeLeveling(false));
        }
    }

    IEnumerator VolumeLeveling(bool levelingUp)
    {
        while (TransitionSongs(levelingUp))
        {
            yield return new WaitForEndOfFrame();
        }
    }

    bool TransitionSongs(bool levelingUp)
    {
        bool anyValueChanged = false;
        float speed = songTransitionSpeed * Time.deltaTime;
        SONG song = activeSong;

        if (levelingUp)
        {
            if (song.volume < song.maxVolume)
            {
                song.volume = songSmoothTransition ? Mathf.Lerp(song.volume, song.maxVolume, speed) : Mathf.MoveTowards(song.volume, song.maxVolume, speed);
                anyValueChanged = true;
            }
        }
        else
        {
            if (song.volume > 0)
            {
                song.volume = songSmoothTransition ? Mathf.Lerp(song.volume, 0, speed) : Mathf.MoveTowards(song.volume, 0, speed);
                anyValueChanged = true;
            }
            else
            {
                song.DestroySong();
                activeSong = null;
            }
        }

        return anyValueChanged;
    }

    [System.Serializable]
    public class SONG
    {
        public AudioSource source;
        public AudioClip clip { get { return source.clip; } set { source.clip = value; } }
        public float maxVolume = 1f;

        public SONG(AudioClip clip, float _maxVolume, float pitch, float startingVolume, bool playOnStart, bool loop)
        {
            source = AudioManager.CreateNewSource(string.Format("SONG [{0}]", clip.name));
            source.clip = clip;
            source.volume = startingVolume;
            maxVolume = _maxVolume;
            source.pitch = pitch;
            source.loop = loop;

            if (playOnStart)
            {
                source.Play();
            }
        }

        public float volume { get { return source.volume; } set { source.volume = value; } }
        public float pitch { get { return source.pitch; } set { source.pitch = value; } }

        public void Play()
        {
            source.Play();
        }

        public void Stop()
        {
            source.Stop();
        }

        public void Pause()
        {
            source.Pause();
        }

        public void UnPause()
        {
            source.UnPause();
        }

        public void DestroySong()
        {
            DestroyImmediate(source.gameObject);
        }
    }
}
