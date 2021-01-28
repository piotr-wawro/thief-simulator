using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassImpactSound : MonoBehaviour
{
    private AudioSource audioSource;
    private AudioClip[] audioClips;

    private bool isFirstCollision = true;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioClips = Resources.LoadAll<AudioClip>("Sounds/Impacts/Glass");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isFirstCollision)
        {
            StartCoroutine(WaitForFirstCollisions());
        }
        else
        {
            if (!audioSource.isPlaying && !collision.collider.CompareTag("Item"))
            {
                audioSource.PlayOneShot(audioClips[Random.Range(0, audioClips.Length)]);
            }
        }
    }

    private IEnumerator WaitForFirstCollisions()
    {
        yield return new WaitForSeconds(2);
        isFirstCollision = false;
    }
}
