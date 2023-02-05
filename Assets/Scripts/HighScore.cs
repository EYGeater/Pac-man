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
        levelOneHighScore.text = " High Score: \n"+ GameManager.highScoreOne.ToString();
        levelTwoHighScore.text = " High Score: \n" + GameManager.highScoreTwo.ToString();
    }
}
