using OrganizationStructure.App.ViewModels.Base;
using OrganizationStructure.App.Views;
using OrganizationStructure.App.Views.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace OrganizationStructure.App.ViewModels
{
    public class MainViewModel : Base.BaseViewModel
    {
        private Base.BaseViewModel _CurrentViewModel;

        public MainViewModel()
        {
            CreateCommand = new RelayCommandBase(CreateAction);
            LoadFromServerCommand = new RelayCommandBase(LoadFromServerAction);
            LoadFromLocalCommand = new RelayCommandBase(LoadFromLocalAction);

            CurrentViewModel = new StartViewModel();
        }

        public ICommand CreateCommand { get; set; }
        public ICommand LoadFromServerCommand { get; set; }
        public ICommand LoadFromLocalCommand { get; set; }

        public Base.BaseViewModel CurrentViewModel
        {
            get
            {
                return _CurrentViewModel;
            }
            set
            {
                _CurrentViewModel = value;
                OnPropertyChanged();
            }
        }
       
        private void CreateAction()
        {
            CreateOStructureView createOSDialog = new CreateOStructureView();
            bool? dialogResult = createOSDialog.ShowDialog();

            if (dialogResult.HasValue && dialogResult.Value)
            {
                CurrentViewModel = new OStructureMainViewModel();
            }
        }

        private void LoadFromServerAction()
        {
            CurrentViewModel = new StartViewModel();
            LoadOStructureView loadOSDialog = new LoadOStructureView(Models.Enums.LoadType.LoadFromServer);
            bool? dialogResult = loadOSDialog.ShowDialog();

            if (dialogResult.HasValue && dialogResult.Value)
            {
                CurrentViewModel = new OStructureMainViewModel();
            }
        }

        private void LoadFromLocalAction()
        {
            CurrentViewModel = new StartViewModel();
            LoadOStructureView loadOSDialog = new LoadOStructureView(Models.Enums.LoadType.LoadFromLocal);
            bool? dialogResult = loadOSDialog.ShowDialog();

            if (dialogResult.HasValue && dialogResult.Value)
            {
                CurrentViewModel = new OStructureMainViewModel();
            }
        }
    }
}