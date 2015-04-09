using System.Net;
using System.Net.Sockets;
using Logger;
using Tasks;

namespace Chat
{
    public abstract class ChatBase
    {
        protected TcpClient client;
        protected TcpListener server;
        protected NetworkStream stream;
        protected Log logger = new Log();

        abstract public bool connect(string serverip, int port);
        abstract public void terminate();

        public event MessagedReceivedHandler MessagedReceived;
        private volatile bool connected = false;

        public bool Connected
        {
            get { return connected; }
            set { connected = value; }
        }

        /// <summary>
        /// Sends the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void send(string message)
        {
            byte[] data = System.Text.Encoding.ASCII.GetBytes(message);

            try
            {
                // Send the message to the connected TcpServer. 
                this.stream.Write(data, 0, data.Length);
                logger.LogLine("Sent: " + message);
            }
            catch (System.Exception error)
            {
                logger.LogLine("Failed to send: " + error.Message);
            }
        }

        /// <summary>
        /// Receives incoming messages.
        /// </summary>
        /// <returns></returns>
        public void receive()
        {
            string responseData = string.Empty;
            try
            {
                while (connected)
                {
                    while (stream.DataAvailable)
                    {
                        // Buffer to store the response bytes.
                        byte[] data = new byte[256];

                        // Read the first batch of the TcpServer response bytes.
                        int bytes = stream.Read(data, 0, data.Length);
                        responseData += System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                    }

                    if (responseData.Length > 0)
                    {
                        MessagedReceived(new MessagedReceivedEventArgs("Server:   " + responseData));
                        logger.LogLine("Received: " + responseData);
                        responseData = "";
                    }
                }
            }
            catch (System.Exception error)
            {
                logger.LogLine("Failed to receive: " + error.Message);
            }
        }
    }
}
