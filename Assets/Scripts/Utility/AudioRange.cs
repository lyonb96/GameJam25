using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioRange : MonoBehaviour
{
    public bool AdjustPitch = true;
    public bool PlayOnStart = true;
    public bool PlayOnEnable = false;
    public float lowestPitch;
    public float highestPitch;
    public AudioClip[] Sounds;

    UtilityManager UM;
    [SerializeField]
    AudioSource Audio;

    float AdjustedPitch;
    public bool affectedBySlowMo = false;

    void Start()
    {
        UM = UtilityManager.UM;
        if(Audio == null) { Audio = gameObject.GetComponent<AudioSource>(); }
        if (PlayOnStart && !PlayOnEnable)
        {
            PlayAudio();
        }
    }
    void OnEnable()
    {
        UM = UtilityManager.UM;
        Audio = gameObject.GetComponent<AudioSource>();
        if (PlayOnStart && PlayOnEnable)
        {
            PlayAudio();
        }
    }
    public void SetPlay(AudioClip Clip, float Pitch, float Volume)
    {
        Audio = Audio != null ? Audio : gameObject.GetComponent<AudioSource>();
        Audio.clip = Clip;
        Audio.pitch = Pitch;
        Audio.volume = Volume * UM.SFXVolumeModifier;
        AdjustedPitch = Pitch;

        Audio.Play();
    }
    public void PlayAudio()
    {
        AdjustedPitch = Random.Range(lowestPitch, highestPitch);
        Audio = gameObject.GetComponent<AudioSource>();
        int index = Random.Range(0, Sounds.Length);
        Audio.clip = Sounds[index];

        Audio.Play();
    }

    public void PlayAudio(int index)
    {
        AdjustedPitch = Random.Range(lowestPitch, highestPitch);
        Audio = gameObject.GetComponent<AudioSource>();
        Audio.clip = Sounds[index];

        Audio.Play();
    }

    public void PlayAudio(string clipName)
    {
        AdjustedPitch = Random.Range(lowestPitch, highestPitch);
        Audio = gameObject.GetComponent<AudioSource>();
        foreach (AudioClip clip in Sounds)
        {
            if (clip.name == clipName)
            {
                Audio.clip = clip;
                break;
            }
        }

        Audio.Play();
    }

    public void PlayAudio(AudioClip[] clips)
    {
        AdjustedPitch = Random.Range(lowestPitch, highestPitch);
        Audio = gameObject.GetComponent<AudioSource>();
        int index = Random.Range(0, clips.Length);
        Audio.clip = clips[index];

        Audio.Play();
    }

    public void PlayAudio(AudioClip clip)
    {
        AdjustedPitch = Random.Range(lowestPitch, highestPitch);
        Audio = gameObject.GetComponent<AudioSource>();
        Audio.clip = clip;

        Audio.Play();
    }

    public void PlayAudio(int[] clips)
    {
        AdjustedPitch = Random.Range(lowestPitch, highestPitch);
        Audio = gameObject.GetComponent<AudioSource>();
        int index = Random.Range(0, clips.Length);
        Audio.clip = Sounds[clips[index]];

        Audio.Play();
    }

    private void Update()
    {
        if (AdjustPitch && Audio.isPlaying)
        {
            Audio.pitch = affectedBySlowMo ? AdjustedPitch * Time.timeScale : AdjustedPitch;
        }
    }
}