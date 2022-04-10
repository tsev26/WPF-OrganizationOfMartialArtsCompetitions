using OOMAC.WPF.Stores;


namespace OOMAC.WPF.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;

        public TopMenuLayerViewModel TopMenuLayerViewModel { get; }
        public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;

        public MainViewModel(NavigationStore navigationStore, TopMenuLayerViewModel topMenuLayerViewModel)
        {
            _navigationStore = navigationStore;

            TopMenuLayerViewModel = topMenuLayerViewModel;

            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
        }

        public string CurrentViewModelName => _navigationStore.CurrentViewModelName;

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
            OnPropertyChanged(nameof(CurrentViewModelName));
        }

    }
}
