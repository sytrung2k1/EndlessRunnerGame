using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartRun()
    {
        GameManager.gameManager.StartRun();
    }

    public void BackToMenu()
    {
        GameManager.gameManager.EndRun();
    }
}
