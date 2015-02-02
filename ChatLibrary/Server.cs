
using System.Net;
using System.Net.Sockets;


namespace Chat
{
    /// <summary>
    /// A server object for receiving TCP chat messages.
    /// </summary>
    public class Server : ChatBase
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Server"/> class.
        /// </summary>
        /// <param name="server">The server.</param>
        /// <param name="port">The port.</param>
        public Server(string server, int port)
        {
            this.server = new TcpListener(IPAddress.Parse(server), port);
        }

        /// <summary>
        /// Connects this instance.
        /// </summary>
        override public void connect()
        {
            // Start listening for client requests.
            server.Start();
            this.client = this.server.AcceptTcpClient();
            this.stream = client.GetStream();
        }

        /// <summary>
        /// Terminates this instance.
        /// </summary>
        override public void terminate()
        {
            // Shutdown and end connection
            client.Close();
            server.Stop();
        }
    }
}
