using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    public AudioClip[] walkingClips; // Array of walking footstep audio clips
    public AudioClip[] runningClips; // Array of running footstep audio clips
    public float walkingStepInterval = 0.5f; // Interval between walking footsteps
    public float runningStepInterval = 0.3f; // Interval between running footsteps

    private float stepInterval; // Current interval between footsteps
    private float timer = 0f; // Timer to track footsteps

    private AudioSource audioSource;
    private CharacterController characterController;

    

    public bool isPlayer = true;

    [Header("Wind loop Sound")]

    public AudioSource windSoundSource;
    public float increaseSpeed = 1.0f;
    public float stopTime = 0.3f;
    public float targetVolume = 1.0f;

    private float currentVolume;
    private float currentVelocity;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        characterController = GetComponent<CharacterController>();

        currentVolume = windSoundSource.volume;
    }

    private void Update()
    {
        windSoundSource.volume = currentVolume;

        if(isPlayer)
        {
            if (characterController.isGrounded && characterController.velocity.magnitude > 0)
            {
                if (Input.GetKey(KeyCode.LeftShift)) // If the left shift key is being held
                {
                    stepInterval = runningStepInterval;
                    PlayFootstepSound(runningClips);

                    currentVolume = Mathf.SmoothDamp(currentVolume, targetVolume, ref currentVelocity, increaseSpeed);
                }
                else // If the left shift key is not being held
                {
                    stepInterval = walkingStepInterval;
                    PlayFootstepSound(walkingClips);

                    currentVolume = Mathf.SmoothDamp(currentVolume, 0.0f, ref currentVelocity, stopTime);
                }

            }
            else
            {
                // Reset the timer when the player is not moving
                timer = 0f;
            }
        }
        else
        {
            if (characterController.isGrounded && characterController.velocity.magnitude > 0)
            {

                stepInterval = walkingStepInterval;
                PlayFootstepSound(walkingClips);

            }
            else
            {
                // Reset the timer when the player is not moving
                timer = 0f;
            }
        }
    }

    private void PlayFootstepSound(AudioClip[] clips)
    {
        if (timer >= stepInterval)
        {
            int randomIndex = Random.Range(0, clips.Length);
            audioSource.clip = clips[randomIndex];
            audioSource.Play();

            timer = 0f; // Reset the timer
        }

        timer += Time.deltaTime;
    }
}