using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateObstacles : MyMonoBehaviour
{
    private PlayerController player;

    private float timer = 0;
    private float delay = 2f;
    private int currentObstacles = 0;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();

        for (int i = 0; i < 10; i++)
        {
            string randString = RandomObstacles();
            int randomLane = Random.Range(-1, 2);
            if (randString == ObstaclesSpawner.obstaclesTwo_1.ToString() || randString == ObstaclesSpawner.obstaclesTwo_2.ToString())
            {
                randomLane = 0;
            }
            Vector3 spawnPos = new Vector3(randomLane, 0, 15*(i+1) + 15);
            Transform newObstacles = ObstaclesSpawner.Instance.Spawn(randString, spawnPos);
            if (newObstacles == null) return;
            newObstacles.gameObject.SetActive(true);
            currentObstacles++;
        }
    }

    private void FixedUpdate()
    {
        this.timer += Time.fixedDeltaTime;
        if (this.timer < this.delay) return;
        else
        {
            string randString = RandomObstacles();
            int randomLane = Random.Range(-1, 2);
            if (randString == ObstaclesSpawner.obstaclesTwo_1.ToString() || randString == ObstaclesSpawner.obstaclesTwo_2.ToString())
            {
                randomLane = 0;
            }
            Vector3 spawnPos = new Vector3(randomLane, 0, player.transform.position.z + 175);
            Transform newObstacles = ObstaclesSpawner.Instance.Spawn(randString, spawnPos);
            if (newObstacles == null) return;
            newObstacles.gameObject.SetActive(true);
            currentObstacles++;
            this.timer = 0;
        }
    }

    private string RandomObstacles()
    {
        int rand = Random.RandomRange(0, 6);
        if (rand == 0) return ObstaclesSpawner.obstaclesOne_1.ToString();
        else if (rand == 1) return ObstaclesSpawner.obstaclesOne_2.ToString();
        else if (rand == 2) return ObstaclesSpawner.obstaclesOne_3.ToString();
        else if (rand == 3) return ObstaclesSpawner.obstaclesOne_4.ToString();
        else if (rand == 4) return ObstaclesSpawner.obstaclesTwo_1.ToString();
        else return ObstaclesSpawner.obstaclesTwo_2.ToString();
    }
}
