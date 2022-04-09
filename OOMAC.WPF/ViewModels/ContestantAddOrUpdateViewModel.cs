using OOMAC.Domain.Models;
using OOMAC.EF.Services;
using OOMAC.WPF.Commands;
using OOMAC.WPF.Services.Navigations;
using OOMAC.WPF.Stores;
using System;
using System.Windows.Input;
using static OOMAC.Domain.Models.Contestant;

namespace OOMAC.WPF.ViewModels
{
    public class ContestantAddOrUpdateViewModel : ViewModelBase
    {
        private ContestantStore _contestantStore;

        private bool IsNewContestant => _contestantStore.SelectedContestant == null;
        public ContestantAddOrUpdateViewModel(ContestantStore contestantStore, INavigationService contestantNavigationService, GenericDataService<Contestant> contestantService) 
        {
            _contestantStore = contestantStore;

            CreateNewContestantCommand = new CreateNewContestantCommand(this, contestantNavigationService, contestantService, _contestantStore);

            //_contestantStore.ContestantSelectionChange += ContestantSelectionChange;
        }

        public ICommand CreateNewContestantCommand { get; }

        public string Loader
        {
            get
            {
                FirstName = _contestantStore.SelectedContestant?.FirstName ?? "";
                LastName = _contestantStore.SelectedContestant?.LastName ?? "";
                Email = _contestantStore.SelectedContestant?.Email ?? "";
                DateBorn = _contestantStore.SelectedContestant?.DateBorn ?? DateTime.Parse("2000-01-01");
                TechnickalSkillInt = (int)(_contestantStore.SelectedContestant?.TechSkill ?? TechnicalSkill._10kyu);


                return "";
            }
        }

        private string _firstName;
        public string FirstName
        {
            get
            {
                return _firstName;
            }
            set
            {
                _firstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }

        private string _lastName;
        public string LastName
        {
            get
            {
                return _lastName;
            }
            set
            {
                _lastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }

        private string _email;
        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        private DateTime _dateBorn;
        public DateTime DateBorn
        {
            get
            {
                return _dateBorn;
            }
            set
            {
                _dateBorn = value;
                OnPropertyChanged(nameof(DateBorn));
            }
        }

        private double technicalSkillInt;

        public double TechnickalSkillInt
        {
            get
            {
                return technicalSkillInt;
            }
            set
            {
                technicalSkillInt = value;
                OnPropertyChanged(nameof(TechnickalSkillInt));
                OnPropertyChanged(nameof(TechnicalSkill));
                OnPropertyChanged(nameof(TechnicalSkillString));
            }
        }

        public TechnicalSkill TechnicalSkill => (TechnicalSkill)TechnickalSkillInt;


        public string TechnicalSkillString => GetEnumDescription(TechnicalSkill);

        public string ButtonName => IsNewContestant ? "Vytvořit" : "Upravit";

        public string TitleName => IsNewContestant ? "Nový závodník" : "Úprava závodníka";

        /*
        private void ContestantSelectionChange()
        {
            FirstName = _contestantStore.SelectedContestant.FirstName;
            LastName = _contestantStore.SelectedContestant.LastName;
            Email = _contestantStore.SelectedContestant.Email;
            DateBorn = _contestantStore.SelectedContestant.DateBorn;
            TechnickalSkillDouble = (double)_contestantStore.SelectedContestant.TechSkill;
        }
        */

    }
}
