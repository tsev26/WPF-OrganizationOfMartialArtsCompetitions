using OOMAC.WPF.ViewModels;

namespace OOMAC.WPF.Stores
{
    public interface INavigationStore
    {
        ViewModelBase CurrentViewModel { set; }
    }
}
