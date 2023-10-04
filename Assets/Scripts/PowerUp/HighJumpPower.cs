using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighJumpPower : MonoBehaviour
{
    private PlayerController player;
    private UIManager uIManager;

    public static int HighJumpEnable = -1;
    public GameObject HighJumpParticles;

    private float maxPowerTime;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        uIManager = FindObjectOfType<UIManager>();

        maxPowerTime = GameManager.gameManager.GetPowerUpBase(3).GetCurrentMaxTime();
    }

    // Update is called once per frame
    void Update()
    {
        if (HighJumpEnable == 1)
        {
            if (!uIManager.highJumpImage.activeInHierarchy)
            {
                uIManager.highJumpImage.SetActive(true);
                player.remainingPowerTimeHighJump = maxPowerTime;
                HighJumpParticles.SetActive(true);
            }

            if (player.remainingPowerTimeHighJump > 0)
            {
                player.SetJumpHeight(3);
                player.remainingPowerTimeHighJump -= Time.deltaTime;
                uIManager.UpdateHighJumpTime(Mathf.CeilToInt(player.remainingPowerTimeHighJump));
            }
            else
            {
                player.SetJumpHeight(2);
                HighJumpEnable = -1;
                HighJumpParticles.SetActive(false);
                uIManager.highJumpImage.SetActive(false);
            }

            if (player.GetGameOver())
            {
                player.SetJumpHeight(2);
                HighJumpEnable = -1;
                HighJumpParticles.SetActive(false);
                uIManager.highJumpImage.SetActive(false);
            }
        }
    }
}
