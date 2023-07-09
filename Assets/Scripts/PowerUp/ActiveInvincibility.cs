using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveInvincibility : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        InvincibilityPower.InvincibilityEnable = 1;
        gameObject.SetActive(false);
    }
}
