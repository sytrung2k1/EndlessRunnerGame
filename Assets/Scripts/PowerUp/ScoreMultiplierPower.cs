using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreMultiplierPower : MonoBehaviour
{
    private PlayerController player;
    private UIManager uIManager;

    public static int ScoreMultiplierEnable = -1;
    public GameObject MultiplierParticles;

    private float maxPowerTime;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        uIManager = FindObjectOfType<UIManager>();

        maxPowerTime = GameManager.gameManager.GetPowerUpBase(2).GetCurrentMaxTime();
    }

    // Update is called once per frame
    void Update()
    {
        if(ScoreMultiplierEnable == 1)
        {
            if (!uIManager.scoreMultiplierImage.activeInHierarchy)
            {
                uIManager.scoreMultiplierImage.SetActive(true);
                player.remainingPowerTimeScoreMultiplier = maxPowerTime;
                MultiplierParticles.SetActive(true);
            }

            if(player.remainingPowerTimeScoreMultiplier > 0)
            {
                player.SetScoreMultiplier(2);
                player.remainingPowerTimeScoreMultiplier -= Time.deltaTime;
                uIManager.UpdateScoreMultiplierTime(Mathf.CeilToInt(player.remainingPowerTimeScoreMultiplier));
            }
            else
            {
                player.SetScoreMultiplier(1);
                ScoreMultiplierEnable = -1;
                MultiplierParticles.SetActive(false);
                uIManager.scoreMultiplierImage.SetActive(false);
            }

            if (player.GetGameOver())
            {
                player.SetScoreMultiplier(1);
                ScoreMultiplierEnable = -1;
                MultiplierParticles.SetActive(false);
                uIManager.scoreMultiplierImage.SetActive(false);
            }
        }
    }
}
