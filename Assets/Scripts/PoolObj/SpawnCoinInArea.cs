using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCoinInArea : MonoBehaviour
{
    public Transform StartPos;
    public Transform EndPos;

    private float Current_Coin_Pos;
    // Start is called before the first frame update

    private void OnEnable()
    {
        Current_Coin_Pos = StartPos.position.z;
        while (Current_Coin_Pos < EndPos.position.z)
        {
            Vector3 spawnPos = new Vector3(StartPos.position.x, StartPos.position.y, Current_Coin_Pos);
            Transform newCoin = CoinSpawner.Instance.Spawn(CoinSpawner.coinOne, spawnPos);
            if (newCoin == null) return;
            newCoin.gameObject.SetActive(true);
            newCoin.GetChild(0).localPosition = new Vector3(0, 0, 0);
            newCoin.GetChild(0).gameObject.SetActive(true);
            newCoin.GetChild(0).GetChild(0).localPosition = new Vector3(0, 0.5f, 0);
            newCoin.GetChild(0).GetChild(0).gameObject.SetActive(true);
            Current_Coin_Pos += 1.5f;
        }
    }
}
