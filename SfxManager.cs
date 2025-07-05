using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfxManager : MonoBehaviour
{
    [Header("Audio Sources (Assign in Inspector)")]
    [SerializeField] AudioSource SFXSource;


    [Header("Audio Clips (Assign in Inspector)")]
    public AudioClip punch;
    public AudioClip hurt;
    public AudioClip hit;
    public AudioClip ratHurt;
    public AudioClip coin;
    public AudioClip click;

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

}
