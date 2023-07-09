using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Random = UnityEngine.Random;

[Serializable]
public class PlayerData
{
    public int highScore;
    public int coins;
    public int[] max;
    public int[] progress;
    public int[] currentProgress;
    public int[] reward;
    public string[] missionType;
}

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    public int coins;
    public int highScore;

    private string filePath;
    private MissionBase[] missions;

    private void Awake()
    {
        if(gameManager == null)
        {
            gameManager = this;
        }
        else if(gameManager!= this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        filePath = Application.persistentDataPath + "/playerInfo.dat";

        missions = new MissionBase[2];

        if (File.Exists(filePath))
        {
            Load();
        }
        else
        {
            for (int i = 0; i < missions.Length; i++)
            {
                GameObject newMission = new GameObject("Mission" + i);
                newMission.transform.SetParent(transform);
                MissionType[] missionType = { MissionType.SingleRun, MissionType.TotalMeters, MissionType.CoinSingleRun, MissionType.TotalCoin };
                int randomType = Random.Range(0, missionType.Length);
                if(randomType == (int)MissionType.SingleRun)
                {
                    missions[i] = newMission.AddComponent<SingleRunMission>();
                }
                else if (randomType == (int)MissionType.TotalMeters)
                {
                    missions[i] = newMission.AddComponent<TotalMetersMission>();
                }
                else if (randomType == (int)MissionType.CoinSingleRun)
                {
                    missions[i] = newMission.AddComponent<CoinSingleRunMission>();
                }
                else if (randomType == (int)MissionType.TotalCoin)
                {
                    missions[i] = newMission.AddComponent<TotalCoinMission>();
                }
                missions[i].Created();
            }
        }
    }

    public void Save()
    {
        BinaryFormatter binary = new BinaryFormatter();
        FileStream file = File.Create(filePath);

        PlayerData data = new PlayerData();

        data.highScore = highScore;
        data.coins = coins;
        data.max = new int[2];
        data.progress = new int[2];
        data.reward = new int[2];
        data.currentProgress = new int[2];
        data.missionType = new string[2];

        for (int i = 0; i < 2; i++)
        {
            data.max[i] = missions[i].max;
            data.progress[i] = missions[i].progress;
            data.reward[i] = missions[i].reward;
            data.currentProgress[i] = missions[i].currentProgress;
            data.missionType[i] = missions[i].missionType.ToString();
        }

        binary.Serialize(file, data);
        file.Close();
    }

    void Load()
    {
        BinaryFormatter binary = new BinaryFormatter();
        FileStream file = File.Open(filePath, FileMode.Open);

        PlayerData data = (PlayerData)binary.Deserialize(file);
        file.Close();

        highScore = data.highScore;
        coins = data.coins;

        for (int i = 0; i < 2; i++)
        {
            GameObject newMission = new GameObject("Mission" + i);
            newMission.transform.SetParent(transform);
            if(data.missionType[i] == MissionType.SingleRun.ToString())
            {
                missions[i] = newMission.AddComponent<SingleRunMission>();
                missions[i].missionType = MissionType.SingleRun;
            }
            else if (data.missionType[i] == MissionType.TotalMeters.ToString())
            {
                missions[i] = newMission.AddComponent<TotalMetersMission>();
                missions[i].missionType = MissionType.TotalMeters;
            }
            else if(data.missionType[i] == MissionType.CoinSingleRun.ToString())
            {
                missions[i] = newMission.AddComponent<CoinSingleRunMission>();
                missions[i].missionType = MissionType.CoinSingleRun;
            }
            else if(data.missionType[i] == MissionType.TotalCoin.ToString())
            {
                missions[i] = newMission.AddComponent<TotalCoinMission>();
                missions[i].missionType = MissionType.TotalCoin;
            }

            missions[i].max = data.max[i];
            missions[i].progress = data.progress[i];
            missions[i].reward = data.reward[i];
            missions[i].currentProgress = data.currentProgress[i];
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartRun()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void EndRun()
    {
        SceneManager.LoadScene("StartScene");
    }

    public void Mission()
    {
        SceneManager.LoadScene("MissionScene");
    }

    public MissionBase GetMission(int index)
    {
        return missions[index];
    }

    public void StartMissions()
    {
        for (int i = 0; i < 2; i++)
        {
            missions[i].RunStart();
        }
    }

    public void GenerateMission(int i)
    {
        Destroy(missions[i].gameObject);

        GameObject newMission = new GameObject("Mission" + i);
        newMission.transform.SetParent(transform);
        MissionType[] missionType = { MissionType.SingleRun, MissionType.TotalMeters, MissionType.CoinSingleRun, MissionType.TotalCoin };
        int randomType = Random.Range(0, missionType.Length);
        if (randomType == (int)MissionType.SingleRun)
        {
            missions[i] = newMission.AddComponent<SingleRunMission>();
        }
        else if (randomType == (int)MissionType.TotalMeters)
        {
            missions[i] = newMission.AddComponent<TotalMetersMission>();
        }
        else if (randomType == (int)MissionType.CoinSingleRun)
        {
            missions[i] = newMission.AddComponent<CoinSingleRunMission>();
        }
        else if (randomType == (int)MissionType.TotalCoin)
        {
            missions[i] = newMission.AddComponent<TotalCoinMission>();
        }
        missions[i].Created();

        FindObjectOfType<MissionMenu>().SetMission();
    }
}
