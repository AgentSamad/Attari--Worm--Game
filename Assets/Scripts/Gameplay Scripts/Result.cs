using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Result : MonoBehaviour
{
    // introduce a variable and a filed of last high score in this script to find last high score 
    
    public Text resultName;
    public Text resultScore;
    public Text lastHighScore;

    // Use this for initialization
    void Start()
    {
        if (!PlayerPrefs.HasKey("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", 0);
        }
    }

    public void SetScore(string name, int score)
    {
        if (name == "") name = "Score";
        resultName.text = name;
        resultScore.text = score.ToString();
        SetHighScore(score);
    }


    void SetHighScore(int currentScore)
    {
        int lastScore = PlayerPrefs.GetInt("HighScore");

        if (currentScore >= lastScore)
            lastScore = currentScore;
            
        PlayerPrefs.SetInt("HighScore",lastScore);
        lastHighScore.text = "Last High Score : " + lastScore;
    }
}