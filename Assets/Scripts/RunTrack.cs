using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunTrack : MonoBehaviour
{
    public GameObject[] obstacles;
    public Vector2 numberOfObstacles;
    public GameObject coin;
    public Vector2 numberOfCoins;
    public GameObject[] powerUps;

    public List<GameObject> newObstacles;
    public List<GameObject> newCoins;
    public GameObject newPowerUps;

    // Start is called before the first frame update
    void Start()
    {
        int newNumberOfObstacles = (int)Random.Range(numberOfObstacles.x, numberOfObstacles.y);
        int newNumberOfCoins = (int)Random.Range(numberOfCoins.x, numberOfCoins.y);
        
        for (int i = 0; i < newNumberOfObstacles; i++)
        {
            newObstacles.Add(Instantiate(obstacles[Random.Range(0, obstacles.Length)], transform));
            newObstacles[i].SetActive(false);
        }

        for (int i = 0; i < newNumberOfCoins; i++)
        {
            newCoins.Add(Instantiate(coin, transform));
            newCoins[i].SetActive(false);
        }

        newPowerUps = Instantiate(powerUps[Random.Range(0, powerUps.Length)], transform);
        newPowerUps.SetActive(false);

        PositionateObstacles();
        PositionateCoins();
        PositionatePowerUps();
    }

    void PositionateObstacles()
    {
        for (int i = 0; i < newObstacles.Count; i++)
        {
            float posZMin = (216f / newObstacles.Count) + (216f / newObstacles.Count) * i;
            float posZMax = (216f / newObstacles.Count) + (216f / newObstacles.Count) * i + 1;
            newObstacles[i].transform.localPosition = new Vector3(0, 0, Random.Range(posZMin, posZMax));
            newObstacles[i].SetActive(true);
            if(newObstacles[i].GetComponent<ChangeLane>() != null)
            {
                newObstacles[i].GetComponent<ChangeLane>().PositionLane();
            }
        }
    }

    void PositionateCoins()
    {
        float minZPos = 10f;
        for (int i = 0; i < newCoins.Count; i++)
        {
            float maxZPos = minZPos + 7f;
            float randomZPos = Random.Range(minZPos, maxZPos);
            newCoins[i].transform.localPosition = new Vector3(transform.position.x, transform.position.y, randomZPos);
            newCoins[i].SetActive(true);
            newCoins[i].GetComponent<ChangeLane>().PositionLane();
            minZPos = randomZPos + 2f;
        }
    }

    void PositionatePowerUps()
    {
        newPowerUps.transform.localPosition = new Vector3(0, 0, Random.Range(50f, 150f));
        newPowerUps.SetActive(true);
        newPowerUps.GetComponent<ChangeLane>().PositionLane();
    }
}
