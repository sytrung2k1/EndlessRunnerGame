using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesSpawner : Spawner
{
    private static ObstaclesSpawner instance;
    public static ObstaclesSpawner Instance { get { return instance; } }

    public static string obstaclesOne_1 = "ObstacleBin";
    public static string obstaclesOne_2 = "ObstacleRoadworksBarrier";
    public static string obstaclesOne_3 = "ObstacleRoadworksCone";
    public static string obstaclesOne_4 = "ObstacleWheelyBin";
    public static string obstaclesTwo_1 = "ObstacleHighBarrier";
    public static string obstaclesTwo_2 = "ObstacleLowBarrier";

    protected override void Awake()
    {
        base.Awake();
        if (ObstaclesSpawner.instance != null) Debug.LogError("Only 1 ObstaclesSpawner allow to exist");
        ObstaclesSpawner.instance = this;
    }
}
