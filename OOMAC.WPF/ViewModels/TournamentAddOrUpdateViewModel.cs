using OOMAC.WPF.Commands;
using OOMAC.WPF.Services.Navigations;
using OOMAC.WPF.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static OOMAC.Domain.Models.Contestant;

namespace OOMAC.WPF.ViewModels
{
    public class TournamentAddOrUpdateViewModel : ViewModelBase
    {
        private TournamentStore _tournamentStore;
        public TournamentAddOrUpdateViewModel(TournamentStore tournamentStore, INavigationService tournamentNavigationService) 
        {
            _tournamentStore = tournamentStore;

            CreateNewTournamentCommand = new CreateNewTournamentCommand(this, tournamentNavigationService);

            _tournamentStore.TournamentSelectionChange += TournamentSelectionChange;
        }

        public ICommand CreateNewTournamentCommand { get; }

        public string Loader
        {
            get
            {
                MinAge = 18;
                MaxAge = 35;
                MinTechnicalSkill = 1;
                MaxTechnicalSkill = 20;

                return "";
            }
        }

        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private int _minAge;
        public int MinAge
        {
            get
            {
                return _minAge;
            }
            set
            {
                _minAge = value;
                OnPropertyChanged(nameof(MinAge));
            }
        }

        private int _maxAge;
        public int MaxAge
        {
            get
            {
                return _maxAge;
            }
            set
            {
                _maxAge = value;
                OnPropertyChanged(nameof(MaxAge));
            }
        }

        private int _minTechnicalSkill;
        public int MinTechnicalSkill
        {
            get
            {
                return _minTechnicalSkill;
            }
            set
            {
                _minTechnicalSkill = value;
                OnPropertyChanged(nameof(MinTechnicalSkill));
                OnPropertyChanged(nameof(MinTechnicalSkillString));
            }
        }

        public string MinTechnicalSkillString => GetEnumDescription((TechnicalSkill)MinTechnicalSkill);

        private int _maxTechnicalSkill;
        public int MaxTechnicalSkill
        {
            get
            {
                return _maxTechnicalSkill;
            }
            set
            {
                _maxTechnicalSkill = value;
                OnPropertyChanged(nameof(MaxTechnicalSkill));
                OnPropertyChanged(nameof(MaxTechnicalSkillString));
            }
        }

        public string MaxTechnicalSkillString => GetEnumDescription((TechnicalSkill)MaxTechnicalSkill);

        private void TournamentSelectionChange()
        {
            throw new NotImplementedException();
        }
    }
}
