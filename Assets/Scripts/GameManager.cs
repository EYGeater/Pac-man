using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public Ghost[] ghosts;
    public Pacman pacman;
    public Transform pellets;
    public Text scoreText;
    public Text livesText;
    public GameObject GameOverCanvas;

    public AudioClip levelOneclip;
    public AudioClip menu;
    public AudioClip gameOverClip;
    public AudioSource levelSource { get; private set; }

    public int ghostMultiplier { get; private set; } = 1;

    public int score { get; private set; }
    public int lives { get; private set; }




    private void Awake()
    {
        GameOverCanvas.SetActive(false);
        levelSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        
        NewGame();
        
    }
    /*public void LevelOne()
    {
        SceneManager.LoadScene("Level1");
        //mainMenu.gameObject.SetActive(false); 
        //levelOneSound();
        //NewGame();
    }
    public void LevelTwo()
    {
        SceneManager.LoadScene("Level2");
    }

    public void Menu()
    {
        mainMenu.gameObject.SetActive(true);
        menuSound();
    }*/


    private void Update()
    {
        scoreText.text = "Score: " + score;
        livesText.text = "Lives: " + lives;
        /*if (this.lives <= 0 && Input.anyKeyDown)
        {
            NewGame();
        }*/
 
    }
    private void NewGame()
    {
        SetScore(0);
        SetLives(3);
        NewRound();
    }
    private void NewRound()
    {
        foreach (Transform pellet in this.pellets)
        {
            pellet.gameObject.SetActive(true);
        }
        ResetState();
    }

    private void GameOver()
    {
        for (int i = 0; i < this.ghosts.Length; i++)
        {
            this.ghosts[i].gameObject.SetActive(false);
        }

        this.pacman.gameObject.SetActive(false);
        GameOverSound();
        GameOverCanvas.SetActive(true);

        //put in game over text with score at the middle then have a button to go back to the menu 


    }

    private void ResetState()
    {
        RestGhostMultiplier();
        for (int i = 0; i < this.ghosts.Length; i++)
        {
            this.ghosts[i].ResetState();
        }

        this.pacman.ResetState();
    }


    private void SetScore(int score)
    {
        this.score = score;
    }
    private void SetLives(int lives)
    {
        this.lives = lives;
    }


    public void GhostEaten(Ghost ghost)
    {
        int points = ghost.points * this.ghostMultiplier;
        SetScore(this.score + points);
        this.ghostMultiplier++;
    }
    public void PacmanEaten()
    {
        this.pacman.DeathSequence();
        //this.pacman.gameObject.SetActive(false);
        SetLives(this.lives - 1);
        if (this.lives > 0)
        {
            Invoke(nameof(ResetState), 1.0f);
        }
        else
        {
            Invoke(nameof(GameOver), 1.0f);
        }
    }
    public void PelletEaten(Pellet pellet)
    {
        pellet.gameObject.SetActive(false);
        SetScore(this.score + pellet.points);
        if (!HasRemainingPellets())
        {
            this.pacman.gameObject.SetActive(false);
            Invoke(nameof(NewRound), 3.0f);
        }
    }
    public void PowerPelletEaten(PowerPellet pellet)
    {

        for (int i = 0; i < this.ghosts.Length; i++)
        {
            this.ghosts[i].frightened.Enable(pellet.duration);
        }

        PelletEaten(pellet);
        CancelInvoke(); // this is so if you get another powerpellet right after another - the muiltiplier stays 
        Invoke(nameof(RestGhostMultiplier), pellet.duration);


    }

    private bool HasRemainingPellets()
    {
        foreach (Transform pellet in this.pellets)
        {
            if (pellet.gameObject.activeSelf)
            {
                return true;
            }
        }
        return false;
    }

    private void RestGhostMultiplier()
    {
        this.ghostMultiplier = 1;
    }
    private void levelOneSound()
    {
        this.levelSource.clip = levelOneclip;
        this.levelSource.Play();
    }
    private void menuSound()
    {
        this.levelSource.clip = menu;
        this.levelSource.Play();
    }
    private void GameOverSound()
    {
        this.levelSource.clip = gameOverClip;
        this.levelSource.Play();
    }
}
