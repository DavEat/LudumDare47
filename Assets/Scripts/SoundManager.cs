using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] AudioSource mudSource = null;
    [SerializeField] AudioClip[] bells = null;
    [SerializeField] AudioSource bellsSource = null;
    [SerializeField] AudioSource enterCharSource = null;
    [SerializeField] AudioSource pickupPaperSource = null;
    [SerializeField] AudioSource flyPaperSource = null;

    [SerializeField] AudioSource clickButtonSource = null;

    [SerializeField] AudioSource ambainceSource = null;

    void Start()
    {
        ambainceSource.time = Random.Range(0, 60);
        ambainceSource.Play();
    }

    public void PlayBell()
    {
        if (GameManager.inst.dayEnded) return;

        if (!bellsSource.isPlaying)
        {
            bellsSource.clip = bells[Random.Range(0, bells.Length)];
            bellsSource.Play();
            enterCharSource.Play();
        }
    }
    public void PlayMud()
    {
        if (GameManager.inst.dayEnded) return;

        if (!mudSource.isPlaying)
            mudSource.Play();
    }
    public void StopMud()
    {
        if (mudSource.isPlaying)
            mudSource.Pause();
    }
    public void PlayPickUpPaper()
    {
        if (!pickupPaperSource.isPlaying)
        {
            pickupPaperSource.Play();
        }
    }
    public void PlayFlyPaper()
    {
        if (!flyPaperSource.isPlaying)
        {
            flyPaperSource.Play();
        }
    }
    public void ClickButton()
    {
        if (!clickButtonSource.isPlaying)
        {
            clickButtonSource.Play();
        }
    }
}
