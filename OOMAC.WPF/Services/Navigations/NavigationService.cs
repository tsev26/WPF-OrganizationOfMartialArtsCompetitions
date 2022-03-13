using OOMAC.WPF.Stores;
using OOMAC.WPF.ViewModels;

namespace OOMAC.WPF.Services.Navigations
{
    public class NavigationService<TViewModel> : INavigationService where TViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly CreateViewModel<TViewModel> _createViewModel;

        public NavigationService(NavigationStore navigationStore, CreateViewModel<TViewModel> createViewModel)
        {
            _navigationStore = navigationStore;
            _createViewModel = createViewModel;
        }

        public void Navigate()
        {
            _navigationStore.CurrentViewModel = _createViewModel();
        }
    }
}
