using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : Spawner
{
    private static CoinSpawner instance;
    public static CoinSpawner Instance { get { return instance; } }

    public static string coinOne = "Coin";

    protected override void Awake()
    {
        base.Awake();
        if (CoinSpawner.instance != null) Debug.LogError("Only 1 CoinSpawner allow to exist");
        CoinSpawner.instance = this;
    }
}
