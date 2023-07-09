using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraLifePower : MonoBehaviour
{
    private UIManager uIManager;
    private PlayerController player;
    public GameObject extraLifeParticles;
    public static int ExtraLifeEnable = -1;

    // Start is called before the first frame update
    void Start()
    {
        uIManager = FindObjectOfType<UIManager>();
        player = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        if(ExtraLifeEnable == 1)
        {
            int life = player.GetCurrentLife();
            if (life < 3)
            {
                life++;
            }
            player.SetCurrentLife(life);
            uIManager.UpdateLives(life);
            extraLifeParticles.SetActive(true);

            ExtraLifeEnable = -1;
            StartCoroutine(DisableExtraLife(5f));
        }
    }

    IEnumerator DisableExtraLife(float timer)
    {
        yield return new WaitForSeconds(timer);
        extraLifeParticles.SetActive(false);
    }
}
