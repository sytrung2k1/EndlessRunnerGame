using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreMultiplierPower : MonoBehaviour
{
    private PlayerController player;
    private UIManager uIManager;

    public static int ScoreMultiplierEnable = -1;
    public float maxPowerTime = 10f;
    public GameObject MultiplierParticles;

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
        if(ScoreMultiplierEnable == 1)
        {
            if (!uIManager.scoreMultiplierImage.activeInHierarchy)
            {
                uIManager.scoreMultiplierImage.SetActive(true);
                remainingPowerTime = maxPowerTime;
                MultiplierParticles.SetActive(true);
            }

            if(remainingPowerTime > 0)
            {
                player.SetScoreMultiplier(2);
                remainingPowerTime -= Time.deltaTime;
                uIManager.UpdateScoreMultiplierTime(Mathf.CeilToInt(remainingPowerTime));
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
