using Microsoft.Extensions.DependencyInjection;
using OOMAC.Domain.Models;
using OOMAC.EF;
using OOMAC.EF.Services;
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

            //stores
            services.AddSingleton<NavigationStore>();
            services.AddSingleton<TournamentStore>();
            services.AddSingleton<ContestantStore>();


            //first loaded ViewModel
            services.AddSingleton<INavigationService>(s => CreateTournamentNavigationService(s));

            //DB
            services.AddSingleton<OOMACDBContextFactory>();
            services.AddSingleton<GenericDataService<Contestant>>();
            services.AddSingleton<GenericDataService<Tournament>>();


            //ViewModels
            services.AddSingleton<TopMenuLayerViewModel>(s => new TopMenuLayerViewModel(
                                                                    s.GetRequiredService<NavigationStore>(), 
                                                                    CreateContestantNavigationService(s), 
                                                                    CreateTournamentNavigationService(s)));
            

            services.AddSingleton<HomeViewModel>(s => new HomeViewModel());
            services.AddSingleton<ContestantViewModel>(s => new ContestantViewModel(
                                                                    s.GetRequiredService<ContestantStore>(),
                                                                    CreateContestantAddOrUpdateNavigationService(s)));
            services.AddSingleton<TournamentListViewModel>(s => new TournamentListViewModel(
                                                                        s.GetRequiredService<TournamentStore>(),
                                                                        CreateTournamentAddOrUpdateNavigationService(s),
                                                                        CreateTournamentAddContestantsNavigationService(s)));
            
            services.AddSingleton<TournamentAddOrUpdateViewModel>(s => new TournamentAddOrUpdateViewModel(
                                                                            s.GetRequiredService<TournamentStore>(),
                                                                            CreateTournamentNavigationService(s),
                                                                            s.GetRequiredService<GenericDataService<Tournament>>()));

            services.AddSingleton<TournamentAddContestantsViewModel>(s => new TournamentAddContestantsViewModel());
            services.AddSingleton<ContestantAddOrUpdateViewModel>(s => new ContestantAddOrUpdateViewModel(
                                                                                s.GetRequiredService<ContestantStore>(),
                                                                                CreateContestantNavigationService(s),
                                                                                s.GetRequiredService<GenericDataService<Contestant>>()));


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


        // TODO: Add private function for all navigation to all viewModels

        private INavigationService CreateContestantNavigationService(IServiceProvider serviceProvider)
        {
            string name = "Závodníci";
            return new NavigationService<ContestantViewModel>(
                serviceProvider.GetRequiredService<NavigationStore>(),
                () => serviceProvider.GetRequiredService<ContestantViewModel>(),
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

        private INavigationService CreateTournamentAddContestantsNavigationService(IServiceProvider serviceProvider)
        {
            string name = "Přidání závodníků do turnaje";
            return new NavigationService<TournamentAddContestantsViewModel>(
                serviceProvider.GetRequiredService<NavigationStore>(),
                () => serviceProvider.GetRequiredService<TournamentAddContestantsViewModel>(),
                name);
        }

        private INavigationService CreateTournamentAddOrUpdateNavigationService(IServiceProvider serviceProvider)
        {
            string name = "Vytvoření / úprava turnaje";
            return new NavigationService<TournamentAddOrUpdateViewModel>(
                serviceProvider.GetRequiredService<NavigationStore>(),
                () => serviceProvider.GetRequiredService<TournamentAddOrUpdateViewModel>(),
                name);
        }

        private INavigationService CreateContestantAddOrUpdateNavigationService(IServiceProvider serviceProvider)
        {
            string name = "Vytvoření / úprava závodníka";
            return new NavigationService<ContestantAddOrUpdateViewModel>(
                serviceProvider.GetRequiredService<NavigationStore>(),
                () => serviceProvider.GetRequiredService<ContestantAddOrUpdateViewModel>(),
                name);
        }


        /*
        private INavigationService CreateHomeNavigationService(IServiceProvider serviceProvider)
        {
            string name = "Menu";
            return new NavigationService<HomeViewModel>(
                serviceProvider.GetRequiredService<NavigationStore>(),
                () => serviceProvider.GetRequiredService<HomeViewModel>(),
                name);
        }
        */

    }
}
