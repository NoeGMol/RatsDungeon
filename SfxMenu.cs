using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfxMenu : MonoBehaviour
{
    [Header("Audio Sources (Assign in Inspector)")]
    [SerializeField] AudioSource SFXSource;


    [Header("Audio Clips (Assign in Inspector)")]

    public AudioClip click;

    public void PlaySFXMenu(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
