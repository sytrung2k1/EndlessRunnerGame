using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlByCamera : MonoBehaviour
{
    public Toggle toggle;
    // Start is called before the first frame update
    void Start()
    {
        toggle.isOn = GameManager.gameManager.controlByCamera;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
