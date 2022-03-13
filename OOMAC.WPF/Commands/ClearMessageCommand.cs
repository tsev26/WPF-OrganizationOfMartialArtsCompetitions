using OOMAC.WPF.Stores;

namespace OOMAC.WPF.Commands
{
    public class ClearMessageCommand : CommandBase
    {
        private readonly MessageStore _messageStore;

        public ClearMessageCommand(MessageStore messageStore)
        {
            _messageStore = messageStore;
        }

        public override void Execute(object parameter)
        {
            _messageStore.ClearCurrentMessage();
        }
    }
}
