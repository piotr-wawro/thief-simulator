using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class DrawerSound : MonoBehaviour
{
    private AudioSource audioSource;
    private AudioClip[] slideClips;

    private float currentXPosition = 0;
    private float currentZPosition = 0;
    private bool isFirstCollision = true;
    private void Awake()
    {
        audioSource = (AudioSource)gameObject.AddComponent(typeof(AudioSource));
        slideClips = Resources.LoadAll<AudioClip>("Sounds/Drawers/");

        var audioMixerGroup = Resources.Load<AudioMixer>("MainMixer").FindMatchingGroups("Effects")[0];
        audioSource.outputAudioMixerGroup = audioMixerGroup;
        audioSource.volume = 0.1f;
        audioSource.playOnAwake = false;
    }

    private void Update()
    {
        if (isFirstCollision)
        {
            currentZPosition = gameObject.transform.localPosition.z;
            currentXPosition = gameObject.transform.localPosition.x;
            isFirstCollision = false;
        }
        else
        {
            if (!audioSource.isPlaying && (Mathf.Abs(gameObject.transform.localPosition.z - currentZPosition) > 0.1f || (Mathf.Abs(gameObject.transform.localPosition.x - currentXPosition) > 0.1f)))
            {
                audioSource.clip = slideClips[Random.Range(0, slideClips.Length)];
                audioSource.Play();
                currentZPosition = gameObject.transform.localPosition.z;
                currentXPosition = gameObject.transform.localPosition.x;
            }
        }
    }
}
