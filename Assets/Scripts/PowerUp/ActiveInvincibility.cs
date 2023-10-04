using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveInvincibility : MonoBehaviour
{
    private PlayerController player;
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        InvincibilityPower.InvincibilityEnable = 1;
        player.remainingPowerTimeInvincibility = GameManager.gameManager.GetPowerUpBase(1).GetCurrentMaxTime();
        gameObject.SetActive(false);
    }
}
