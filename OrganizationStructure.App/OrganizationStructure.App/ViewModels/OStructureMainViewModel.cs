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
    public class OStructureMainViewModel : Base.BaseViewModel
    {
        private Base.BaseViewModel _SubCurrentViewModel;
        private OrganizationStructureService _StructureService;
        
        public OStructureMainViewModel()
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
                return _SubCurrentViewModel;
            }
            set
            {
                _SubCurrentViewModel = value;
                OnPropertyChanged();
            }
        }

        public string CompanyName { get; private set; }
        public DateTime CreatedDate { get; private set; }

        private void LoadOrganizationStructureAction()
        {
            SubCurrentViewModel = new OStructureControlViewModel();
        }

        private void LoadPersonAction()
        {
            SubCurrentViewModel = new PersonsViewModel();
        }

        private void LoadRoleAction()
        {
            SubCurrentViewModel = new RolesViewModel();
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
