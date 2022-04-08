using OOMAC.WPF.ViewModels;
using System;

namespace OOMAC.WPF.Stores
{
    public class NavigationStore : INavigationStore
    {
        private ViewModelBase _currentViewModel;
        private string _currentViewModelName; 

        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel?.Dispose();
                _currentViewModel = value;
                OnCurrentViewModelChanged();
            }
        }

        public string CurrentViewModelName
        {
            get
            {
                return _currentViewModelName;
            }
            set
            {
                _currentViewModelName = value;
            }
        }

        public event Action CurrentViewModelChanged;

        private void OnCurrentViewModelChanged()
        {
            CurrentViewModelChanged?.Invoke();
        }
    }
}
