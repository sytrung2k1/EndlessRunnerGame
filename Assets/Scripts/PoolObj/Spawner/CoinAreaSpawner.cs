using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinAreaSpawner : Spawner
{
    private static CoinAreaSpawner instance;
    public static CoinAreaSpawner Instance { get { return instance; } }

    public static string coinArea1 = "CoinArea1";
    public static string coinArea2 = "CoinArea2";
    public static string coinArea3 = "CoinArea3";

    protected override void Awake()
    {
        base.Awake();
        if (CoinAreaSpawner.instance != null) Debug.LogError("Only 1 CoinAreaSpawner allow to exist");
        CoinAreaSpawner.instance = this;
    }
}
