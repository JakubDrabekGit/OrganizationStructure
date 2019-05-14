using OrganizationStructure.App.Models;
using OrganizationStructure.App.Services;
using OrganizationStructure.App.ViewModels.Base;
using System;
using System.Collections.Generic;
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

        private void SavePersonAction(System.Windows.Window window)
        {
            if (EditMode)
            {
                var person = StructureService.GetPerson(this.PersonId.Value);
                person.FirstName = FirstName;
                person.LastName = LastName;
                person.UpdateModel();
                foreach (var item in StructureService.GetAllStructures())
                {
                    if (item.PersonId == person.Id)
                    {
                        item.UpdateModel();
                    }
                }
            }
            else
            {
                var person = new Person()
                {
                    Id = StructureService.GetNewPersonId(),
                    FirstName = FirstName,
                    LastName = LastName
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
