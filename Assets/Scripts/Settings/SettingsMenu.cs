using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenu : MonoBehaviour
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

    public void ControlByCamera(bool tog)
    {
        GameManager.gameManager.controlByCamera = tog;
    }

    public void OnMusicChange(float value)
    {
        GameManager.gameManager.music = value;
    }

    public void OnSoundChange(float value)
    {
        GameManager.gameManager.sound = value;
    }
}
