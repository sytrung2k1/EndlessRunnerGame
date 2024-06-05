using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pausePanel;

    public void Pause()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void Continue()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void Exit()
    {
        Time.timeScale = 1;
        PlayerController.SetGameOver();
        SceneManager.LoadScene("StartScene");
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
