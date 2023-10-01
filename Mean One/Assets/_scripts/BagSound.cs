using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagSound : MonoBehaviour
{
    public AudioClip[] bagSounds;

    public AudioSource audioSource;

    public void PlayBagSound(AudioClip[] clips)
    {
        int randomIndex = Random.Range(0, clips.Length);
        audioSource.clip = clips[randomIndex];
        audioSource.Play();
    }
}
