using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Image[] lifeHearts;
    public Text coinText;
    public GameObject gameOverPanel;
    public Text scoreText;
    public Text distanceText;
    public GameObject magnetImage;
    public Text magnetTime;
    public GameObject invincibilityImage;
    public Text invincibilityTime;
    public GameObject scoreMultiplierImage;
    public Text scoreMultiplierTime;
    public GameObject highJumpImage;
    public Text highJumpTime;

    public void UpdateLives(int lives)
    {
        for (int i = 0; i < lifeHearts.Length; i++)
        {
            if(lives > i)
            {
                lifeHearts[i].color = Color.white;
            }
            else
            {
                lifeHearts[i].color = Color.black;
            }
        }
    }

    public void UpdateCoinText(int coin)
    {
        coinText.text = coin.ToString();
    }

    public void UpdateScoreText(int score)
    {
        scoreText.text = "Score: " + score;
    }

    public void UpdateDistanceText(int distance)
    {
        distanceText.text = distance + "m";
    }

    public void UpdateMagnetTime(int timer)
    {
        magnetTime.text = timer.ToString();
    }

    public void UpdateInvincibilityTime(int timer)
    {
        invincibilityTime.text = timer.ToString();
    }

    public void UpdateScoreMultiplierTime(int timer)
    {
        scoreMultiplierTime.text = timer.ToString();
    }

    public void UpdateHighJumpTime(int timer)
    {
        highJumpTime.text = timer.ToString();
    }
}
