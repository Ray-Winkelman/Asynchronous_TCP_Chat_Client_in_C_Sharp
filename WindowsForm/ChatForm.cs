using System;
using System.Windows.Forms;
using Chat;
using System.Threading;
using Interfaces;
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
    /// <summary>
    /// 
    /// </summary>
    public partial class ChatForm : Form
    {
        private string ipaddress = "127.0.0.1";
        private int port = 13000;

        private IChatService connectionBase = null;
        private Thread receiverthread = null;

        public ChatForm(IChatService connectionBase, string ipaddress, int port)
        {
            InitializeComponent();
            this.AcceptButton = sendbutton;
            this.connectionBase = connectionBase;
            this.ipaddress = ipaddress;
            this.port = port;
        }

        /// <summary>
        /// Handles the Load event of the ChatForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ChatForm_Load(object sender, EventArgs e)
        {
            conversationtextbox.Text = "Welcome, to the chat master cheese nugget." +
                NewLine() + "Connect to a server to get started." + NewLine();
            sendbutton.Enabled = false;
            messagetextbox.Enabled = false;
        }

        /// <summary>
        /// News the line.
        /// </summary>
        /// <returns></returns>
        private string NewLine()
        {
            return Environment.NewLine;
        }

        /// <summary>
        /// Handles the Click event of the connectbutton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void connectbutton_Click(object sender, EventArgs e)
        {
            if (connectionBase == null || !connectionBase.isConnected())
            {
                try
                {
                    connectionBase.setConnected(connectionBase.connect(ipaddress, port));
                    connectionBase.setMessageReceivedHandler(new MessagedReceivedHandler(Receive));
                }
                catch (System.Exception)
                {
                    AppendToConvoTextBox("Error: Failed to connect.");
                }
                finally
                {
                    if (connectionBase.isConnected())
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

        /// <summary>
        /// Handles the Click event of the disconnectbutton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void disconnectbutton_Click(object sender, EventArgs e)
        {
            if (connectionBase.isConnected())
            {
                try
                {
                    connectionBase.setConnected(false);
                    connectionBase.terminate();
                    connectionBase = null;
                }
                catch (System.Exception)
                {
                    AppendToConvoTextBox("Error: Failed to terminate the connection.");
                }
                finally
                {
                    if (connectionBase == null || !connectionBase.isConnected())
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

        /// <summary>
        /// Starts the receive thread.
        /// </summary>
        private void StartReceiveThread()
        {
            // Create the thread object, passing in the Receive method
            // via a ThreadStart delegate.
            this.receiverthread = new Thread(new ThreadStart(connectionBase.receive));
            //receiverthread.IsBackground
            // Starting the thread
            this.receiverthread.Start();
        }

        /// <summary>
        /// Receives the specified MSG.
        /// </summary>
        /// <param name="msg">The <see cref="Tasks.MessagedReceivedEventArgs"/> instance containing the event data.</param>
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

        /// <summary>
        /// Appends to convo text box.
        /// </summary>
        /// <param name="text">The text.</param>
        private void AppendToConvoTextBox(string text)
        {
            this.conversationtextbox.Text += NewLine() + text;
            this.conversationtextbox.SelectionStart = this.conversationtextbox.TextLength;
            this.conversationtextbox.ScrollToCaret();
        }

        /// <summary>
        /// Handles the Click event of the sendbutton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void sendbutton_Click(object sender, EventArgs e)
        {
            if (connectionBase.isConnected() && messagetextbox.Text.Length > 0)
            {
                bool success = false;

                try
                {
                    connectionBase.send(messagetextbox.Text);
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
            else if (!connectionBase.isConnected())
            {
                this.conversationtextbox.Text += NewLine() + "Please connect to a server.";
            }
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Form.FormClosing" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.FormClosingEventArgs" /> that contains the event data.</param>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {        
            if (connectionBase != null)
            {
                connectionBase.setConnected(false);
                connectionBase.terminate();
                connectionBase = null;
            }
        }
    }
}