﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Network
{
    /// <summary>
    /// Creates a web interface for users to connect and send info to. 
    /// </summary>
    class NetworkManager : IDisposable
    {
        public readonly int Port;

        Socket serverSocket;

        List<NetworkClient> clients = new List<NetworkClient>();



        public NetworkManager(int port = 39293)
        {
            this.Port = port;
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            var ipEndP = new IPEndPoint(IPAddress.Any, Port);
            serverSocket.Bind(ipEndP);
            serverSocket.Listen(3);

            serverSocket.BeginAccept(onClientConnect, null);
        }

        public void Dispose()
        {
            //remove them clients
            foreach (var c in clients)
                c.Dispose();
            //then stop the server
            serverSocket.Dispose();
        }

        void onClientConnect(IAsyncResult ar)
        {
            //accept the connection
            Socket sock = serverSocket.EndAccept(ar);

            //add the client
            var client = new NetworkClient(sock);
            clients.Add(client);

            //continue listening
            serverSocket.BeginAccept(new AsyncCallback(onClientConnect), null);
        }
    }
}
