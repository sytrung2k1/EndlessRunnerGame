using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighJumpPower : MonoBehaviour
{
    private PlayerController player;
    private UIManager uIManager;

    public static int HighJumpEnable = -1;
    public float maxPowerTime = 10f;
    public GameObject HighJumpParticles;

    private float remainingPowerTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        uIManager = FindObjectOfType<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (HighJumpEnable == 1)
        {
            if (!uIManager.highJumpImage.activeInHierarchy)
            {
                uIManager.highJumpImage.SetActive(true);
                remainingPowerTime = maxPowerTime;
                HighJumpParticles.SetActive(true);
            }

            if (remainingPowerTime > 0)
            {
                player.SetJumpHeight(3);
                remainingPowerTime -= Time.deltaTime;
                uIManager.UpdateHighJumpTime(Mathf.CeilToInt(remainingPowerTime));
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
