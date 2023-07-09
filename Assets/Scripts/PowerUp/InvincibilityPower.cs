using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincibilityPower : MonoBehaviour
{
    private UIManager uIManager;
    private PlayerController player;

    public static int InvincibilityEnable = -1;
    public float maxPowerTime = 10f;
    public GameObject invincibilityParticles;

    private float remainingPowerTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        uIManager = FindObjectOfType<UIManager>();
        player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(InvincibilityEnable == 1)
        {
            if (!uIManager.invincibilityImage.activeInHierarchy)
            {
                uIManager.invincibilityImage.SetActive(true);
                remainingPowerTime = maxPowerTime;
                invincibilityParticles.SetActive(true);
            }

            if (remainingPowerTime > 0)
            {
                player.SetInvincibility(true);
                remainingPowerTime -= Time.deltaTime;
                uIManager.UpdateInvincibilityTime(Mathf.CeilToInt(remainingPowerTime));
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
