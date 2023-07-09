using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MissionType
{
    SingleRun, TotalMeters, CoinSingleRun, TotalCoin
}

public abstract class MissionBase : MonoBehaviour
{
    public int max;
    public int progress;
    public int reward;
    public PlayerController player;
    public int currentProgress;
    public MissionType missionType;

    public int RandomValue()
    {
        int randomMaxValue = Random.Range(0, 15);
        int random;
        if (randomMaxValue < 5)
            random = 0;
        else if (randomMaxValue < 9)
            random = 1;
        else if (randomMaxValue < 12)
            random = 2;
        else if (randomMaxValue < 14)
            random = 3;
        else
            random = 4;
        return random;
    }

    public abstract void Created();
    public abstract string GetMissionDescription();
    public abstract void RunStart();
    public abstract void Update();

    public bool GetMissionComplete()
    {
        if((progress + currentProgress) >= max)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}