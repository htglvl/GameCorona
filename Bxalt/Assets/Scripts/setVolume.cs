using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class setVolume : MonoBehaviour
{
    public AudioMixer audioMixer;
    public string ExposedParameter;
    public void SetLevel(float sliderValue)
    {
        audioMixer.SetFloat(ExposedParameter, Mathf.Log10(sliderValue) * 20);
    }
}
