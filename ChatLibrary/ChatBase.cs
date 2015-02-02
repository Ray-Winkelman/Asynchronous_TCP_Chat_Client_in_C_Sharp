using System.Net;
using System.Net.Sockets;


namespace Chat
{
    public abstract class ChatBase
    {
        protected TcpClient client;
        protected TcpListener server;
        protected NetworkStream stream;

        abstract public void connect();
        abstract public void terminate();

        /// <summary>
        /// Sends the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void send(string message)
        {
            byte[] data = System.Text.Encoding.ASCII.GetBytes(message);

            // Send the message to the connected TcpServer. 
            this.stream.Write(data, 0, data.Length);
        }

        /// <summary>
        /// Receives incoming messages.
        /// </summary>
        /// <returns></returns>
        public string receive()
        {
            string responseData = string.Empty;

            while (stream.DataAvailable)
            {
                // Buffer to store the response bytes.
                byte[] data = new byte[256];
                
                // Read the first batch of the TcpServer response bytes.
                int bytes = stream.Read(data, 0, data.Length);
                responseData += System.Text.Encoding.ASCII.GetString(data, 0, bytes);              
            }

            return responseData;
        }
    }
}
