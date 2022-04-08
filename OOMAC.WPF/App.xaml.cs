﻿using Microsoft.Extensions.DependencyInjection;
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

            services.AddSingleton<INavigationService>(s => CreateHomeNavigationService(s));

            services.AddSingleton<OOMACDBContextFactory>();

            services.AddSingleton<TopMenuLayerViewModel>(s => new TopMenuLayerViewModel(
                                                                    s.GetRequiredService<NavigationStore>(), 
                                                                    CreateHomeNavigationService(s), 
                                                                    CreateContestantNavigationService(s), 
                                                                    CreateTournamentNavigationService(s)));
            

            services.AddSingleton<HomeViewModel>(s => new HomeViewModel());
            services.AddSingleton<ContestantViewModel>(s => new ContestantViewModel());
            services.AddSingleton<TournamentListViewModel>(s => new TournamentListViewModel());

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
            string name = "Závodníci";
            return new NavigationService<ContestantViewModel>(
                serviceProvider.GetRequiredService<NavigationStore>(),
                () => serviceProvider.GetRequiredService<ContestantViewModel>(),
                name);
        }

        private INavigationService CreateHomeNavigationService(IServiceProvider serviceProvider)
        {
            string name = "Menu";
            return new NavigationService<HomeViewModel>(
                serviceProvider.GetRequiredService<NavigationStore>(),
                () => serviceProvider.GetRequiredService<HomeViewModel>(),
                name);
        }

        private INavigationService CreateTournamentNavigationService(IServiceProvider serviceProvider)
        {
            string name = "Turnaje";
            return new NavigationService<TournamentListViewModel>(
                serviceProvider.GetRequiredService<NavigationStore>(),
                () => serviceProvider.GetRequiredService<TournamentListViewModel>(),
                name);
        }


    }
}
