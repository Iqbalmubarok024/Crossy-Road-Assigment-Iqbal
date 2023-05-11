using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneShotSFXZone : MonoBehaviour
{
    [SerializeField] private AudioSource audio;

    [SerializeField] private AudioClip sfxClip;
    private const string playerTag = "Hero";


    private void OnTriggerEnter(Collider other)
    {
        if (Duck.isGameOver) return;
        if (other.CompareTag(playerTag))
        {
            audio.PlayOneShot(sfxClip);
        }
    }
}
