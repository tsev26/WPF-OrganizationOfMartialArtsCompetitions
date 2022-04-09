using OOMAC.Domain.Models;
using OOMAC.WPF.Stores;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using static OOMAC.Domain.Models.Contestant;

namespace OOMAC.WPF.ViewModels
{
    public class TournamentAddContestantsViewModel : ViewModelBase
    {
        private ContestantStore _contestantStore;
        private TournamentStore _tournamentStore;

        public TournamentAddContestantsViewModel(TournamentStore tournamentStore, ContestantStore contestantStore) 
        {
            _tournamentStore = tournamentStore;
            _contestantStore = contestantStore;
        }

        public string Loader
        {
            get
            {
                if (_tournamentStore.SelectedTournament.Contestans == null)
                {
                    _tournamentStore.SelectedTournament.Contestans = new List<Contestant>();
                }

                return "";
            }
        }

        public ICommand AssignContestantToTournamentCommand { get; }

        public List<Contestant> TournamentContestantList => _tournamentStore.SelectedTournament.Contestans.ToList();

        private Contestant _selectedContestantTournament;
        public Contestant SelectedContestantTournament
        {
            get
            {
                return _selectedContestantTournament;
            }
            set
            {
                _selectedContestantTournament = value;
                OnPropertyChanged(nameof(SelectedContestantTournament));
            }
        }

        public List<Contestant> ContestantList => _contestantStore.Contestants
                                                                   .Except(TournamentContestantList)
                                                                   .Where(s =>
                                                                          s.Age >= _tournamentStore.SelectedTournament.MinAge &&
                                                                          s.Age <= _tournamentStore.SelectedTournament.MaxAge &&
                                                                          s.TechSkill >= _tournamentStore.SelectedTournament.MinTechnicalSkill &&
                                                                          s.TechSkill <= _tournamentStore.SelectedTournament.MaxTechnicalSkill)
                                                                   .ToList();

        private Contestant _selectedContestant;
        public Contestant SelectedContestant
        {
            get
            {
                return _selectedContestant;
            }
            set
            {
                _selectedContestant = value;
                OnPropertyChanged(nameof(SelectedContestant));
            }
        }

        public string TitleName => _tournamentStore.SelectedTournament.Name;

        public int MinAge => _tournamentStore.SelectedTournament.MinAge;
        public int MaxAge => _tournamentStore.SelectedTournament.MaxAge;
        public string MinTechnicalSkill => GetEnumDescription(_tournamentStore.SelectedTournament.MaxTechnicalSkill);
        public string MaxTechnicalSkill => GetEnumDescription(_tournamentStore.SelectedTournament.MinTechnicalSkill);
    }
}
