using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimbSounds : MonoBehaviour
{
    public AudioClip[] thudClips;
    public Vector2 impulseRange;

    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.impulse.magnitude);
        float volume = Mathf.InverseLerp(impulseRange.x, impulseRange.y, collision.impulse.magnitude);
        volume = Mathf.Clamp(volume, 0.5f, 1f);
        audioSource.volume = volume;
        PlayClipOneShot(thudClips, false);
    }

    void PlayClipOneShot(AudioClip[] clips, bool pitchAdjust)
    {
        int rand = Random.Range(0, clips.Length);
        if (pitchAdjust)
            audioSource.pitch = Random.Range(-0.90f, 1.1f);
        else
            audioSource.pitch = 1;
        audioSource.PlayOneShot(clips[rand]);
    }
}
