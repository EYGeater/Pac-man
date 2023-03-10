using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public Ghost[] ghosts;
    public Pacman pacman;
    public Transform pellets;
    public Transform fruit;
    public Text scoreText;
    public Text livesText;
    public Text finalText;
    public GameObject gameOverCanvas;
    public GameObject map;
    public static int highScoreOne;
    public static int highScoreTwo;
    public int level; 

    public AudioClip levelOneclip;
    public AudioClip menu;
    public AudioClip gameOverClip;
    public AudioSource levelSource { get; private set; }

    public int ghostMultiplier { get; private set; } = 1;

    public int score { get; private set; }
    public int lives { get; private set; }

    private int pelletsEaten = 0;
    private bool gameOver = false; 


    private void Awake()
    {
        gameOverCanvas.SetActive(false);
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
        if (gameOver == true){
            scoreText.text = " " ;
            livesText.text = " " ;
        }
        else
        {
            scoreText.text = "Score: " + score;
            livesText.text = "Lives: " + lives;
        }
        
        /*if (this.lives <= 0 && Input.anyKeyDown)
        {
            NewGame();
        }*/
 
    }
    private void NewGame()
    {
        map.gameObject.SetActive(true);
        SetScore(0);
        SetLives(3);
        NewRound();
    }
    private void NewRound()
    {
        fruit.gameObject.SetActive(false);
        gameOver = false;

        foreach (Transform pellet in this.pellets)
        {
            pellet.gameObject.SetActive(true);
        }
        ResetState();
    }

    private void GameOver()
    {
        fruit.gameObject.SetActive(false);
        gameOver = true;
        for (int i = 0; i < this.ghosts.Length; i++)
        {
            this.ghosts[i].gameObject.SetActive(false);
        }
        livesText.text = " ";
        scoreText.text = " ";

        map.gameObject.SetActive(false);
        this.pacman.gameObject.SetActive(false);
        GameOverSound();

        gameOverCanvas.SetActive(true);
        if ( level == 1 && highScoreOne <= score)
        {
            finalText.text = "New High Score!\n" + score;
            highScoreOne = score;
        }
        else if ( level == 1 && highScoreOne > score)
        {
            finalText.text = "High Score:\n" + highScoreOne + "\nYour Score: \n" + score;
        }
        if (level == 2 && highScoreTwo <= score)
        {
            finalText.text = "New High Score!\n" + score;
            highScoreTwo = score;
        }
        else if (level == 2 && highScoreTwo > score)
        {
            finalText.text = "High Score:\n" + highScoreTwo + "\nYour Score: \n" + score;
        }



        //put in game over text with score at the middle then have a button to go back to the menu 


    }

    private void ResetState()
    {
        fruit.gameObject.SetActive(false);
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
        pelletsEaten++;

        if(pelletsEaten == 200)
        {
            pelletsEaten = 0;
            fruit.gameObject.SetActive(true);
        }

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

    public void FruitEaten(Fruit fruit)
    {
        fruit.gameObject.SetActive(false);
        SetScore(this.score + fruit.points);
    }
    public void FruitDespawn(Fruit fruit)
    {
        fruit.gameObject.SetActive(false);
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
