using OOMAC.Domain.Models;
using OOMAC.EF.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOMAC.WPF.Stores
{
    public class ContestantStore
    {
        private readonly GenericDataService<Contestant> _contestantService;

        public ContestantStore(GenericDataService<Contestant> contestantService)
        {
            _contestantService = contestantService;
        }
        public event Action ContestantStoreChange;

        private List<Contestant> _contestants;
        public List<Contestant> Contestants
        {
            get
            {
                return _contestants;
            }
            set
            {
                _contestants = value;
                ContestantStoreChange?.Invoke();
            }
        }

        public event Action ContestantSelectionChange;

        public async Task LoadAsync(params object[] arguments)
        {
            SelectedContestant = null;
            IEnumerable<Contestant> users = await _contestantService.GetAll();

            Contestants = users.ToList();
        }

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
                ContestantSelectionChange?.Invoke();
            }
        }
    }
}
