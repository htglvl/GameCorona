using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class audio
{
    public AudioClip amThanh;
    public string name;
    [Range(0f, 1f)]
    public float volume = 1f;
    [Range(0.1f, 3f)]
    public float pitch = 1f;
    [HideInInspector]
    public AudioSource AuSource;
    public AudioMixerGroup audioMixerGroup;
    public bool loop = false, playOnAwake = false;

}
