using OOMAC.Domain.Models;
using OOMAC.EF.Services;
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

        private bool IsNewTournament => _tournamentStore.SelectedTournament == null;
        public TournamentAddOrUpdateViewModel(TournamentStore tournamentStore, INavigationService tournamentNavigationService, TournamentDataService tournamentService) 
        {
            _tournamentStore = tournamentStore;

            CreateNewTournamentCommand = new CreateNewTournamentCommand(this, tournamentNavigationService, tournamentService, _tournamentStore);

            //_tournamentStore.TournamentSelectionChange += TournamentSelectionChange;
        }

        public ICommand CreateNewTournamentCommand { get; }

        public string Loader
        {
            get
            {
                Name = _tournamentStore.SelectedTournament?.Name ?? "";
                MinAge = _tournamentStore.SelectedTournament?.MinAge ?? 18;
                MaxAge = _tournamentStore.SelectedTournament?.MaxAge ?? 30;
                MinTechnicalSkillInt = (int)(_tournamentStore.SelectedTournament?.MinTechnicalSkill ?? TechnicalSkill._10kyu);
                MaxTechnicalSkillInt = (int)(_tournamentStore.SelectedTournament?.MaxTechnicalSkill ?? TechnicalSkill._10dan);


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
        public int MinTechnicalSkillInt
        {
            get
            {
                return _minTechnicalSkill;
            }
            set
            {
                _minTechnicalSkill = value;
                OnPropertyChanged(nameof(MinTechnicalSkillInt));
                OnPropertyChanged(nameof(MinTechnicalSkill));
                OnPropertyChanged(nameof(MinTechnicalSkillString));
            }
        }

        public TechnicalSkill MinTechnicalSkill => (TechnicalSkill)MinTechnicalSkillInt;
        public string MinTechnicalSkillString => GetEnumDescription(MinTechnicalSkill);

        private int _maxTechnicalSkill;
        public int MaxTechnicalSkillInt
        {
            get
            {
                return _maxTechnicalSkill;
            }
            set
            {
                _maxTechnicalSkill = value;
                OnPropertyChanged(nameof(MaxTechnicalSkill));
                OnPropertyChanged(nameof(MaxTechnicalSkillInt));
                OnPropertyChanged(nameof(MaxTechnicalSkillString));
            }
        }

        public TechnicalSkill MaxTechnicalSkill => (TechnicalSkill)MaxTechnicalSkillInt;
        public string MaxTechnicalSkillString => GetEnumDescription(MaxTechnicalSkill);

        public string ButtonName => IsNewTournament ? "Vytvořit" : "Upravit";

        public string TitleName => IsNewTournament ? "Nový turnaj" : "Úprava turnaje";

        /*
        private void TournamentSelectionChange()
        {
            Name = _tournamentStore.SelectedTournament?.Name ?? "";
            MinAge = _tournamentStore.SelectedTournament?.MinAge ?? 18;
            MaxAge = _tournamentStore.SelectedTournament?.MaxAge ?? 30;
            MinTechnicalSkillInt = (int)(_tournamentStore.SelectedTournament?.MinTechnicalSkill ?? TechnicalSkill._10kyu);
            MaxTechnicalSkillInt = (int)(_tournamentStore.SelectedTournament?.MaxTechnicalSkill ?? TechnicalSkill._10dan);
        }
        */
    }
}
