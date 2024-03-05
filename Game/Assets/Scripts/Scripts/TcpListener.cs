using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class TcpListener : MonoBehaviour
{
    TcpServer tcpServer;
    static byte[] buffer;
    string data;
    [SerializeField] private GameObject cube;
    private class TcpServer
    {
        public string ip;
        public int port;
        IPEndPoint endPoint;
        Socket socket;
        public TcpServer(string ip, int port)
        {
            this.ip = ip;
            this.port = port;
            this.endPoint = new IPEndPoint(IPAddress.Any, port);
            this.socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(endPoint);
            socket.Listen(10);
        }
        public void Wait()
        {

            var client = socket.Accept();
            Debug.Log(client.RemoteEndPoint);
            while (true)
            {

                byte[] buffer = new byte[1024];
                int size;

                do
                {
                    try
                    {
                        size = client.Receive(buffer);

                    }
                    catch (Exception) {
                        Debug.Log("клієнт відключився");
                        break;
                    }


                } while (client.Available > 0);
                TcpListener.buffer = buffer;
            }
        }
    }

    void Start()
    {
        tcpServer = new TcpServer("192.168.1.8", 8888);
        Thread thread = new Thread(new ThreadStart(tcpServer.Wait));
        thread.Start();
    }
    void Update()
    {
        if (buffer != null)
        {
            
            GetComponent<TextMeshProUGUI>().text = Encoding.UTF8.GetString(buffer);
            //data = Encoding.UTF8.GetString(buffer);

            //Vector3 result = new Vector3();

            //float x, y, z;

            buffer = null;
        }

    }
}
