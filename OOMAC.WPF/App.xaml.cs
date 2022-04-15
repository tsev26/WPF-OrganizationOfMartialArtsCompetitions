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
            services.AddSingleton<TournamentDataService>();
            services.AddSingleton<ContestantDataService>(); 



            //ViewModels
            services.AddSingleton<TopMenuLayerViewModel>(s => new TopMenuLayerViewModel(
                                                                    s.GetRequiredService<NavigationStore>(), 
                                                                    CreateContestantNavigationService(s), 
                                                                    CreateTournamentNavigationService(s)));
            
            services.AddSingleton<ContestantViewModel>(s => new ContestantViewModel(
                                                                    s.GetRequiredService<ContestantStore>(),
                                                                    CreateContestantAddOrUpdateNavigationService(s)));
            services.AddSingleton<TournamentListViewModel>(s => new TournamentListViewModel(
                                                                        s.GetRequiredService<TournamentStore>(),
                                                                        CreateTournamentAddOrUpdateNavigationService(s),
                                                                        CreateTournamentAddContestantsNavigationService(s),
                                                                        CreateTournamentDetailNavigationService(s),
                                                                        s.GetRequiredService<TournamentDataService>()));
            
            services.AddSingleton<TournamentAddOrUpdateViewModel>(s => new TournamentAddOrUpdateViewModel(
                                                                            s.GetRequiredService<TournamentStore>(),
                                                                            CreateTournamentNavigationService(s),
                                                                            s.GetRequiredService<TournamentDataService>()));

            services.AddSingleton<TournamentAddContestantsViewModel>(s => new TournamentAddContestantsViewModel(
                                                                              s.GetRequiredService<TournamentStore>(),
                                                                              s.GetRequiredService<ContestantStore>(),
                                                                              s.GetRequiredService<TournamentDataService>()));
            services.AddSingleton<ContestantAddOrUpdateViewModel>(s => new ContestantAddOrUpdateViewModel(
                                                                                s.GetRequiredService<ContestantStore>(),
                                                                                CreateContestantNavigationService(s),
                                                                                s.GetRequiredService<ContestantDataService>()));

            services.AddSingleton<TournamentSelectedViewModel>(s => new TournamentSelectedViewModel(
                                                                            s.GetRequiredService<TournamentStore>(), 
                                                                            s.GetRequiredService<TournamentDataService>()));

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


        private INavigationService CreateNavigationService<TViewModel>(string name, IServiceProvider serviceProvider) where TViewModel : ViewModelBase
        {
            return new NavigationService<TViewModel> (
                serviceProvider.GetRequiredService<NavigationStore>(),
                () => serviceProvider.GetRequiredService<TViewModel>(),
                name);
        }

        private INavigationService CreateContestantNavigationService(IServiceProvider serviceProvider)
        {
            return CreateNavigationService<ContestantViewModel>("Závodníci", serviceProvider);
        }


        private INavigationService CreateTournamentNavigationService(IServiceProvider serviceProvider)
        {
            return CreateNavigationService<TournamentListViewModel>("Turnaje", serviceProvider);
        }

        private INavigationService CreateTournamentAddContestantsNavigationService(IServiceProvider serviceProvider)
        {
            return CreateNavigationService<TournamentAddContestantsViewModel>("Přidání závodníků do turnaje", serviceProvider);
        }

        private INavigationService CreateTournamentAddOrUpdateNavigationService(IServiceProvider serviceProvider)
        {
            return CreateNavigationService<TournamentAddOrUpdateViewModel>("Vytvoření / úprava turnaje", serviceProvider);
        }

        private INavigationService CreateContestantAddOrUpdateNavigationService(IServiceProvider serviceProvider)
        {
            return CreateNavigationService<ContestantAddOrUpdateViewModel>("Vytvoření / úprava závodníka", serviceProvider);
        }

        private INavigationService CreateTournamentDetailNavigationService(IServiceProvider serviceProvider)
        {
            return CreateNavigationService<TournamentSelectedViewModel>("Detail turnaje", serviceProvider);
        }

    }
}
