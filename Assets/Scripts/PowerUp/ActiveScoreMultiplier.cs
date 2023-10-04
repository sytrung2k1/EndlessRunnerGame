using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveScoreMultiplier : MonoBehaviour
{
    private PlayerController player;
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        ScoreMultiplierPower.ScoreMultiplierEnable = 1;
        player.remainingPowerTimeScoreMultiplier = GameManager.gameManager.GetPowerUpBase(2).GetCurrentMaxTime();
        gameObject.SetActive(false);
    }
}
