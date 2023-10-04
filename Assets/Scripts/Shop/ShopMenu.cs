using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopMenu : MonoBehaviour
{
    public Text[] currentLevel, currentMaxTime, nextLevel, nextMaxTime, cost;
    public GameObject[] upgradeButton;
    public GameObject[] nextImgs;
    public Text coinsText;

    public void UpdateCoins(int coins)
    {
        coinsText.text = coins.ToString();
    }

    public void UpdateBtnColor(int i, Color color)
    {
        Button button = upgradeButton[i].GetComponent<Button>();
        ColorBlock cb = button.colors;
        cb.normalColor = color;
        button.colors = cb;
    }

    // Start is called before the first frame update
    void Start()
    {
        SetPowerUpData();
        UpdateCoins(GameManager.gameManager.coins);
    }

    public void StartRun()
    {
        GameManager.gameManager.StartRun();
    }

    public void BackToMenu()
    {
        GameManager.gameManager.EndRun();
    }

    public void SetPowerUpData()
    {
        for (int i = 0; i < 4; i++)
        {
            PowerUpBase powerUp = GameManager.gameManager.GetPowerUpBase(i);
            currentLevel[i].text = "Level " + powerUp.currentLevel;
            currentMaxTime[i].text = "Max Time: " + (int)powerUp.GetCurrentMaxTime();
            
            if(powerUp.currentLevel < 5)
            {
                nextLevel[i].text = "Level " + (powerUp.currentLevel + 1);
                nextMaxTime[i].text = "Max Time: " + (int)powerUp.GetNextMaxTime();
                cost[i].text = "Cost: " + powerUp.GetCost();
                if (GameManager.gameManager.coins >= powerUp.GetCost())
                {
                    UpdateBtnColor(i, Color.white);
                }
                else
                {
                    UpdateBtnColor(i, new Color(102f / 255f, 229f / 255f, 214f / 255f));
                }
            }
            else
            {
                nextLevel[i].text = "";
                nextMaxTime[i].text = "";
                cost[i].text = "";
                nextImgs[i].SetActive(false);
                upgradeButton[i].SetActive(false);
            } 
        }

        GameManager.gameManager.Save();
    }

    public void UpGrade(int powerUpIndex)
    {
        if(GameManager.gameManager.coins >= GameManager.gameManager.GetPowerUpBase(powerUpIndex).GetCost()){
            GameManager.gameManager.coins -= GameManager.gameManager.GetPowerUpBase(powerUpIndex).GetCost();
            UpdateCoins(GameManager.gameManager.coins);
            GameManager.gameManager.GetPowerUpBase(powerUpIndex).Upgrade();
            SetPowerUpData();
        }
    }
}
