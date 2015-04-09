using System;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Chat;
using System.Threading;
using Logger;
using Tasks;

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
        private const string IP_ADDRESS = "127.0.0.1";
        private const int PORT = 13000;

        Client client = null;
        Thread receiverthread;

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
            sendbutton.Enabled = false;
            messagetextbox.Enabled = false;
        }

        // Returns a new line character. The syntax is too long to concatenate. 
        private string NewLine()
        {
            return Environment.NewLine;
        }

        // Connects to a server, else displays a message.
        private void connectbutton_Click(object sender, EventArgs e)
        {
            if (client == null || !client.Connected)
            {
                try
                {
                    client = new Client();
                    client.Connected = client.connect(IP_ADDRESS, PORT);
                    client.MessagedReceived += new MessagedReceivedHandler(Receive);
                }
                catch (System.Exception)
                {
                    AppendToConvoTextBox("Error: Failed to connect.");
                }
                finally
                {
                    if (client.Connected)
                    {
                        AppendToConvoTextBox("Connection succeeded.");
                        StartReceiveThread();
                        sendbutton.Enabled = true;
                        messagetextbox.Enabled = true;
                    }
                    else
                    {
                        AppendToConvoTextBox("Error: Failed to connect.");
                    }
                }
            }
            else
            {
                AppendToConvoTextBox("We're already connected!");
            }
        }

        // Terminates the current receiver thread and connection, else displays a message.
        private void disconnectbutton_Click(object sender, EventArgs e)
        {
            if (client.Connected)
            {
                try
                {
                    client.Connected = false;
                    client.terminate();
                    client = null;
                }
                catch (System.Exception)
                {
                    AppendToConvoTextBox("Error: Failed to terminate the connection.");
                }
                finally
                {
                    if (client == null || !client.Connected)
                    {
                        AppendToConvoTextBox("Connection terminated.");
                        sendbutton.Enabled = false;
                        messagetextbox.Enabled = false;
                    }
                }
            }
            else
            {
                AppendToConvoTextBox("No need. We're not currently connected!");
            }
        }

        // Starts the message receiving thread.
        private void StartReceiveThread()
        {
            // Create the thread object, passing in the Receive method
            // via a ThreadStart delegate.
            this.receiverthread = new Thread(new ThreadStart(client.receive));
            //receiverthread.IsBackground
            // Starting the thread
            this.receiverthread.Start();
        }

        public void Receive(Tasks.MessagedReceivedEventArgs msg)
        {
            if (conversationtextbox.InvokeRequired)
            {
                MethodInvoker myMethod = new MethodInvoker(
                    delegate
                    {
                        AppendToConvoTextBox(msg.Message);
                    }
                );
                conversationtextbox.Invoke(myMethod);
            }
            else
            {
                AppendToConvoTextBox(msg.Message);
            }           
        }

        // Sets the text of a form control. 
        private void AppendToConvoTextBox(string text)
        {
            this.conversationtextbox.Text += NewLine() + text;
            this.conversationtextbox.SelectionStart = this.conversationtextbox.TextLength;
            this.conversationtextbox.ScrollToCaret();
        }

        // Sends messagetextbox.Text if Length > 0
        private void sendbutton_Click(object sender, EventArgs e)
        {
            if (client.Connected && messagetextbox.Text.Length > 0)
            {
                bool success = false;

                try
                {
                    client.send(messagetextbox.Text);
                    success = true;
                }
                catch (System.Exception)
                {
                    AppendToConvoTextBox("Error: Message failed to send.");
                }
                finally
                {
                    if (success)
                    {
                        AppendToConvoTextBox("You:      " + messagetextbox.Text);
                        messagetextbox.Text = "";
                    }
                }
            }
            else if (!client.Connected)
            {
                this.conversationtextbox.Text += NewLine() + "Please connect to a server.";
            }
        }

        // Overriding the form close.
        protected override void OnFormClosing(FormClosingEventArgs e)
        {        
            if (client != null)
            {
                client.Connected = false;
                client.terminate();
                client = null;
            }
        }
    }
}