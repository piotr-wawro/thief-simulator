using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class CabinetDoorSound : MonoBehaviour
{
    private AudioSource audioSource;
    private AudioClip openingClip;
    private AudioClip closingClip;

    private bool isOpen = false;

    private void Awake()
    {
        openingClip = Resources.Load<AudioClip>("Sounds/Cabinet doors/open");
        closingClip = Resources.Load<AudioClip>("Sounds/Cabinet doors/close");
        audioSource = (AudioSource)gameObject.AddComponent(typeof(AudioSource));

        var audioMixerGroup = Resources.Load<AudioMixer>("MainMixer").FindMatchingGroups("Effects")[0];
        audioSource.outputAudioMixerGroup = audioMixerGroup;
        audioSource.volume = 0.8f;
        audioSource.playOnAwake = false;
    }

    void Update()
    {
        var angle = GetComponent<HingeJoint>().angle;
        if (!isOpen && Mathf.Abs(angle) > 5)
        {
            isOpen = true;
            audioSource.clip = openingClip;
            audioSource.Play();
        }

        if (isOpen && Mathf.Abs(angle) < 5)
        {
            isOpen = false;
            audioSource.clip = closingClip;
            audioSource.Play();
        }
    }
}
