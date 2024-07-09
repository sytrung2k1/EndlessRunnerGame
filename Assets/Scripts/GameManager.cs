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
    public int[] powerUpListLevels;
}

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    public int coins;
    public int highScore;
    [HideInInspector]
    public bool controlByCamera = false;
    [HideInInspector]
    public bool appPython = false;
    public bool readyControl = false;

    public GameObject loadingPanel;
    public GameObject pausePanel;

    private string filePath;
    private MissionBase[] missions;
    private PowerUpBase[] powerUps;

    [HideInInspector]
    public float music = 0.5f;
    [HideInInspector]
    public float sound = 0.5f;

    private void Awake()
    {
        if (gameManager == null)
        {
            gameManager = this;
        }
        else if (gameManager != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        filePath = Application.persistentDataPath + "/playerInfo.dat";

        missions = new MissionBase[4];
        powerUps = new PowerUpBase[4];

        if (File.Exists(filePath))
        {
            //Debug.Log(filePath);
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

            for (int i = 0; i < 4; i++)
            {
                GameObject powerUpObject = new GameObject("PowerUpObject");
                powerUps[i] = powerUpObject.AddComponent<PowerUpBase>(); ;
                powerUps[i].Created();
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
        data.max = new int[4];
        data.progress = new int[4];
        data.reward = new int[4];
        data.currentProgress = new int[4];
        data.missionType = new string[4];
        data.powerUpListLevels = new int[4];

        for (int i = 0; i < 4; i++)
        {
            data.max[i] = missions[i].max;
            data.progress[i] = missions[i].progress;
            data.reward[i] = missions[i].reward;
            data.currentProgress[i] = missions[i].currentProgress;
            data.missionType[i] = missions[i].missionType.ToString();
            
            data.powerUpListLevels[i] = powerUps[i].currentLevel;
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

        for (int i = 0; i < 4; i++)
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

            GameObject powerUpObject = new GameObject("PowerUpObject");
            powerUps[i] = powerUpObject.AddComponent<PowerUpBase>();
            powerUps[i].currentLevel = data.powerUpListLevels[i];
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Transform canvasTransform = GameObject.Find("Canvas").transform;
        Transform loadingTransform = canvasTransform.Find("Loading");
        loadingPanel = loadingTransform.gameObject;
        loadingPanel.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (readyControl)
        {
            loadingPanel = GameObject.Find("Loading");
            if (loadingPanel != null)
            {
                loadingPanel.SetActive(false);
            }
        }
    }

    public void OnPauseMenu()
    {
        Transform canvasMenuTransform = GameObject.Find("CanvasMenu").transform;
        Transform pauseMenuPanelTransform = canvasMenuTransform.Find("PauseMenuPanel");
        pausePanel = pauseMenuPanelTransform.gameObject;
        if (pausePanel != null)
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void OffPauseMenu()
    {
        Transform canvasMenuTransform = GameObject.Find("CanvasMenu").transform;
        Transform pauseMenuPanelTransform = canvasMenuTransform.Find("PauseMenuPanel");
        pausePanel = pauseMenuPanelTransform.gameObject;
        if (pausePanel != null)
        {
            pausePanel.SetActive(false);
            Time.timeScale = 1;
        }
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

    public void Shop()
    {
        SceneManager.LoadScene("ShopScene");
    }

    public void Settings()
    {
        SceneManager.LoadScene("SettingsScene");
    }

    public void Tutorial()
    {
        SceneManager.LoadScene("TutorialScene");
    }

    public MissionBase GetMission(int index)
    {
        return missions[index];
    }

    public PowerUpBase GetPowerUpBase(int i)
    {
        return powerUps[i];
    }

    public void StartMissions()
    {
        for (int i = 0; i < 4; i++)
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
