using OrganizationStructure.App.Models;
using OrganizationStructure.App.Services;
using OrganizationStructure.App.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace OrganizationStructure.App.ViewModels.Dialogs
{
    public class CreateOStructureViewModel : Base.BaseViewModel
    {
        private OrganizationStructureService _StructureService;

        public CreateOStructureViewModel()
        {
            CreateCommand = new RelayCommand<System.Windows.Window>(CreateOrganizationStructureAction);
        }
        public ICommand CreateCommand { get; set; }
        public string CompanyName { get; set; }

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
        private void CreateOrganizationStructureAction(System.Windows.Window window)
        {
            StructureService.CreateOrganizationStructure(CompanyName);

            window.DialogResult = true;
            window.Close();
        }
    }
}
