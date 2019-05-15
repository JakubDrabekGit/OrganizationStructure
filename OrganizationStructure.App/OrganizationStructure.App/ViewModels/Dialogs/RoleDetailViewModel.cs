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
    class RoleDetailViewModel : Base.BaseViewModel
    {
        private string _RoleName;
        private string _RoleDescription;
        private OrganizationStructureService _MainModelService;

        public ICommand SaveRoleCommand { get; set; }
        public int? RoleId { get; private set; }
        public bool EditMode { get; set; }

        public string RoleName
        {
            get { return _RoleName; }
            set
            {
                SetProperty(ref _RoleName, value);
            }
        }

        public string RoleDescription
        {
            get { return _RoleDescription; }
            set
            {
                SetProperty(ref _RoleDescription, value);
            }
        }

        public OrganizationStructureService MainModelService
        {
            get
            {
                if (_MainModelService == null)
                {
                    _MainModelService = new OrganizationStructureService();
                }
                return _MainModelService;
            }
        }

        public RoleDetailViewModel(int? roleId)
        {
            this.RoleId = roleId;

            if (this.RoleId.HasValue)
            {
                EditMode = true;

                var role = MainModelService.GetRole(this.RoleId.Value);
                RoleName = role.Name;
                RoleDescription = role.Description;
            }

            SaveRoleCommand = new RelayCommand<System.Windows.Window>(SaveRoleAction);

        }
        

        private void SaveRoleAction(System.Windows.Window window)
        {
            if (EditMode)
            {
                var role = MainModelService.GetRole(this.RoleId.Value);
                role.Name = RoleName;
                role.Description = RoleDescription;
                role.UpdateModel();
            }
            else
            {
                var role = new Role()
                {
                    Id = MainModelService.GetNewRoleId(),
                    Name = RoleName,
                    Description = RoleDescription
                };
                MainModelService.AddRole(role);
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
