using OrganizationStructure.App.Models;
using OrganizationStructure.App.Services;
using OrganizationStructure.App.ViewModels.Base;
using OrganizationStructure.App.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace OrganizationStructure.App.ViewModels
{
    public class StructureMainViewModel : Base.BaseViewModel
    {
        private Base.BaseViewModel _SubCurrentVM;
        private OrganizationStructureService _StructureService;
        private StructureViewModel _StructureVM;
        private PersonsViewModel _PersonsVM;
        private RolesViewModel _RolesVM;

        public StructureMainViewModel()
        {
            LoadOrganizationStructureViewCommand = new RelayCommandBase(LoadOrganizationStructureAction);
            LoadPersonViewCommand = new RelayCommandBase(LoadPersonAction);
            LoadRoleViewCommand = new RelayCommandBase(LoadRoleAction);
            SaveToLocalCommand = new RelayCommand(SaveToLocalAction);
            SaveToServerCommand = new RelayCommand(SaveToServerAction);

            CompanyName = GlobalInstance.Instance.Model.StructureModel.CompanyName;
            CreatedDate = GlobalInstance.Instance.Model.StructureModel.CreatedDate;

            LoadOrganizationStructureAction();
        }

        public ICommand LoadOrganizationStructureViewCommand { get; set; }
        public ICommand LoadPersonViewCommand { get; set; }
        public ICommand LoadRoleViewCommand { get; set; }
        public ICommand SaveToServerCommand { get; set; }
        public ICommand SaveToLocalCommand { get; set; }

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

        public Base.BaseViewModel SubCurrentViewModel
        {
            get
            {
                return _SubCurrentVM;
            }
            set
            {
                _SubCurrentVM = value;
                OnPropertyChanged();
            }
        }

        public StructureViewModel StructureVM
        {
            get
            {
                if (_StructureVM == null)
                {
                    _StructureVM = new StructureViewModel();
                }
                return _StructureVM;
            }
            set
            {
                _StructureVM = value;
            }
        }

        public PersonsViewModel PersonsVM
        {
            get
            {
                if (_PersonsVM == null)
                {
                    _PersonsVM = new PersonsViewModel();
                }
                return _PersonsVM;
            }
            set
            {
                _PersonsVM = value;
            }
        }

        public RolesViewModel RolesVM
        {
            get
            {
                if (_RolesVM == null)
                {
                    _RolesVM = new RolesViewModel();
                }
                return _RolesVM;
            }
            set
            {
                _RolesVM = value;
            }
        }

        public string CompanyName { get; private set; }
        public DateTime CreatedDate { get; private set; }

        private void LoadOrganizationStructureAction()
        {
            SubCurrentViewModel = StructureVM;
        }

        private void LoadPersonAction()
        {
            SubCurrentViewModel = PersonsVM;
        }

        private void LoadRoleAction()
        {
            SubCurrentViewModel = RolesVM;
        }

        private void SaveToLocalAction(object data)
        {
            StructureService.SaveToLocal();

            MessageBox.Show("Save to local is successfull.", "Save to local", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void SaveToServerAction(object data)
        {
            StructureService.SaveToServer();

            MessageBox.Show("Save to server is successfull.", "Save to server", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
