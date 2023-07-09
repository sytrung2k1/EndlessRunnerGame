using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveScoreMultiplier : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        ScoreMultiplierPower.ScoreMultiplierEnable = 1;
        gameObject.SetActive(false);
    }
}
