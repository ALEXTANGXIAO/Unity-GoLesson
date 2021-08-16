using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class GameApp : MonoBehaviour
{
    private Socket socket;
    private string m_Host = "127.0.0.1";
    private int m_Port = 9000;
    private Message message = new Message();
    void Start()
    {
        socket = new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);

        socket.Connect(m_Host, m_Port);

        StartReceive();
    }

    void StartReceive()
    {
        socket.BeginReceive(message.Buffer, message.StartIndex, message.Remsize, SocketFlags.None, ReceiveCallback, null);
    }

    private void ReceiveCallback(IAsyncResult asyncResult)
    {
        try
        {
            if (socket == null || socket.Connected == false)
            {
                return;
            }

            int length = socket.EndReceive(asyncResult);

            if (length == 0)
            {
                //todo close socket

                return;
            }

            message.ReadBuffer(length);

            StartReceive();
        }
        catch (Exception e)
        {
            Debug.LogError(e);
            throw;
        }
    }

    void Update()
    {
        
    }
}
