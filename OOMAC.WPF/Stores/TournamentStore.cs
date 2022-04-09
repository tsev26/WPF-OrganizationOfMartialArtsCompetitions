using OOMAC.Domain.Models;
using OOMAC.EF.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOMAC.WPF.Stores
{
    public class TournamentStore
    {

        private readonly GenericDataService<Tournament> _tournamentService;

        public TournamentStore(GenericDataService<Tournament> tournamentService)
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
            SelectedTournament = null;
            IEnumerable<Tournament> users = await _tournamentService.GetAll();

            Tournaments = users.ToList();
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
