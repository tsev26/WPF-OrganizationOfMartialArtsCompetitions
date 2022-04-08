using OOMAC.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOMAC.WPF.Stores
{
    public class TournamentStore
    {
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

        private Contestant _selectedTournament;
        public Contestant SelectedTournament
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
