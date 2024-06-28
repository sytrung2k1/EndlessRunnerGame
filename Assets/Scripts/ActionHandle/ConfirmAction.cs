using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ConfirmAction : MonoBehaviour
{
    public static string LR_Action;
    public static string JD_Action;
    private PlayerController playerController;
    private bool isPause = false;
    private int resumeCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        playerController = FindObjectOfType<PlayerController>();

        string data = Communication.communication.data;
        //Check start game, if not start ready, if start stop, action, nulls

        if (data == "")
        {
            LR_Action = "";
            JD_Action = "";
        }
        else if (data == "READY")
        {
            GameManager.gameManager.readyControl = true;
            Communication.communication.SendData("OK");
        }
        else if (data == "STOP" && GameManager.gameManager.controlByCamera && playerController != null)
        {
            // Pause Game
            if (playerController.GetGameStart())
            {
                isPause = true;
                GameManager.gameManager.OnPauseMenu();
            }
        }
        else
        {
            if (isPause == false)
            {
                string[] actions = data.Split(';');
                if (actions.Length >= 2) // Kiểm tra độ dài của mảng
                {
                    /*print(actions[0] + "     " + actions[1]);*/
                    LR_Action = actions[0];
                    JD_Action = actions[1];
                }
            }
            else
            {
                resumeCount++;
                if (resumeCount == 50)
                {
                    isPause = false;
                    GameManager.gameManager.OffPauseMenu();
                    resumeCount = 0;
                }
            }
        }
    }
}
