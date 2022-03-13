using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOMAC.WPF.Stores
{
    public class MessageStore
    {
        public enum MessageType
        {
            Status,
            Error
        }

        private string _currentMessage;
        public string CurrentMessage
        {
            get => _currentMessage;
            private set
            {
                _currentMessage = value;
                CurrentMessageChanged?.Invoke();
            }
        }

        private MessageType _currentMessageType;

        public MessageType CurrentMessageType
        {
            get => _currentMessageType;
            private set
            {
                _currentMessageType = value;
                CurrentMessageTypeChanged?.Invoke();
            }
        }

        public bool HasCurrentMessage => !string.IsNullOrEmpty(CurrentMessage);

        public event Action CurrentMessageChanged;
        public event Action CurrentMessageTypeChanged;


        public void SetCurrentMessage(string message, MessageType messageType)
        {
            CurrentMessage = message;
            CurrentMessageType = messageType;
        }

        public void ClearCurrentMessage()
        {
            CurrentMessage = string.Empty;
        }

        public async Task ClearCurrentMessage(int afterSeconds, string msg)
        {
            await Task.Delay(TimeSpan.FromSeconds(afterSeconds));
            if (_currentMessage == msg)
            {
                CurrentMessage = "";
            }
        }
    }
}
