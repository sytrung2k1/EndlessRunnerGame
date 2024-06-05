using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Threading;

public class Communication : MonoBehaviour
{
    public static Communication communication;

    UdpClient udpServer;
    IPEndPoint endPoint;

    public string data;
    ConcurrentQueue<string> messageQueue = new ConcurrentQueue<string>();

    bool isClosing = false;

    private void Awake()
    {
        if (communication == null)
        {
            communication = this;
        }
        else if (communication != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        // Khởi tạo UDP server
        udpServer = new UdpClient(12345);
        endPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 12345);

        // Nhận dữ liệu từ Python
        ReceiveData();

        // Bắt đầu luồng để gửi tin nhắn từ hàng đợi
        SendData();
    }

    void SendData()
    {
        Thread sendThread = new Thread(() =>
        {
            while(true)
            {
                // Kiểm tra và gửi tin nhắn từ hàng đợi
                string message;
                while(messageQueue.TryDequeue(out message))
                {
                    Debug.Log(message);

                    byte[] responseData = Encoding.ASCII.GetBytes(message);
                    udpServer.Send(responseData, responseData.Length, endPoint);
                }
                Thread.Sleep(10);
            }
        });
        sendThread.Start();
    }

    void ReceiveData()
    {
        udpServer.BeginReceive(ReceiveCallback, null);
    }

    void ReceiveCallback(IAsyncResult result)
    {
        byte[] receivedData = udpServer.EndReceive(result, ref endPoint);
        string receivedMessage = Encoding.ASCII.GetString(receivedData);
        
        data = receivedMessage;

        // In ra dữ liệu nhận được từ Python
        Debug.Log("Received message from Python: " + receivedMessage);

        // Tiếp tục nhận dữ liệu
        ReceiveData();
    }

    public void SendData(string message)
    {
        messageQueue.Enqueue(message);
    }

    void OnApplicationQuit()
    {
        for (int i = 0;i < 10; i++)
        {
            SendData("CLOSE");
        }

        Thread.Sleep(100);

        // Đóng kết nối khi ứng dụng thoát
        udpServer.Close();
    }
}
