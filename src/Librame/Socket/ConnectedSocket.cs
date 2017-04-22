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
using System.Text;

namespace Librame.Socket
{
    /// <summary>
    /// An IPv4 TCP connected socket.
    /// </summary>
    public sealed class ConnectedSocket : IDisposable
    {
        private readonly Encoding _encoding;

        private readonly System.Net.Sockets.Socket _socket;


        /// <summary>
        /// Constructs and connects the socket.
        /// </summary>
        /// <param name="endpoint">Endpoint to connect to</param>
        public ConnectedSocket(EndPoint endpoint)
            : this(endpoint, Encoding.UTF8)
        {
        }
        /// <summary>
        /// Constructs and connects the socket.
        /// </summary>
        /// <param name="endpoint">Endpoint to connect to</param>
        /// <param name="encoding">Encoding of the content sended and received by the socket</param>
        public ConnectedSocket(EndPoint endpoint, Encoding encoding)
        {
            _encoding = encoding;
            _socket = new System.Net.Sockets.Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _socket.Connect(endpoint);
        }

        /// <summary>
        /// Constructs and connects the socket.
        /// </summary>
        /// <param name="host">Host to connect to</param>
        /// <param name="port">Port to connect to</param>
        public ConnectedSocket(string host, int port)
            : this(host, port, Encoding.UTF8)
        {
        }
        /// <summary>
        /// Constructs and connects the socket.
        /// </summary>
        /// <param name="host">Host to connect to</param>
        /// <param name="port">Port to connect to</param>
        /// <param name="encoding">Encoding of the content sended and received by the socket</param>
        public ConnectedSocket(string host, int port, Encoding encoding)
        {
            _encoding = encoding;
            _socket = new System.Net.Sockets.Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _socket.Connect(host, port);
        }


        /// <summary>
        /// ConnectedSocket.
        /// </summary>
        /// <param name="socket"></param>
        internal ConnectedSocket(System.Net.Sockets.Socket socket)
        {
            _encoding = Encoding.UTF8;
            _socket = socket;
        }

        /// <summary>
        /// True if there's any data to receive on the socket.
        /// </summary>
        public bool AnythingToReceive
        {
            get
            {
                return _socket.Available > 0;
            }
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
        /// Disposes the socket.
        /// </summary>
        public void Dispose()
        {
            _socket.Shutdown(SocketShutdown.Both);
            _socket.Dispose();
        }

        /// <summary>
        /// Receives any pending data.
        /// This blocks execution until there's data available.
        /// </summary>
        /// <param name="bufferSize">Amount of data to read</param>
        /// <returns>Received data</returns>
        public string Receive(int bufferSize = 1024)
        {
            var buffer = new byte[bufferSize];
            _socket.Receive(buffer);
            return _encoding.GetString(buffer).TrimEnd('\0');
        }

        /// <summary>
        /// Sends the given data.
        /// </summary>
        /// <param name="data">Data to send</param>
        public void Send(string data)
        {
            var bytes = _encoding.GetBytes(data);
            _socket.Send(bytes);
        }
    }
}
