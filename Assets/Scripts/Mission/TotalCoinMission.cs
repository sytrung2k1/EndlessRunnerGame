using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotalCoinMission : MissionBase
{
    public override void Created()
    {
        missionType = MissionType.TotalCoin;
        int[] maxValues = { 500, 1000, 1500, 2000, 2500 };
        int[] rewardValues = { 250, 500, 750, 1000, 1250 };

        int randomValue = RandomValue();
        
        max = maxValues[randomValue];
        reward = rewardValues[randomValue];
        progress = 0;
    }

    public override string GetMissionDescription()
    {
        return "Collect " + max + " coin in total";
    }

    public override void RunStart()
    {
        progress += currentProgress;
        player = FindObjectOfType<PlayerController>();
    }

    public override void Update()
    {
        if (player == null)
            return;
        currentProgress = (int)player.coins;
    }
}
