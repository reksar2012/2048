using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTracker : MonoBehaviour {

    private int _score;
    public int Score
    {
        get
        {
            return _score;
        }
        set
        {
            _score = value;
            UpdateScore();
            
        }
    }
    public static ScoreTracker Instance;
    public Text resultScoreText;
    public Text scoreText;
    public Text hightScoreText;
    private void Awake()
    {
        Instance = this;
        if (!PlayerPrefs.HasKey("HightScore"))
        {
            PlayerPrefs.SetInt("HightScore", 0);
        }
        scoreText.text = "0";
        hightScoreText.text = PlayerPrefs.GetInt("HightScore").ToString();
    }
    private void UpdateScore()
    {
        string score= _score.ToString();
        scoreText.text = score;
        resultScoreText.text = score;
        if (PlayerPrefs.GetInt("HightScore") < _score)
        {
            PlayerPrefs.SetInt("HightScore", _score);
            hightScoreText.text = score;
        }
    }
}
