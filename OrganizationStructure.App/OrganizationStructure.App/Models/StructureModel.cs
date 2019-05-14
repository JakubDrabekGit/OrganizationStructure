using OrganizationStructure.App.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationStructure.App.Models
{
    public class StructureModel : ViewModels.Base.BaseViewModel, IModel
    {
        private int _Id;
        private string name;
        private string _Description;
        private int _PersonId;
        private OrganizationStructureService _StructureService;

        public int Id
        {
            get { return _Id; }
            set
            {
                SetProperty(ref _Id, value);
            }
        }

        //public bool IsRoot { get; set; }
        public string Name
        {
            get { return name; }
            set
            {
                SetProperty(ref name, value);
            }
        }

        public string Description
        {
            get { return _Description; }
            set
            {
                SetProperty(ref _Description, value);
            }
        }

        public int PersonId
        {
            get { return _PersonId; }
            set
            {
                SetProperty(ref _PersonId, value);
                OnPropertyChanged(nameof(PersonFullName));
                OnPropertyChanged(nameof(PersonRole));
            }
        }
      
        public string PersonFullName
        {
            get
            {
                var personFullname = StructureService.GetPersonFullName(PersonId);
                if (personFullname != null)
                {
                    return personFullname;
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public string PersonRole
        {
            get
            {
                var rolename = StructureService.GetPersonRole(PersonId);
                if (rolename != null)
                {
                    return rolename;
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public ObservableCollection<StructureModel> SubStructures { get; set; } = new ObservableCollection<StructureModel>();

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

        public void UpdateModel()
        {
            OnPropertyChanged("");
        }
        public override string ToString()
        {
            return $"{Name} {PersonFullName} {PersonRole}";
        }
    }
}
