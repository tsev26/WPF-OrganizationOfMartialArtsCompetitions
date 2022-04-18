using OOMAC.Domain.Models;
using OOMAC.Domain.Models.Calculating;
using OOMAC.EF.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOMAC.WPF.Stores
{
    public class TournamentStore
    {

        private readonly TournamentDataService _tournamentService;

        public TournamentStore(TournamentDataService tournamentService)
        {
            _tournamentService = tournamentService;
        }

        public event Action TournamentStoreChange;

        private List<Tournament> _tournaments;
        public List<Tournament> Tournaments
        {
            get
            {
                return _tournaments;
            }
            set
            {
                _tournaments = value;
                TournamentStoreChange?.Invoke();
            }
        }

        public event Action TournamentSelectionChange;

        public async Task LoadAsync(params object[] arguments)
        {
            IEnumerable<Tournament> users = await _tournamentService.GetAll();

            Tournaments = users.ToList();
        }

        public async Task UpdateSelectedAsync(int tournamentId)
        {
            Tournament selectedTournament = _tournamentService.Get(tournamentId);
            SelectedTournament = selectedTournament;
        }

        public void Unselect()
        {
            SelectedTournament = null;
        }

        private Tournament _selectedTournament;
        public Tournament SelectedTournament
        {
            get
            {
                return _selectedTournament;
            }
            set
            {
                _selectedTournament = value;
                TournamentSelectionChange?.Invoke();
            }
        }

    }
}
