using OOMAC.WPF.Commands;
using OOMAC.WPF.Services.Navigations;
using System.Windows.Input;

namespace OOMAC.WPF.ViewModels
{
    public class ContestantViewModel : ViewModelBase
    {
        public ICommand NavigateHomeCommand { get; }

        public ContestantViewModel(INavigationService homeNavigationService)
        {
            NavigateHomeCommand = new NavigateCommand(homeNavigationService);
        }
    }
}
