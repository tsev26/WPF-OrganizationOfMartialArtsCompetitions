using OOMAC.Domain.Models;
using OOMAC.EF.Services;
using OOMAC.WPF.Services.Navigations;
using OOMAC.WPF.Stores;
using OOMAC.WPF.ViewModels;
using System.Collections.Generic;
using System.Windows.Input;

namespace OOMAC.WPF.Commands
{
    public class CreateNewTournamentCommand : CommandBase
    {
        private readonly TournamentAddOrUpdateViewModel _tournamentAddOrUpdateViewModel;
        private readonly TournamentDataService _tournamentService;
        private readonly TournamentStore _tournamentStore;

        public ICommand NavigateTournamentCommand { get; }
        public CreateNewTournamentCommand(TournamentAddOrUpdateViewModel tournamentAddOrUpdateViewModel, 
                                          INavigationService tournamentNavigationService,
                                          TournamentDataService tournamentService,
                                          TournamentStore tournamentStore)
        {
            _tournamentAddOrUpdateViewModel = tournamentAddOrUpdateViewModel;
            NavigateTournamentCommand = new NavigateCommand(tournamentNavigationService);
            _tournamentService = tournamentService;
            _tournamentStore = tournamentStore;
        }

        public override void Execute(object parameter)
        {
            Tournament newTournament = new Tournament();
            newTournament.Name = _tournamentAddOrUpdateViewModel.Name;
            newTournament.MinAge = _tournamentAddOrUpdateViewModel.MinAge;
            newTournament.MaxAge = _tournamentAddOrUpdateViewModel.MaxAge;
            newTournament.MinTechnicalSkill = _tournamentAddOrUpdateViewModel.MinTechnicalSkill;
            newTournament.MaxTechnicalSkill = _tournamentAddOrUpdateViewModel.MaxTechnicalSkill;
            newTournament.Name = _tournamentAddOrUpdateViewModel.Name;
            newTournament.Brackets = new List<Bracket>();
            newTournament.Contestans = new List<Contestant>();


            if (_tournamentStore.SelectedTournament != null)
            {
                newTournament.Id = _tournamentStore.SelectedTournament.Id;
                _ = _tournamentService.Update(_tournamentStore.SelectedTournament.Id,newTournament);
            } else
            {
                _ = _tournamentService.Create(newTournament);
            }
            

            NavigateTournamentCommand.Execute(null);
        }
    }
}
