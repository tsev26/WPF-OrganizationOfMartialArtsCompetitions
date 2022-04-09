using OOMAC.Domain.Models;
using OOMAC.EF.Services;
using OOMAC.WPF.Services.Navigations;
using OOMAC.WPF.Stores;
using OOMAC.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OOMAC.WPF.Commands
{
    public class CreateNewTournamentCommand : CommandBase
    {

        //TODO values from contestantAddOrUpdateViewModel use to create new or update in DB
        private readonly TournamentAddOrUpdateViewModel _tournamentAddOrUpdateViewModel;
        private readonly GenericDataService<Tournament> _tournamentService;
        private readonly TournamentStore _tournamentStore;

        public ICommand NavigateTournamentCommand { get; }
        public CreateNewTournamentCommand(TournamentAddOrUpdateViewModel tournamentAddOrUpdateViewModel, 
                                          INavigationService tournamentNavigationService, 
                                          GenericDataService<Tournament> tournamentService,
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
