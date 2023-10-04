using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetPower : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    private PlayerController player;
    public static int MagnetEnable = -1;

    private float maxPowerTime;
    
    private UIManager uIManager;
    public GameObject magnetParticles;

    // Start is called before the first frame update
    void Start()
    {
        uIManager = FindObjectOfType<UIManager>();
        player = FindObjectOfType<PlayerController>();

        maxPowerTime = GameManager.gameManager.GetPowerUpBase(0).GetCurrentMaxTime();
    }

    // Update is called once per frame
    void Update()
    {
        if (MagnetEnable == 1)
        {
            if (!uIManager.magnetImage.activeInHierarchy)
            {
                uIManager.magnetImage.SetActive(true);
                player.remainingPowerTimeMagnet = maxPowerTime;
                magnetParticles.SetActive(true);
            }

            if(player.remainingPowerTimeMagnet > 0)
            {
                transform.position = Player.transform.position;
                player.remainingPowerTimeMagnet -= Time.deltaTime;
                uIManager.UpdateMagnetTime(Mathf.CeilToInt(player.remainingPowerTimeMagnet));
            }
            else
            {
                magnetParticles.SetActive(false);
                MagnetEnable = -1;
                uIManager.magnetImage.SetActive(false);
            }

            if (player.GetGameOver())
            {
                magnetParticles.SetActive(false);
                MagnetEnable = -1;
                uIManager.magnetImage.SetActive(false);
            }
        }
    }
}
