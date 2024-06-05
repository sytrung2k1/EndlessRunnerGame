using UnityEngine;
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;

public class UDPReceive : MonoBehaviour
{
    public static UDPReceive udpReceive;

    UdpClient client;
    IPEndPoint endPoint;
    public int port = 12345;

    public bool startRecieving = true;
    public bool printToConsole = false;
    public string data;

    Queue<string> messageQueue = new Queue<string>();
    object queueLock = new object();

    public void Start()
    {
        if (udpReceive == null)
        {
            udpReceive = this;
        }
        else if (udpReceive != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        client = new UdpClient(port);
        endPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);

        // Nhận dữ liệu từ Python
        ReceiveData();
    }

    private void ReceiveData()
    {
        client.BeginReceive(ReceiveCallback, null);
    }

    void ReceiveCallback(IAsyncResult result)
    {
        byte[] dataByte = client.EndReceive(result, ref endPoint);
        data = Encoding.ASCII.GetString(dataByte);

        if (printToConsole)
        {
            Debug.Log("Received message from Python: " + data);
        }

        lock (queueLock)
        {
            if (messageQueue.Count > 0)
            {
                string responseMessage = messageQueue.Dequeue();
                byte[] responseData = Encoding.ASCII.GetBytes(responseMessage);
                client.Send(responseData, responseData.Length, endPoint);
            }
        }

        ReceiveData();
    }

    public void SendData(string message)
    {
        lock (queueLock)
        {
            messageQueue.Enqueue(message);
        }
    }

    private void OnApplicationQuit()
    {
        if (client != null)
        {
            client.Close();
        }
    }

}
