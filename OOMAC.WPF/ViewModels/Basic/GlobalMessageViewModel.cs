﻿using OOMAC.WPF.Commands;
using OOMAC.WPF.Stores;
using System.Windows.Input;
using static OOMAC.WPF.Stores.MessageStore;

namespace OOMAC.WPF.ViewModels
{
    public class GlobalMessageViewModel : ViewModelBase
    {
        private readonly MessageStore _messageStore;

        public string Message => _messageStore.CurrentMessage;
        public bool IsStatusMessage => _messageStore.CurrentMessageType == MessageType.Status;
        public bool IsErrorMessage => _messageStore.CurrentMessageType == MessageType.Error;
        public bool HasMessage => _messageStore.HasCurrentMessage;

        public ICommand ClearMessageCommand { get; }

        public GlobalMessageViewModel(MessageStore messageStore)
        {
            _messageStore = messageStore;

            _messageStore.CurrentMessageChanged += MessageStore_CurrentMessageChanged;
            _messageStore.CurrentMessageTypeChanged += MessageStore_CurrentMessageTypeChanged;

            ClearMessageCommand = new ClearMessageCommand(_messageStore);
        }

        public override void Dispose()
        {
            _messageStore.CurrentMessageChanged -= MessageStore_CurrentMessageChanged;
            _messageStore.CurrentMessageTypeChanged -= MessageStore_CurrentMessageTypeChanged;

            base.Dispose();
        }

        private void MessageStore_CurrentMessageChanged()
        {
            OnPropertyChanged(nameof(Message));
            OnPropertyChanged(nameof(HasMessage));
        }

        private void MessageStore_CurrentMessageTypeChanged()
        {
            OnPropertyChanged(nameof(IsStatusMessage));
            OnPropertyChanged(nameof(IsErrorMessage));
        }
    }
}
