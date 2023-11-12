using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCoin : MyMonoBehaviour
{
    private PlayerController player;

    private float timer = 0;
    private float delay = 0.3f;
    private int currentCoin = 0;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();

        for (int i = 0; i < 20; i++)
        {
            int randomLane = Random.Range(-1, 2);
            Vector3 spawnPos = new Vector3(randomLane, 0, 20 + 3*i);
            Transform newCoin = CoinSpawner.Instance.Spawn(CoinSpawner.coinOne, spawnPos);
            if (newCoin == null) return;
            newCoin.gameObject.SetActive(true);
            newCoin.GetChild(0).gameObject.SetActive(true);
            newCoin.GetChild(0).GetChild(0).gameObject.SetActive(true);
            currentCoin++;
        }
    }

    private void FixedUpdate()
    {
        if (!player.CheckCanMove()) return;
        this.timer += Time.fixedDeltaTime;
        if (this.timer < this.delay) return;
        else
        {
            int randomLane = Random.Range(-1, 2);
            Vector3 spawnPos = new Vector3(randomLane, 0, player.transform.position.z + 80);
            Transform newCoin = CoinSpawner.Instance.Spawn(CoinSpawner.coinOne, spawnPos);
            if (newCoin == null) return;
            newCoin.gameObject.SetActive(true);
            newCoin.GetChild(0).gameObject.SetActive(true);
            newCoin.GetChild(0).GetChild(0).gameObject.SetActive(true);
            currentCoin++;
            this.timer = 0;
        }
    }
}
