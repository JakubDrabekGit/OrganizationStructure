using OrganizationStructure.App.Models;
using OrganizationStructure.App.Services;
using OrganizationStructure.App.ViewModels.Base;
using OrganizationStructure.App.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace OrganizationStructure.App.ViewModels
{
    public class PersonsViewModel : Base.BaseViewModel
    {
        private OrganizationStructureService _StructureService;
        private Person _selectedPerson;

        public PersonsViewModel()
        {
            DeletePersonCommand = new RelayCommand<int>(DeletePersonAction);
            EditPersonCommand = new RelayCommand<int>(EditPersonAction);
            AddPersonCommand = new RelayCommandBase(AddPersonAction);
        }

        public OrganizationStructureService StructureService
        {
            get
            {
                if (_StructureService == null)
                {
                    _StructureService = new OrganizationStructureService();
                }
                return _StructureService;
            }
        }

        public ICommand AddPersonCommand { get; set; }
        public ICommand EditPersonCommand { get; set; }
        public ICommand DeletePersonCommand { get; set; }
        
        public Person SelectedPerson
        {
            get
            {
                return _selectedPerson;
            }
            set
            {
                _selectedPerson = value;

                if (_selectedPerson == null)
                    return;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Person> Persons
        {
            get { return GlobalInstance.Instance.Model.Persons; }
            set
            {
                SetProperty(ref GlobalInstance.Instance.Model.Persons, value);
            }
        }

        public ObservableCollection<Role> Roles
        {
            get { return GlobalInstance.Instance.Model.Roles; }
            set
            {
                SetProperty(ref GlobalInstance.Instance.Model.Roles, value);
            }
        }

        private void AddPersonAction()
        {
            PersonDetailView personDetail = new PersonDetailView();
            personDetail.Closed += PersonDetailDialog_Closed;
            personDetail.Show();
        }

        private void EditPersonAction(int personId)
        {
            PersonDetailView personDetail = new PersonDetailView((int)personId);
            personDetail.Closed += PersonDetailDialog_Closed;
            personDetail.Show();
        }

        private void PersonDetailDialog_Closed(object sender, EventArgs e)
        {
            OnPropertyChanged("");
        }
        private void DeletePersonAction(int selectedPersonId)
        {
            var personFullName = StructureService.GetPerson(selectedPersonId).FullName;
            MessageBoxResult msgResult = MessageBox.Show($"Are you sure delete person {personFullName}", "Delete person", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if(msgResult == MessageBoxResult.Yes)
            {
                DeletePerson(selectedPersonId);
            }
        }

        private void DeletePerson(int personId)
        {
            StructureService.DeletePerson(personId);
        }      
    }
}