using System;
using System.Runtime.InteropServices.ComTypes;

namespace Interfaces
{
    public interface IChatService
    {
        void setMessageReceivedHandler(dynamic handler);

        bool isConnected();

        void setConnected(bool isConnected);

        /// <summary>
        /// Sends the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        void send(string message);

        /// <summary>
        /// Receives incoming messages.
        /// </summary>
        /// <returns></returns>
        void receive();

        /// <summary>
        /// Connects to a client or server.
        /// </summary>
        /// <returns></returns>
        bool connect(string serverip, int port);

        /// <summary>
        /// Terminates this instance.
        /// </summary>
        void terminate();
    }
}
