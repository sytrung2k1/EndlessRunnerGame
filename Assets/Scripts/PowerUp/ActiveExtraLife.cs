using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveExtraLife : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        ExtraLifePower.ExtraLifeEnable = 1;
        gameObject.SetActive(false);
    }
}
