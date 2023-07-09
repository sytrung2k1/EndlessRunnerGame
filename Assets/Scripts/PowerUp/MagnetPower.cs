using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetPower : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    private PlayerController player;
    public static int MagnetEnable = -1;
    private float remainingPowerTime = 0f;

    public float maxPowerTime = 10f;
    private UIManager uIManager;
    public GameObject magnetParticles;

    // Start is called before the first frame update
    void Start()
    {
        uIManager = FindObjectOfType<UIManager>();
        player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (MagnetEnable == 1)
        {
            if (!uIManager.magnetImage.activeInHierarchy)
            {
                uIManager.magnetImage.SetActive(true);
                remainingPowerTime = maxPowerTime;
                magnetParticles.SetActive(true);
            }

            if(remainingPowerTime > 0)
            {
                transform.position = Player.transform.position;
                remainingPowerTime -= Time.deltaTime;
                uIManager.UpdateMagnetTime(Mathf.CeilToInt(remainingPowerTime));
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
