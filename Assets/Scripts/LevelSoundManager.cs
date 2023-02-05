using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(AudioSource))]
public class LevelSoundManager : MonoBehaviour
{
    public AudioClip levelOne { get; private set; }
    public AudioClip menu { get; private set; }
    public AudioSource levelSource { get; private set; }


    public void Awake()
    {
        levelSource = GetComponent<AudioSource>();
    }

    private void levelOneSound()
    {
        this.levelSource.clip = levelOne;
        this.levelSource.Play();
    }
    private void menuSound()
    {
        this.levelSource.clip = menu;
        this.levelSource.Play();
    }



}
