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
    public class RolesViewModel : Base.BaseViewModel
    {
        private OrganizationStructureService mainModelService;
        private Role _selectedRole;

        public RolesViewModel()
        {
            //GlobalDataModel.Instance.Model.PropertyChanged += Model_PropertyChanged;
            DeleteRoleCommand = new RelayCommand<int>(DeleteRoleAction);
            EditRoleCommand = new RelayCommand<int>(EditRoleAction);
            AddRoleCommand = new RelayCommandBase(AddRoleAction);
        }

        public ICommand AddRoleCommand { get; set; }
        public ICommand EditRoleCommand { get; set; }
        public ICommand DeleteRoleCommand { get; set; }

        public OrganizationStructureService MainModelService
        {
            get
            {
                if (mainModelService == null)
                {
                    mainModelService = new OrganizationStructureService();
                }
                return mainModelService;
            }
        }
        public Role SelectedRole
        {
            get
            {
                return _selectedRole;
            }
            set
            {
                _selectedRole = value;

                if (_selectedRole == null)
                    return;
                OnPropertyChanged();

                //OnPropertyChanged(nameof(Persons));
                //SelectedCommand.Execute(_selectedItem);

                //SelectedItem = null;
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

        private void AddRoleAction()
        {
            RoleDetailView roleDetail = new RoleDetailView();
            roleDetail.Closed += RoleDetailDialog_Closed;
            roleDetail.Show();
        }

        private void EditRoleAction(int roleId)
        {
            RoleDetailView personDetail = new RoleDetailView((int)roleId);
            personDetail.Closed += RoleDetailDialog_Closed;
            personDetail.Show();
        }

        private void RoleDetailDialog_Closed(object sender, EventArgs e)
        {
            OnPropertyChanged("");
        }

        private void DeleteRoleAction(int selectedRoleId)
        {
            var selectedRole = Roles.Where(x => x.Id == selectedRoleId).FirstOrDefault();
            MessageBoxResult msgResult = MessageBox.Show($"Are you sure delete - Role:  {selectedRole.ToString()}", "Delete role", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if(msgResult == MessageBoxResult.Yes)
            {
                DeleteRole(selectedRoleId);
            }
        }

        private void DeleteRole(int roleId)
        {
            MainModelService.DeleteRole(roleId);
        }
    }
}