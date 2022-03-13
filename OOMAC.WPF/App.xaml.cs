using Microsoft.Extensions.DependencyInjection;
using OOMAC.EF;
using OOMAC.WPF.Services.Navigations;
using OOMAC.WPF.Stores;
using OOMAC.WPF.ViewModels;
using System;
using System.Windows;

namespace OOMAC.WPF
{

    public partial class App : Application
    {
        private readonly IServiceProvider _serviceProvider;

        public App()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddSingleton<NavigationStore>();
            services.AddSingleton<ModalNavigationStore>();

            services.AddSingleton<INavigationService>(s => CreateHomeNavigationService(s));

            services.AddSingleton<OOMACDBContextFactory>();

            services.AddSingleton<CloseModalNavigationService>();

            services.AddSingleton<HomeViewModel>(s => new HomeViewModel(CreateContestantNavigationService(s)));
            services.AddSingleton<ContestantViewModel>(s => new ContestantViewModel(CreateHomeNavigationService(s)));


            services.AddSingleton<MainViewModel>();
            services.AddSingleton<MainWindow>(s => new MainWindow()
            {
                DataContext = s.GetRequiredService<MainViewModel>()
            });

            _serviceProvider = services.BuildServiceProvider();
        }


        protected override void OnStartup(StartupEventArgs e)
        {

            INavigationService initialNavigationService = _serviceProvider.GetRequiredService<INavigationService>();
            initialNavigationService.Navigate();

            Window window = _serviceProvider.GetRequiredService<MainWindow>();
            window.Show();

            base.OnStartup(e);
        }


        private INavigationService CreateContestantNavigationService(IServiceProvider serviceProvider)
        {
            return new NavigationService<ContestantViewModel>(
                serviceProvider.GetRequiredService<NavigationStore>()
                , () => serviceProvider.GetRequiredService<ContestantViewModel>());
        }

        private INavigationService CreateHomeNavigationService(IServiceProvider serviceProvider)
        {
            return new NavigationService<HomeViewModel>(
                serviceProvider.GetRequiredService<NavigationStore>()
                , () => serviceProvider.GetRequiredService<HomeViewModel>());
        }


    }
}
