using System;
using UnityEngine.Audio;
using UnityEngine;

public class audiomanager : MonoBehaviour
{
    public audio[] sounds;
    public static audiomanager Instance;
    public bool OnlyOneCanBe;
    // Use this for initialization
    void Awake()
    {
        if (OnlyOneCanBe)
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this.gameObject);
                return;
            }
            DontDestroyOnLoad(this.gameObject);
        }
        foreach (audio s in sounds)
        {
            s.AuSource = gameObject.AddComponent<AudioSource>();
            s.AuSource.clip = s.amThanh;
            s.AuSource.outputAudioMixerGroup = s.audioMixerGroup;
            s.AuSource.volume = s.volume;
            s.AuSource.pitch = s.pitch;
            s.AuSource.loop = s.loop;
            s.AuSource.playOnAwake = s.playOnAwake;
            if (s.AuSource.playOnAwake)
            {
                s.AuSource.Play();
            }
        }
    }
    public void Play(string name)
    {

        audio s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            return;
        }
        s.AuSource.Play();
    }
}
