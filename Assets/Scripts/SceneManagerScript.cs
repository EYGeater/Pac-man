using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    public void LevelOne()
    {
        Debug.Log("huh");
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
        SceneManager.LoadScene("Menu");
    }
}
