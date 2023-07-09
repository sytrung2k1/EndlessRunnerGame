using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleRunMission : MissionBase
{
    public override void Created()
    {
        missionType = MissionType.SingleRun;
        int[] maxValues = { 1000, 1500, 2000, 2500, 3000 };
        int[] rewardValues = { 200, 300, 400, 500, 600 };
        
        int randomValue = RandomValue();
        
        max = maxValues[randomValue];
        reward = rewardValues[randomValue];
        progress = 0;
    }

    public override string GetMissionDescription()
    {
        return "Run " + max + "m in one run";
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
        progress = (int)player.score;
    }
}
