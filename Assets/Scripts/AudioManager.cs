using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    

    public AudioSource background;
    public AudioSource effect;

    public AudioClip menuBackgroundSound;
    public AudioClip gameBackgroundSound;
    public AudioClip coinEffectSound;
    public AudioClip jumpEffectSound;
    public AudioClip landingEffectSound;
    public AudioClip rollEffectSound;
    public AudioClip deathEffectSound;
    

    public bool soundEnable = true;

    AudioClip currentBackgroundSound;
   

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public void RefreshSoundState()
    {
        if (soundEnable)
        {
            background.UnPause();
        }
        else
        {
            background.Pause();
        }
    }

    public void PlayBackground(AudioClip backgroundSound)
    {
        if (soundEnable && backgroundSound != currentBackgroundSound)
        {
            currentBackgroundSound = backgroundSound;
            background.clip = backgroundSound;
            background.Play();
        }
    }

    public void PlayEffect(AudioClip effectSound)
    {
        if (soundEnable)
        {
            effect.PlayOneShot(effectSound);
        }
    }

    public void SetSoundEnable(bool enableState)
    {
        soundEnable = enableState;
    }
}
