using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [Header("Components"), SerializeField]
    private AudioSource source;

    [Header("Sounds"), SerializeField]
    private AudioClip buttonSound;

    public void PlayButtonSound()
    {
        source.PlayOneShot(buttonSound);
    }
}