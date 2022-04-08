using OOMAC.WPF.Stores;
using OOMAC.WPF.ViewModels;

namespace OOMAC.WPF.Services.Navigations
{
    public class NavigationService<TViewModel> : INavigationService where TViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly CreateViewModel<TViewModel> _createViewModel;
        private readonly string _viewModelName;

        public NavigationService(NavigationStore navigationStore, CreateViewModel<TViewModel> createViewModel, string viewModelName)
        {
            _navigationStore = navigationStore;
            _createViewModel = createViewModel;
            _viewModelName = viewModelName;
        }

        public void Navigate()
        {
            _navigationStore.CurrentViewModelName = _viewModelName;
            _navigationStore.CurrentViewModel = _createViewModel();
        }
    }
}
