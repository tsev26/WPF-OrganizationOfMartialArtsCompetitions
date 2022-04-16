using OOMAC.Domain.Models;
using OOMAC.Domain.Models.Calculating;
using OOMAC.EF.Services;
using OOMAC.WPF.Commands;
using OOMAC.WPF.Stores;
using System.Collections.Generic;
using System.Linq;
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

        //public List<GroupTable> groupTables => Matches.Select(x => x.ContestantA.ShortName, x => x.)

        

        /*
        public void superFunction()
        {

            var groupTablesA = Matches.Select(x => new GroupTable { ConName = x.ContestantA.ShortName, MatchedPlayed = 1, PointsObtained = x.ScoreContestantA }).ToList();//.Concat(Matches.Select(x => new { x.ContestantB.ShortName, V = 1, x.ScoreContestantB }));
            var groupTablesB = Matches.Select(x => new GroupTable { ConName = x.ContestantB.ShortName, MatchedPlayed = 1, PointsObtained = x.ScoreContestantB }).ToList();

            var groupTable = groupTablesA.Concat(groupTablesB).ToList();

            var sumsInGroupTable = groupTable.GroupBy(s => s.ConName).Select(x => new GroupTable { ConName = x.First().ConName, MatchedPlayed = x.Sum(x => x.MatchedPlayed), PointsObtained = x.Sum(x => x.PointsObtained) }).ToList();

            var resultInOneRow = Matches.Select(x => new GroupTable { ConName = x.ContestantA.ShortName, MatchedPlayed = 1, PointsObtained = x.ScoreContestantA }).Concat(Matches.Select(x => new GroupTable { ConName = x.ContestantB.ShortName, MatchedPlayed = 1, PointsObtained = x.ScoreContestantB })).GroupBy(s => s.ConName).Select(x => new GroupTable { ConName = x.First().ConName, MatchedPlayed = x.Sum(x => x.MatchedPlayed), PointsObtained = x.Sum(x => x.PointsObtained) }).ToList();
        }
        */


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
                OnPropertyChanged(nameof(GroupTablesSum));
                OnPropertyChanged(nameof(ShowGroupTablesSum));
                SelectedMatch = null;
            }
        }

        public bool IsBracketSelected => SelectedBracket != null;

        public List<Match> Matches => SelectedBracket?.Matches.ToList();

        public List<GroupTable> GroupTablesSum => Matches.Where(x => x.Bracket.Round == 0).Select(x => new GroupTable { Contestant = x.ContestantA, MatchedPlayed = x.HasFinished ? 1 : 0, PointsObtained = x.ScoreContestantA }).Concat(Matches.Where(x => x.Bracket.Round == 0).Select(x => new GroupTable { Contestant = x.ContestantB, MatchedPlayed = x.HasFinished ? 1 : 0, PointsObtained = x.ScoreContestantB })).GroupBy(s => s.Contestant).Select(x => new GroupTable { Contestant = x.First().Contestant, MatchedPlayed = x.Sum(x => x.MatchedPlayed), PointsObtained = x.Sum(x => x.PointsObtained) }).OrderByDescending(x => x.PointsObtained).ToList();

        public bool ShowGroupTablesSum => SelectedBracket?.Round == 0;

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
                OnPropertyChanged(nameof(IsMatchOneOne));
            }
        }

        public bool IsMatchSelected => SelectedMatch != null;

        public bool IsMatchZeroZero => SelectedMatch?.ScoreContestantA == 0 && SelectedMatch?.ScoreContestantB == 0;

        public bool HasMatchFinished => !SelectedMatch?.HasFinished ?? false;

        public bool HasMatchNotFinished => !HasMatchFinished;


        public bool IsMatchOneOne => HasMatchFinished && SelectedMatch?.ScoreContestantA == 1 && SelectedMatch?.ScoreContestantB == 1 && SelectedMatch?.Bracket.Round == 0;
    }
}
