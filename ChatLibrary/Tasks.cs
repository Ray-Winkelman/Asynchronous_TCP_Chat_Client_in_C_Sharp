using System;

namespace Tasks
{
    public delegate void MessagedReceivedHandler(MessagedReceivedEventArgs msg);

    public class MessagedReceivedEventArgs : EventArgs
    {
        private string message;

        public MessagedReceivedEventArgs(string value)
        {
            message = value;
        }

        public string Message
        {
            get
            {
                return message;
            }
        }
    }
}
