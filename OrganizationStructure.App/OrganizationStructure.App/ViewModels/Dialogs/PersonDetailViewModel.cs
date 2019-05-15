using OrganizationStructure.App.Models;
using OrganizationStructure.App.Services;
using OrganizationStructure.App.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OrganizationStructure.App.ViewModels
{
    class PersonDetailViewModel : Base.BaseViewModel
    {
        private OrganizationStructureService _StructureService;
        private string _FirstName;
        private string _LastName;
        private Role _SelectedRole;

        public PersonDetailViewModel(int? personId)
        {
            this.PersonId = personId;

            if (this.PersonId != null)
            {
                EditMode = true;

                var person = StructureService.GetPerson(personId.Value);
                LastName = person.LastName;
                FirstName = person.FirstName;
            }

            SavePersonCommand = new RelayCommand<System.Windows.Window>(SavePersonAction);
        }

        public ICommand SavePersonCommand { get; set; }

        public int? PersonId { get; private set; }
        public bool EditMode { get; set; }
        public string FirstName
        {
            get { return _FirstName; }
            set
            {
                SetProperty(ref _FirstName, value);
            }
        }
        public string LastName
        {
            get { return _LastName; }
            set
            {
                SetProperty(ref _LastName, value);
            }
        }
        public Role SelectedRole
        {
            get { return _SelectedRole; }
            set
            {
                SetProperty(ref _SelectedRole, value);
            }
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

        public ObservableCollection<Role> Roles
        {
            get { return GlobalInstance.Instance.Model.Roles; }
        }

        private void SavePersonAction(System.Windows.Window window)
        {
            if (EditMode)
            {
                var person = StructureService.GetPerson(this.PersonId.Value);
                person.FirstName = FirstName;
                person.LastName = LastName;
                if (SelectedRole != null)
                {
                    person.RoleId = SelectedRole.Id;
                }
                person.UpdateModel();
                var allStructures = StructureService.GetAllStructures();
                if (allStructures != null)
                {
                    foreach (var item in allStructures)
                    {
                        if (item.PersonId == person.Id)
                        {
                            item.UpdateModel();
                        }
                    }
                }
            }
            else
            {
                int? roleId = null;
                if (SelectedRole != null)
                {
                    roleId = SelectedRole.Id;
                }

                var person = new Person()
                {
                    Id = StructureService.GetNewPersonId(),
                    FirstName = FirstName,
                    LastName = LastName,
                    RoleId = roleId,
                };
                StructureService.AddPerson(person);
            }

            CloseWindow(window);
        }

        private void CloseWindow(System.Windows.Window window)
        {
            if (window != null)
            {
                window.Close();
            }
        }
    }
}
