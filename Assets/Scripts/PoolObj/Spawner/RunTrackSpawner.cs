using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunTrackSpawner : Spawner
{
    private static RunTrackSpawner instance;
    public static RunTrackSpawner Instance { get { return instance; } }

    public static string runTrackOne = "RunTrack_1";
    public static string runTrackTwo = "RunTrack_2";
    public static string runTrackThree = "RunTrack_3";

    protected override void Awake()
    {
        base.Awake();
        if (RunTrackSpawner.instance != null) Debug.LogError("Only 1 RunTrackSpawner allow to exist");
        RunTrackSpawner.instance = this;
    }
}
