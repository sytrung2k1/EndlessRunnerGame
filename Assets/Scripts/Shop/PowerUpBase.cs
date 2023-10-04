using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBase : MonoBehaviour
{
    public int currentLevel;

    private float[] maxTimeList = { 10f, 12f, 14f, 16f, 18f, 20f };
    private int[] costList = {100, 200, 500, 1000, 2000 };

    public void Created()
    {
        currentLevel = 0;
    }

    public float GetCurrentMaxTime()
    {
        return maxTimeList[currentLevel];
    }

    public float GetNextMaxTime()
    {
        return maxTimeList[currentLevel + 1];
    }

    public int GetCost()
    {
        return costList[currentLevel];
    }

    public void Upgrade()
    {
        currentLevel++;
    }
}
