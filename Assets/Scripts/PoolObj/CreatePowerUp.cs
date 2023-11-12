using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CreatePowerUp : MyMonoBehaviour
{
    private PlayerController player;

    private float timer = 0;
    private float delay = 20f;
    private int currentPowerUp = 0;

    public Transform ObstaclesHolder;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();

        string randString = RandomPowerUp();
        int randomLane = Random.Range(-1, 2);
        Vector3 spawnPos = new Vector3(randomLane, 0, 150 + player.transform.position.z);

        for(int i = 0; i < ObstaclesHolder.transform.childCount; i++)
        {
            Transform child = ObstaclesHolder.transform.GetChild(i);

            if (child.gameObject.activeInHierarchy)
            {
                if(child.position.z > (spawnPos.z - 3) && child.position.z < (spawnPos.z + 3) && child.position.x == spawnPos.x)
                {
                    if (spawnPos.x == -1 || spawnPos.x == 1) spawnPos.x = 0;
                    else
                    {
                        int rand = Random.Range(0, 2);
                        if (rand == 0) spawnPos.x = -1;
                        else spawnPos.x = 1;
                    }
                    spawnPos.z += 5;
                    break;
                }
            }
        }
        
        Transform newPowerUp = PowerUpSpawner.Instance.Spawn(randString, spawnPos);
        if (newPowerUp == null) return;
        newPowerUp.gameObject.SetActive(true);
        newPowerUp.GetChild(0).gameObject.SetActive(true);
        currentPowerUp++;
    }

    private void FixedUpdate()
    {
        if (!player.CheckCanMove()) return;
        this.timer += Time.fixedDeltaTime;
        if (this.timer < this.delay) return;
        else
        {
            string randString = RandomPowerUp();
            int randomLane = Random.Range(-1, 2);
            Vector3 spawnPos = new Vector3(randomLane, 0, 150 + player.transform.position.z);

            for (int i = 0; i < ObstaclesHolder.transform.childCount; i++)
            {
                Transform child = ObstaclesHolder.transform.GetChild(i);

                if (child.gameObject.activeInHierarchy)
                {
                    if (child.position.z > (spawnPos.z - 3) && child.position.z < (spawnPos.z + 3) && child.position.x == spawnPos.x)
                    {
                        if (spawnPos.x == -1 || spawnPos.x == 1) spawnPos.x = 0;
                        else
                        {
                            int rand = Random.Range(0, 2);
                            if (rand == 0) spawnPos.x = -1;
                            else spawnPos.x = 1;
                        }
                        spawnPos.z += 5;
                        break;
                    }
                }
            }

            Transform newPowerUp = PowerUpSpawner.Instance.Spawn(randString, spawnPos);
            if (newPowerUp == null) return;
            newPowerUp.gameObject.SetActive(true);
            newPowerUp.GetChild(0).gameObject.SetActive(true);
            currentPowerUp++;
            this.timer = 0;
        }
    }

    private string RandomPowerUp()
    {
        int rand = Random.RandomRange(0, 5);
        if (rand == 0) return PowerUpSpawner.powerUpOne.ToString();
        else if (rand == 1) return PowerUpSpawner.powerUpTwo.ToString();
        else if (rand == 2) return PowerUpSpawner.powerUpThree.ToString();
        else if (rand == 3) return PowerUpSpawner.powerUpFour.ToString();
        else return PowerUpSpawner.powerUpFive.ToString();
    }
}
