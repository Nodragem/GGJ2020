using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSounds : MonoBehaviour
{
    Animator anim;


    AudioSource audioSource;
    public AudioClip[] throwClips;
    public AudioClip[] footstepClips;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnFireLimb(InputValue value)
    {
        PlayClipOneShot(throwClips, false);
    }

    void OnFireArm(InputValue value)
    {
        PlayClipOneShot(throwClips, false);
    }

    void OnFireLeg(InputValue value)
    {
        PlayClipOneShot(throwClips, false);
    }

    void Footstep()
    {
        PlayClipOneShot(footstepClips, false);
    }

    void PlayClipOneShot(AudioClip[] clips, bool pitchAdjust)
    {
        int rand = Random.Range(0, clips.Length);
        if(pitchAdjust)
            audioSource.pitch = Random.Range(-0.90f, 1.1f);
        else
            audioSource.pitch = 1;
        audioSource.PlayOneShot(clips[rand]);
    }
}
