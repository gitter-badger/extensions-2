﻿#region License

/* **************************************************************************************
 * Copyright (c) SocketLibrary All rights reserved.
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using System;
using System.Net;
using System.Net.Sockets;

namespace Librame.Socket
{
    /// <summary>
    /// An IPv4 TCP socket listener.
    /// </summary>
    public sealed class SocketListener : IDisposable
    {
        private readonly System.Net.Sockets.Socket _socket;

        /// <summary>
        /// Constructs and listens in the given port.
        /// </summary>
        /// <param name="endpoint">Endpoint to listen (defaults to 127.0.0.1)</param>
        /// <param name="backlog">Number of incoming connections that can be queued for acceptance</param>
        public SocketListener(EndPoint endpoint, int backlog = 10)
        {
            _socket = new System.Net.Sockets.Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _socket.Bind(endpoint);
            _socket.Listen(backlog);
        }

        /// <summary>
        /// Constructs and listens in the given port.
        /// </summary>
        /// <param name="port">Port to listen</param>
        /// <param name="ip">IP to listen (defaults to 127.0.0.1)</param>
        /// <param name="backlog">Number of incoming connections that can be queued for acceptance</param>
        public SocketListener(int port, string ip = "127.0.0.1", int backlog = 10)
        {
            var _ip = IPAddress.Parse(ip);
            var endpoint = new IPEndPoint(_ip, port);
            _socket = new System.Net.Sockets.Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _socket.Bind(endpoint);
            _socket.Listen(backlog);
        }

        /// <summary>
        /// The underlying socket.
        /// </summary>
        public System.Net.Sockets.Socket UnderlyingSocket
        {
            get
            {
                return _socket;
            }
        }

        /// <summary>
        /// Accepts a connection.
        /// This blocks the execution until a connection is made.
        /// </summary>
        /// <returns>The connected socket</returns>
        public ConnectedSocket Accept()
        {
            return new ConnectedSocket(_socket.Accept());
        }

        /// <summary>
        /// Disposes the listener.
        /// </summary>
        public void Dispose()
        {
            _socket.Dispose();
        }
    }
}
