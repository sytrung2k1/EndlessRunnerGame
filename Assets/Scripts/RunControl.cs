using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEngine;

public class RunControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.gameManager.appPython == false)
        {
            RunPython();
            GameManager.gameManager.appPython = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void RunPython()
    {
        ProcessStartInfo start = new ProcessStartInfo();
        string exePath = Path.Combine(Application.dataPath, "../controller/commu_threading.exe");
        start.FileName = exePath;
        /*start.FileName = @"C:\Users\DMX\PycharmProjects\GameControllerByCamera\output\commu_threading\commu_threading.exe"; // Đường dẫn đến file .exe*/
        start.UseShellExecute = false; // Không sử dụng shell để thực thi
        start.RedirectStandardOutput = true; // Chuyển hướng đầu ra chuẩn
        Process process = Process.Start(start);
        /*
        using (Process process = Process.Start(start))
        {
            using (StreamReader reader = process.StandardOutput)
            {
                string result = reader.ReadToEnd(); // Đọc kết quả
                //Debug.Log(result);
            }
        }
        */
    }
}
