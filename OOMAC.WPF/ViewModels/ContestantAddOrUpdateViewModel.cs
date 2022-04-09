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
        public ContestantAddOrUpdateViewModel(ContestantStore contestantStore, INavigationService contestantNavigationService) 
        {
            _contestantStore = contestantStore;

            CreateNewContestantCommand = new CreateNewContestantCommand(this, contestantNavigationService);

            _contestantStore.ContestantSelectionChange += ContestantSelectionChange;
        }

        public ICommand CreateNewContestantCommand { get; }

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

        private double technicalSkillDouble;

        public double TechnickalSkillDouble
        {
            get
            {
                return technicalSkillDouble;
            }
            set
            {
                technicalSkillDouble = value;
                OnPropertyChanged(nameof(TechnickalSkillDouble));
                OnPropertyChanged(nameof(TechnicalSkill));
                OnPropertyChanged(nameof(TechnicalSkillString));
            }
        }

        public TechnicalSkill TechnicalSkill => (TechnicalSkill)TechnickalSkillDouble;


        public string TechnicalSkillString => GetEnumDescription(TechnicalSkill);

        private void ContestantSelectionChange()
        {
            FirstName = _contestantStore.SelectedContestant.FirstName;
            LastName = _contestantStore.SelectedContestant.LastName;
            Email = _contestantStore.SelectedContestant.Email;
            DateBorn = _contestantStore.SelectedContestant.DateBorn;
            TechnickalSkillDouble = (double)_contestantStore.SelectedContestant.TechSkill;
        }


    }
}
