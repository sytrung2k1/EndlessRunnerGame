using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveMagnetPower : MonoBehaviour
{
    private PlayerController player;
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        MagnetPower.MagnetEnable = 1;
        player.remainingPowerTimeMagnet = GameManager.gameManager.GetPowerUpBase(0).GetCurrentMaxTime();
        gameObject.SetActive(false);
    }
}
