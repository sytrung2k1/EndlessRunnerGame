using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSingleRunMission : MissionBase
{
    public override void Created()
    {
        missionType = MissionType.CoinSingleRun;
        int[] maxValues = { 100, 150, 200, 250, 300 };
        int[] rewardValues = { 50, 100, 150, 200, 250 };

        int randomValue = RandomValue();

        max = maxValues[randomValue];
        reward = rewardValues[randomValue];
        progress = 0;
    }

    public override string GetMissionDescription()
    {
        return "Collect " + max + " coin in one run";
    }

    public override void RunStart()
    {
        progress = 0;
        player = FindObjectOfType<PlayerController>();
    }

    public override void Update()
    {
        if (player == null)
            return;
        currentProgress = (int)player.coins;
    }
}
