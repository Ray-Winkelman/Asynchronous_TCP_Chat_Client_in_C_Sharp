using Interfaces;
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
        public Server(ILoggingService logger)
            : base(logger) { }

        /// <summary>
        /// Connects this instance.
        /// </summary>
        override public bool connect(string serverip, int port)
        {
            // Start listening for client requests.

            this.server = new TcpListener(IPAddress.Parse(serverip), port);
            server.Start();
            this.client = this.server.AcceptTcpClient();
            this.stream = client.GetStream();
            return true;
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
