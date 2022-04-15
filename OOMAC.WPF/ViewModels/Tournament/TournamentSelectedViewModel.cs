using OOMAC.Domain.Models;
using OOMAC.EF.Services;
using OOMAC.WPF.Commands;
using OOMAC.WPF.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OOMAC.WPF.ViewModels
{
    public class TournamentSelectedViewModel : ViewModelBase
    {
        private TournamentStore _tournamentStore;

        public TournamentSelectedViewModel(TournamentStore tournamentStore, TournamentDataService tournamentDataService) 
        {
            _tournamentStore = tournamentStore;

            SetScoreCommand = new SetScoreCommand(this, tournamentDataService, tournamentStore);

            _tournamentStore.TournamentSelectionChange += TournamentSelectionChange;
        }

        private void TournamentSelectionChange()
        {
            int? selectedMatch = SelectedMatch?.Id;
            int? selectedBracket = SelectedBracket?.Id;

            OnPropertyChanged(nameof(SelectedTournament));
            OnPropertyChanged(nameof(Bracket));

            OnPropertyChanged(nameof(SelectedBracket));

            if (selectedBracket != null) { SelectedBracket = Bracket?.FirstOrDefault(x => x.Id == selectedBracket); };
            if (selectedMatch != null) { SelectedMatch = SelectedBracket?.Matches?.FirstOrDefault(x => x.Id == selectedMatch); };
        }

        public ICommand SetScoreCommand { get; }

        public Tournament SelectedTournament => _tournamentStore.SelectedTournament;


        public List<Bracket> Bracket => SelectedTournament?.Brackets.ToList();


        private Bracket _selectedBracket;
        public Bracket SelectedBracket
        {
            get
            {
                return _selectedBracket;
            }
            set
            {
                _selectedBracket = value;
                OnPropertyChanged(nameof(SelectedBracket));
                OnPropertyChanged(nameof(IsBracketSelected));
                OnPropertyChanged(nameof(Matches));
                SelectedMatch = null;
            }
        }

        public bool IsBracketSelected => SelectedBracket != null;

        public List<Match> Matches => SelectedBracket?.Matches.ToList();

        private Match _selectedMatch;
        public Match SelectedMatch
        {
            get
            {
                return _selectedMatch;
            }
            set
            {
                _selectedMatch = value;
                OnPropertyChanged(nameof(SelectedMatch));
                OnPropertyChanged(nameof(IsMatchSelected));
                OnPropertyChanged(nameof(IsMatchZeroZero));
                OnPropertyChanged(nameof(HasMatchFinished));
                OnPropertyChanged(nameof(HasMatchNotFinished));
            }
        }

        public bool IsMatchSelected => SelectedMatch != null;

        public bool IsMatchZeroZero => SelectedMatch?.ScoreContestantA == 0 && SelectedMatch?.ScoreContestantB == 0;

        public bool HasMatchFinished => !SelectedMatch?.HasFinished ?? false;

        public bool HasMatchNotFinished => !HasMatchFinished;

    }
}
