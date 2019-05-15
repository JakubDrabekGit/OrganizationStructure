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
    public class OStructureDetailViewModel : Base.BaseViewModel
    {
        private OrganizationStructureService _StructureService;
        private string _StructureName;
        private string _StructureDescription;
        private Person _SelectedPerson;

        public OStructureDetailViewModel(int? structureId, bool editMode/*, int? parentStructureId*/)
        {
            this.StructureId = structureId;
            this.ParentStructureId = structureId;// parentStructureId;

            EditMode = editMode;
            if (EditMode && this.StructureId.HasValue)
            {
                //load
                StructureModel structureModel = StructureService.GetStructureModel(this.StructureId.Value);

                this.StructureName = structureModel.Name;
                this.StructureDescription = structureModel.Description;
                this.SelectedPerson = StructureService.GetPerson(structureModel.PersonId);
            }

            SaveCommand = new RelayCommand<System.Windows.Window> (SaveAction);
        }

        public int? StructureId { get; private set; }
        public int? ParentStructureId { get; private set; }

        public ICommand SaveCommand { get; set; }
        public bool EditMode { get; set; }
        public string StructureName
        {
            get { return _StructureName; }
            set
            {
                SetProperty(ref _StructureName, value);
            }
        }

        public string StructureDescription
        {
            get { return _StructureDescription; }
            set
            {
                SetProperty(ref _StructureDescription, value);
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

        public ObservableCollection<Person> Persons
        {
            //todo musi vratit jen osoby,ktere jsou volne
            get { return GlobalInstance.Instance.Model.Persons; }
            set
            {
                SetProperty(ref GlobalInstance.Instance.Model.Persons, value);
            }
        }

        public Person SelectedPerson
        {
            get { return _SelectedPerson; }
            set
            {
                SetProperty(ref _SelectedPerson, value);
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

        private void SaveAction(System.Windows.Window window)
        {
            if (EditMode)
            {
                StructureModel structureModel = StructureService.GetStructureModel(this.StructureId.Value);
                structureModel.Name = this.StructureName;
                structureModel.Description = this.StructureDescription;
                if (SelectedPerson != null)
                {
                    structureModel.PersonId = this.SelectedPerson.Id;
                }
              
                structureModel.UpdateModel();
            }
            else
            {
                var structureModel = new StructureModel()
                {
                    Id = StructureService.GetNewStructureId(),
                    Name = this.StructureName,
                    Description = this.StructureDescription,
                    PersonId = this.SelectedPerson.Id,
                };

                if (ParentStructureId.HasValue)
                {
                    var parentStructureModel = StructureService.GetStructureModel(this.ParentStructureId.Value);
                    parentStructureModel.SubStructures.Add(structureModel);

                }
                else
                {
                    GlobalInstance.Instance.Model.StructureModel.RootStructure = new ObservableCollection<StructureModel>();

                    GlobalInstance.Instance.Model.StructureModel.RootStructure.Add(structureModel);
                    structureModel.UpdateModel();
                }

                structureModel.UpdateModel();
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