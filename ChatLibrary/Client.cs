﻿using System.Net;
using System.Net.Sockets;
using Interfaces;

namespace Chat
{
    /// <summary>
    /// A client object for transmitting TCP chat messages.
    /// </summary>
    public class Client : ChatBase, IChatService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Client"/> class.
        /// </summary>
        /// <param name="server">The server.</param>
        /// <param name="port">The port.</param>
        public Client(ILoggingService logger)
            : base(logger){ }

        /// <summary>
        /// Connects this instance.
        /// </summary>
        override public bool connect(string serverip, int port)
        {
            try
            {
                this.client = new TcpClient(serverip, port);
                this.stream = client.GetStream();
                logger.Log("Connected.");
            }
            catch (System.Exception error)
            {
                logger.Log("Failed to connect: " + error.Message);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Terminates this instance.
        /// </summary>
        override public void terminate()
        {
            // Close everything.
            try
            {
                stream.Close();
                client.Close();
                logger.Log("Connection terminated.");
            }
            catch (System.Exception error)
            {
                logger.Log("False connection on exit: " + error.Message);
            }
        }
    }
}
