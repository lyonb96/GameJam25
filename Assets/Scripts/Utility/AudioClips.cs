using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioClips : MonoBehaviour
{
    [SerializeField]
    public Clip[] clips;

    public Clip GetClip(string clipName)
    {
        foreach (Clip clip in clips)
        {
            if (clip.clipName == clipName)
            {
                return clip;
            }
        }
        return null;
    }

    public Clip GetClip(int index)
    {
        return clips[index];
    }
}

[System.Serializable]
public class Clip
{
    public string clipName;
    public AudioClip clip;
    [Range(0f, 1f)]
    public float volume = 1f;

    public Clip(AudioClip clip, string clipName, float volume)
    {
        this.clip = clip;
        this.clipName = clipName;
        this.volume = volume;
    }
}