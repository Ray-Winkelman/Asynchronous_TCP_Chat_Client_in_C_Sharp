using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Chat;
using System.Threading;
using Logger;

/// <summary>
/// 
///  Created by Ray Winkelman on 2015-1-24.
///  Copyright (c) 2014 Ray Winkelman. All rights reserved.
///  Submitted to the Nova Scotia Community College. 
///  
/// References:
/// How to: Make Thread-Safe Calls to Windows Forms Controls
/// https://msdn.microsoft.com/en-us/library/ms171728%28v=vs.85%29.aspx
/// 
/// Invoke(Delegate)
/// http://stackoverflow.com/questions/14703698/invokedelegate
/// 
/// Netcat Cheat Sheet
/// http://www.sans.org/security-resources/sec560/netcat_cheat_sheet_v1.pdf
/// </summary>
namespace Assingment_2
{
    public partial class ChatForm : Form
    {
        bool connected;
        Client client;
        Thread receiverthread;
        Log logger = new Log();
        // This delegate enables asynchronous calls for setting
        // the text property on a TextBox control.
        delegate void SetTextCallback(string text);

        public ChatForm()
        {
            InitializeComponent();
            this.AcceptButton = sendbutton;
        }

        // Displays a welcome message.
        private void ChatForm_Load(object sender, EventArgs e)
        {
            conversationtextbox.Text = "Welcome, to the chat master cheese nugget." +
                NewLine() + "Connect to a server to get started." + NewLine();

            logger.LogLine("New application instance.");
        }

        // Returns a new line character. The syntax is too long to concatenate. 
        private string NewLine()
        {
            return Environment.NewLine;
        }

        // Connects to a server, else displays a message.
        private void connectbutton_Click(object sender, EventArgs e)
        {
            try
            {
                client = new Client("127.0.0.1", 13000);
                client.connect();
                connected = true;
            }
            catch (System.Exception error)
            {
               conversationtextbox.Text += NewLine() + "Unfortunately, the chat master cheese nugget cannot connect at this time." +
               NewLine();
               logger.LogLine("Failed to connect: " + error.Message);
            }
            finally
            {
                if (connected)
                {
                    conversationtextbox.Text += NewLine() + "Connection succeeded." + NewLine();
                    logger.LogLine("Connection succeeded.");
                    StartReceiveThread();
                }
            }
        }

        // Terminates the current receiver thread and connection, else displays a message.
        private void disconnectbutton_Click(object sender, EventArgs e)
        {
            try
            {
                this.receiverthread.Abort();
                client.terminate();
                client = null;
                connected = false;
            }
            catch (System.Exception error)
            {
               conversationtextbox.Text += NewLine() + "Unfortunately, the chat master cheese nugget cannot terminate the connection at this time." +
               NewLine();
               logger.LogLine("Failed to disconnect: " + error.Message);
            }
            finally
            {
                if (!connected)
                {
                    conversationtextbox.Text += NewLine() + "Connection terminated.";
                    logger.LogLine("Connection terminated.");
                }
            }
        }

        // Starts the message receiving thread.
        private void StartReceiveThread()
        {
            // Create the thread object, passing in the Receive method
            // via a ThreadStart delegate.
            this.receiverthread = new Thread(new ThreadStart(Receive));

            // Starting the thread
            this.receiverthread.Start();
        }

        // Receives messages and calls SetText() when message.Length > 0
        public void Receive()
        {
            string incoming;

            while (connected)
            {
                incoming = client.receive();

                if (incoming.Length > 0)
                {
                    this.SetText("Server: " + incoming);
                    logger.LogLine("Server: " + incoming);
                }
            }
        }

        // Sets the text of a form control. 
        private void SetText(string text)
        {
            if (this.conversationtextbox.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.conversationtextbox.Text += NewLine() + text;
                this.conversationtextbox.SelectionStart = this.conversationtextbox.TextLength;
                this.conversationtextbox.ScrollToCaret();
            }
        }

        // Sends messagetextbox.Text if Length > 0
        private void sendbutton_Click(object sender, EventArgs e)
        {
            if (messagetextbox.Text.Length > 0)
            {
                bool success = false;

                try
                {
                    client.send(messagetextbox.Text);
                    success = true;
                }
                catch (System.Exception error)
                {
                   conversationtextbox.Text += NewLine() + "Error: Message failed to send.";
                   logger.LogLine("Failed to send: " + error.Message);
                }
                finally
                {
                    if (success)
                    {
                        this.conversationtextbox.Text += NewLine() + "You: " + messagetextbox.Text;
                        logger.LogLine("Client: " + messagetextbox.Text);
                        this.conversationtextbox.SelectionStart = this.conversationtextbox.TextLength;
                        this.conversationtextbox.ScrollToCaret();
                        messagetextbox.Text = "";
                    }
                }
            }
        }
    }
}