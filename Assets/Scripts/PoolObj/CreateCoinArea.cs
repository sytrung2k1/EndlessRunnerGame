using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCoinArea : MonoBehaviour
{
    private PlayerController player;

    private float timer = 0;
    private float delay = 3f;

    public Transform ObstaclesHolder;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();

        for(int i = 0; i < 4; i++)
        {
            string randString = RandomCoinArea();
            int randomLane = Random.Range(-1, 2);
            Vector3 spawnPos = new Vector3(randomLane, 0, 10 + 20 * i);

            for(int j = 0; j < ObstaclesHolder.transform.childCount; j++)
            {
                Transform child = ObstaclesHolder.transform.GetChild(j);

                if (child.gameObject.activeInHierarchy)
                {
                    if (child.position.z > (spawnPos.z - 3) && child.position.z < (spawnPos.z + 13) && child.position.x == spawnPos.x)
                    {
                        if (spawnPos.x == -1 || spawnPos.x == 1) spawnPos.x = 0;
                        else
                        {
                            int rand = Random.Range(0, 2);
                            if(rand == 0) spawnPos.x = -1;
                            else spawnPos.x = 1;
                        }
                        break;
                    }
                }
            }

            Transform newCoinArea = CoinAreaSpawner.Instance.Spawn(randString, spawnPos);
            if (newCoinArea == null) return;
            newCoinArea.gameObject.SetActive(true);
        }
    }

    private void FixedUpdate()
    {
        if (!player.CheckCanMove()) return;
        this.timer += Time.fixedDeltaTime;
        if (this.timer < this.delay) return;
        else
        {
            string randString = RandomCoinArea();
            int randomLane = Random.Range(-1, 2);
            Vector3 spawnPos = new Vector3(randomLane, 0, player.transform.position.z + 70);

            for (int j = 0; j < ObstaclesHolder.transform.childCount; j++)
            {
                Transform child = ObstaclesHolder.transform.GetChild(j);

                if (child.gameObject.activeInHierarchy)
                {
                    if (child.position.z > (spawnPos.z - 3) && child.position.z < (spawnPos.z + 13) && child.position.x == spawnPos.x)
                    {
                        if (spawnPos.x == -1 || spawnPos.x == 1) spawnPos.x = 0;
                        else
                        {
                            int rand = Random.Range(0, 2);
                            if (rand == 0) spawnPos.x = -1;
                            else spawnPos.x = 1;
                        }
                        break;
                    }
                }
            }

            Transform newCoinArea = CoinAreaSpawner.Instance.Spawn(randString, spawnPos);
            if (newCoinArea == null) return;
            newCoinArea.gameObject.SetActive(true);
            this.timer = 0;
        }
    }

    private string RandomCoinArea()
    {
        int rand = Random.Range(0, 3);
        if (rand == 0) return CoinAreaSpawner.coinArea1.ToString();
        if (rand == 1) return CoinAreaSpawner.coinArea2.ToString();
        else return CoinAreaSpawner.coinArea3.ToString();

    }
}
