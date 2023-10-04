using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincibilityPower : MonoBehaviour
{
    private UIManager uIManager;
    private PlayerController player;

    public static int InvincibilityEnable = -1;
    public GameObject invincibilityParticles;

    private float maxPowerTime;

    // Start is called before the first frame update
    void Start()
    {
        uIManager = FindObjectOfType<UIManager>();
        player = FindObjectOfType<PlayerController>();

        maxPowerTime = GameManager.gameManager.GetPowerUpBase(1).GetCurrentMaxTime();
    }

    // Update is called once per frame
    void Update()
    {
        if(InvincibilityEnable == 1)
        {
            if (!uIManager.invincibilityImage.activeInHierarchy)
            {
                uIManager.invincibilityImage.SetActive(true);
                player.remainingPowerTimeInvincibility = maxPowerTime;
                invincibilityParticles.SetActive(true);
            }

            if (player.remainingPowerTimeInvincibility > 0)
            {
                player.SetInvincibility(true);
                player.remainingPowerTimeInvincibility -= Time.deltaTime;
                uIManager.UpdateInvincibilityTime(Mathf.CeilToInt(player.remainingPowerTimeInvincibility));
            }
            else
            {
                player.SetInvincibility(false);
                invincibilityParticles.SetActive(false);
                InvincibilityEnable = -1;
                uIManager.invincibilityImage.SetActive(false);
            }

            if (player.GetGameOver())
            {
                player.SetInvincibility(false);
                invincibilityParticles.SetActive(false);
                InvincibilityEnable = -1;
                uIManager.invincibilityImage.SetActive(false);
            }
        }
    }
}
