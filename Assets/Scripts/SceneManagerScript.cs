using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    public AudioClip selectNoise;
    public AudioSource noiseSource { get; private set; }


    private void Awake()
    {
        noiseSource = GetComponent<AudioSource>();
    }
    public void LevelOne()
    {
        this.noiseSource.clip = selectNoise;
        this.noiseSource.loop = false;
        this.noiseSource.Play();
        Invoke(nameof(LoadLevelOne), 0.5f);
        
    }
    public void LevelTwo()
    {
        this.noiseSource.clip = selectNoise;
        this.noiseSource.loop = false;
        this.noiseSource.Play();
        Invoke(nameof(LoadLevelTwo), 0.5f);
    }

    public void Menu()
    {
        this.noiseSource.clip = selectNoise;
        this.noiseSource.loop = false;
        this.noiseSource.Play();
        Invoke(nameof(LoadMenu), 0.5f);
    }
    public void Credits()
    {
        this.noiseSource.clip = selectNoise;
        this.noiseSource.loop = false;
        this.noiseSource.Play();
        Invoke(nameof(LoadCredits), 0.5f);
    }

    public void LoadLevelOne()
    {
        SceneManager.LoadScene("Level1");

    }
    public void LoadLevelTwo(){

        SceneManager.LoadScene("Level2");

    }
    public void LoadMenu()
    {

        SceneManager.LoadScene("Menu");

    }
    public void LoadCredits()
    {
        SceneManager.LoadScene("Credits");

    }


}
