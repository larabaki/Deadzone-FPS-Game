using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootSteps : MonoBehaviour
{
    public AudioClip[] footStepClips;

    public AudioSource audioSource;

    public CharacterController controller;
    public float footstepThreshhold;

    public float footstepRate;
    private float lastFootstepTime;

    private void Update()
    {
        if(controller.velocity.magnitude > footstepThreshhold)
        {
            if (Time.time - lastFootstepTime > footstepRate)
            {
                lastFootstepTime = Time.time;
                //playoneshot= send over an audio clip and it will play this audio clip once
                audioSource.PlayOneShot(footStepClips[Random.Range(0, footStepClips.Length)]);
            }
        }
    }
}

