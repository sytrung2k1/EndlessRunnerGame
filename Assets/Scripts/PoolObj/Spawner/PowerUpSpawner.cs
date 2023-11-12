using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : Spawner
{
    private static PowerUpSpawner instance;
    public static PowerUpSpawner Instance { get { return instance; } }

    public static string powerUpOne = "CoinMagnet";
    public static string powerUpTwo = "ExtraLife";
    public static string powerUpThree = "HighJump";
    public static string powerUpFour = "Invincibilty";
    public static string powerUpFive = "ScoreMultiplier";

    protected override void Awake()
    {
        base.Awake();
        if(PowerUpSpawner.instance != null ) Debug.LogError("Only 1 PowerUpSpawner allow to exist");
        PowerUpSpawner.instance = this;
    }
}
