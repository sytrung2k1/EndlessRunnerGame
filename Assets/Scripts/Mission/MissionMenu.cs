using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionMenu : MonoBehaviour
{
    public Text[] missionDescription, missionReward, missionProgress;
    public GameObject[] rewardButton;
    public Text coinsText;

    public void UpdateCoins(int coins)
    {
        coinsText.text = coins.ToString();
    }

    private void Start()
    {
        SetMission();
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

    public void SetMission()
    {
        for (int i = 0; i < 4; i++)
        {
            MissionBase mission = GameManager.gameManager.GetMission(i);
            missionDescription[i].text = mission.GetMissionDescription();
            missionReward[i].text = "Reward: " + mission.reward;
            missionProgress[i].text = mission.progress + mission.currentProgress + " / " + mission.max;
            if (mission.GetMissionComplete())
            {
                rewardButton[i].SetActive(true);
            }
        }

        GameManager.gameManager.Save();
    }

    public void GetReward(int missionIndex)
    {
        GameManager.gameManager.coins += GameManager.gameManager.GetMission(missionIndex).reward;
        UpdateCoins(GameManager.gameManager.coins);
        rewardButton[missionIndex].SetActive(false);
        GameManager.gameManager.GenerateMission(missionIndex);
    }
}
