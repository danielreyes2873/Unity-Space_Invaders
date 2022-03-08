using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    private int score = 0;
    private int highScore = 0;
    private string highScoreKey = "HighScore";
    private string scoreFormat = "0000.##";
    // Start is called before the first frame update
    void Start()
    {
        highScore = PlayerPrefs.GetInt(highScoreKey, 0);
        highScoreText.text = highScore.ToString(scoreFormat);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            //reset high score
            PlayerPrefs.SetInt(highScoreKey,0);
            PlayerPrefs.Save();
        }
    }

    public void AddPoints(int points)
    {
        score += points;
        scoreText.text = score.ToString(scoreFormat);
    }

    private void OnDisable()
    {
        if (score > highScore)
        {
            PlayerPrefs.SetInt(highScoreKey,score);
            PlayerPrefs.Save();
        }
    }
}
