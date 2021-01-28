using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class DoorSound : MonoBehaviour
{
    private AudioSource audioSource;
    private AudioClip[] openingClips;
    private AudioClip[] closingClips;
    private HingeJoint hingeJoint;
    private bool isOpen = false;

    private void Awake()
    {
        openingClips = Resources.LoadAll<AudioClip>("Sounds/Doors/Open door");
        closingClips = Resources.LoadAll<AudioClip>("Sounds/Doors/Close door");
        audioSource = (AudioSource)gameObject.AddComponent(typeof(AudioSource));

        var audioMixerGroup = Resources.Load<AudioMixer>("MainMixer").FindMatchingGroups("Effects")[0];
        audioSource.outputAudioMixerGroup = audioMixerGroup;
        audioSource.volume = 0.2f;
        hingeJoint = GetComponent<HingeJoint>();
    }

    void Update()
    {
        var angle = hingeJoint.angle;
        if (!isOpen && Mathf.Abs(angle) > 5)
        {
            isOpen = true;
            audioSource.clip = openingClips[Random.Range(0, openingClips.Length - 1)];
            audioSource.Play();
        }

        if (isOpen && Mathf.Abs(angle) < 5)
        {
            isOpen = false;
            audioSource.clip = closingClips[Random.Range(0, closingClips.Length - 1)];
            audioSource.Play();
        }
    }
}
