using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    public static AudioManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<AudioManager>();
                if (instance == null)
                {
                    GameObject obj = new GameObject("AudioManager");
                    instance = obj.AddComponent<AudioManager>();
                }
            }
            return instance;
        }
    }

    private List<AudioSource> audioSources = new List<AudioSource>();
    private AudioSource musicSource = null;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public AudioSource PlayClip(Clip clip)
    {
        AudioSource source = GetAvailableAudioSource();
        if (source != null)
        {
            source.clip = clip.clip;
            source.volume = clip.volume;
            source.Play();
            return source;
        }
        return null;
    }

    public AudioSource PlayClip(Clip clip, float volume)
    {
        AudioSource source = GetAvailableAudioSource();
        if (source != null)
        {
            source.clip = clip.clip;
            source.volume = volume;
            source.Play();
            return source;
        }
        return null;
    }

    public AudioSource PlayLoopingClip(Clip clip)
    {
        AudioSource source = GetAvailableAudioSource();
        if (source != null)
        {
            source.clip = clip.clip;
            source.volume = clip.volume;
            source.loop = true;
            source.Play();
            return source;
        }
        return null;
    }

    public void PauseLoopingClip(AudioSource source)
    {
        if (source != null)
        {
            source.volume = 0f;
        }
    }

    public void UnPauseLoopingClip(AudioSource source, float volume = 1f)
    {
        if (source != null)
        {
            source.volume = volume;
        }
    }

    public void StopClip(AudioSource source)
    {
        if (source != null)
        {
            source.Stop();
        }
    }

    public AudioSource PlayMusic(Clip clip)
    {
        if (musicSource == null)
        {
            musicSource = gameObject.AddComponent<AudioSource>();
        }
        musicSource.clip = clip.clip;
        musicSource.loop = true;
        musicSource.volume = clip.volume;
        musicSource.Play();
        return musicSource;
    }

    public AudioSource PlayMusic(Clip clip, float volume)
    {
        if (musicSource == null)
        {
            musicSource = gameObject.AddComponent<AudioSource>();
        }
        musicSource.clip = clip.clip;
        musicSource.loop = true;
        musicSource.volume = volume;
        musicSource.Play();
        return musicSource;
    }

    public void StopMusic()
    {
        if (musicSource != null)
        {
            musicSource.Stop();
        }
    }


    private AudioSource GetAvailableAudioSource()
    {
        foreach (AudioSource source in audioSources)
        {
            if (!source.isPlaying)
            {
                return source;
            }
        }

        AudioSource newSource = gameObject.AddComponent<AudioSource>();
        audioSources.Add(newSource);
        return newSource;
    }
}