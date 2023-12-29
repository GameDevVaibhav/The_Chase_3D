using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    private AudioSource audioSource;
    public AudioClip explosionClip;
    public AudioClip cashBoxDestroyedClip;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayExplosionSound()
    {
        if (audioSource != null && explosionClip != null)
        {
            audioSource.clip = explosionClip;
            audioSource.Play();
        }
    }

    public void PlayCashBoxDestroyedSound()
    {
        if (audioSource != null && cashBoxDestroyedClip != null)
        {
            audioSource.clip = cashBoxDestroyedClip;
            audioSource.Play();
        }
    }

    
}
