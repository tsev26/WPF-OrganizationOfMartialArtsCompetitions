using OOMAC.Domain.Models;
using OOMAC.EF.Services;
using OOMAC.WPF.Commands;
using OOMAC.WPF.Stores;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using static OOMAC.Domain.Models.Contestant;

namespace OOMAC.WPF.ViewModels
{
    public class TournamentAddContestantsViewModel : ViewModelBase
    {
        private ContestantStore _contestantStore;
        private TournamentStore _tournamentStore;

        public TournamentAddContestantsViewModel(TournamentStore tournamentStore, ContestantStore contestantStore, TournamentDataService tournamentDataService) 
        {
            _tournamentStore = tournamentStore;
            _contestantStore = contestantStore;

            _contestantStore.ContestantStoreChange += ContestantStoreChange;
            _tournamentStore.TournamentSelectionChange += TournamentSelectionChange;

            AssignContestantToTournamentCommand = new AssignContestantToTournamentCommand(this, tournamentDataService);
        }

        private void TournamentSelectionChange()
        {
            OnPropertyChanged(nameof(TournamentContestantList));
            OnPropertyChanged(nameof(ContestantList));
        }

        private void ContestantStoreChange()
        {
            OnPropertyChanged(nameof(ContestantList));
        }

        public string Loader
        {
            get
            {
                _ = _contestantStore.LoadAsync();
                if (_tournamentStore.SelectedTournament.Contestans == null)
                {
                    _tournamentStore.SelectedTournament.Contestans = new List<Contestant>();
                }

                return "";
            }
        }

        public async Task updateViewChange()
        {
            await _tournamentStore.UpdateSelectedAsync(SelectedTournament.Id);
        }

        public ICommand AssignContestantToTournamentCommand { get; }

        public List<Contestant> TournamentContestantList => _tournamentStore.SelectedTournament.Contestans;

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
                                                                   .Where(s =>
                                                                          s.Age >= _tournamentStore.SelectedTournament.MinAge &&
                                                                          s.Age <= _tournamentStore.SelectedTournament.MaxAge &&
                                                                          s.TechSkill >= _tournamentStore.SelectedTournament.MinTechnicalSkill &&
                                                                          s.TechSkill <= _tournamentStore.SelectedTournament.MaxTechnicalSkill &&
                                                                          !TournamentContestantList.Any(p2 => p2.Id == s.Id))
                                                                   //.Except(_tournamentStore.SelectedTournament.Contestans)
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


        public Tournament SelectedTournament => _tournamentStore.SelectedTournament;
        public string TitleName => SelectedTournament.Name;
        public int MinAge => SelectedTournament.MinAge;
        public int MaxAge => SelectedTournament.MaxAge;
        public string MinTechnicalSkill => GetEnumDescription(SelectedTournament.MaxTechnicalSkill);
        public string MaxTechnicalSkill => GetEnumDescription(SelectedTournament.MinTechnicalSkill);
    }
}
