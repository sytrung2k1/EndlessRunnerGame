using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveHighJump : MonoBehaviour
{
    private PlayerController player;
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        transform.Rotate(new Vector3(0, 60, 0) * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        HighJumpPower.HighJumpEnable = 1;
        player.remainingPowerTimeHighJump = GameManager.gameManager.GetPowerUpBase(3).GetCurrentMaxTime();
        gameObject.SetActive(false);
    }
}
