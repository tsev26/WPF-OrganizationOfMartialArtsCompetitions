using OOMAC.WPF.ViewModels;
using System.Windows.Input;

namespace OOMAC.WPF.Commands
{
    public delegate ICommand CreateCommand<TViewModel>(TViewModel viewModel) where TViewModel : ViewModelBase;
}
