using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HighScore : MonoBehaviour
{
    public Text levelOneHighScore;
    public Text levelTwoHighScore; 

   public void Update()
    {
        
        if (PlayerPrefs.GetInt("highScoreOne") == null)
        {
            PlayerPrefs.SetInt("highScoreOne", 0);
            levelOneHighScore.text = " High Score: \n" + PlayerPrefs.GetInt("highScoreOne").ToString();
        }
        else
        {
            levelOneHighScore.text = " High Score: \n" + PlayerPrefs.GetInt("highScoreOne").ToString();
        }
        if (PlayerPrefs.GetInt("highScoreTwo") == null)
        {
            PlayerPrefs.SetInt("highScoreTwo", 0);
            levelTwoHighScore.text = " High Score: \n" + PlayerPrefs.GetInt("highScoreTwo").ToString();
        }
        else
        {
            levelTwoHighScore.text = " High Score: \n" + PlayerPrefs.GetInt("highScoreTwo").ToString();
        }
        
      
    }
}
