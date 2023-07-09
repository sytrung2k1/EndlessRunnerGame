using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public Text coinsText;
    public Text bestScore;
    public static Menu menu;

    public void Start()
    {
        UpdateBestScore(GameManager.gameManager.highScore);
        UpdateCoins(GameManager.gameManager.coins);
    }
    public void UpdateCoins(int coins)
    {
        coinsText.text = coins.ToString();
    }

    public void UpdateBestScore(int score)
    {
        bestScore.text = "Best Score: " + score;
    }

    public void StartRun()
    {
        GameManager.gameManager.StartRun();
    }

    public void Mission()
    {
        GameManager.gameManager.Mission();
    }
}
