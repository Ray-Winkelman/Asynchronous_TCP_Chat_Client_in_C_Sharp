using System.Net;
using System.Net.Sockets;


namespace Chat
{
    /// <summary>
    /// A client object for transmitting TCP chat messages.
    /// </summary>
    public class Client : ChatBase
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Client"/> class.
        /// </summary>
        /// <param name="server">The server.</param>
        /// <param name="port">The port.</param>
        public Client(string server, int port)
        {
            try
            {
                this.client = new TcpClient(server, port);
            }
            catch (System.Exception)
            {
                // Report to Main();
            }              
        }

        /// <summary>
        /// Connects this instance.
        /// </summary>
        override public void connect()
        {
            this.stream = client.GetStream();
        }

        /// <summary>
        /// Terminates this instance.
        /// </summary>
        override public void terminate()
        {
            // Close everything.
            stream.Close();
            client.Close();  
        }
    }
}
