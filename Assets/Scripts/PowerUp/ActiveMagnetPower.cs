using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveMagnetPower : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        MagnetPower.MagnetEnable = 1;
        gameObject.SetActive(false);
    }
}
