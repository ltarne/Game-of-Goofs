using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class OptionsMenuController : MonoBehaviour {

    public AudioMixer m_AudioMixer;

    public void SetVolume(float volume)
    {
        m_AudioMixer.SetFloat("m_Volume", volume);
    }
}
