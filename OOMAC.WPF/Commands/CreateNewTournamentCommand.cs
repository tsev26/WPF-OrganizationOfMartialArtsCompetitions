using OOMAC.WPF.Services.Navigations;
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
        public ICommand NavigateTournamentCommand { get; }
        public CreateNewTournamentCommand(TournamentAddOrUpdateViewModel tournamentAddOrUpdateViewModel, INavigationService tournamentNavigationService)
        {
            _tournamentAddOrUpdateViewModel = tournamentAddOrUpdateViewModel;
            NavigateTournamentCommand = new NavigateCommand(tournamentNavigationService);
        }

        public override void Execute(object parameter)
        {
            NavigateTournamentCommand.Execute(null);
        }
    }
}
