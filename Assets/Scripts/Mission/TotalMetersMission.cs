using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotalMetersMission : MissionBase
{
    public override void Created()
    {
        missionType = MissionType.TotalMeters;
        int[] maxValues = { 5000, 10000, 15000, 20000, 25000 };
        int[] rewardValues = { 1000, 2000, 3000, 4000, 5000 };

        int randomValue = RandomValue();

        max = maxValues[randomValue];
        reward = rewardValues[randomValue];
        progress = 0;
    }

    public override string GetMissionDescription()
    {
        return "Run " + max + "m in total";
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
        currentProgress = (int)player.distance;
    }
}
