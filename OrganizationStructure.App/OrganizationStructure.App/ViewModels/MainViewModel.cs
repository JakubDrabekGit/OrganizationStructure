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
        private StructureMainViewModel _OStructureMainVM;
        private StartViewModel _StartVM;

        public MainViewModel()
        {
            CreateCommand = new RelayCommandBase(CreateAction);
            LoadFromServerCommand = new RelayCommandBase(LoadFromServerAction);
            LoadFromLocalCommand = new RelayCommandBase(LoadFromLocalAction);

            CurrentViewModel = StartVM;
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

        public StartViewModel StartVM
        {
            get
            {
                if (_StartVM == null)
                {
                    _StartVM = new StartViewModel();
                }
                return _StartVM;
            }
            set
            {
                _StartVM = value;
            }
        }

        public StructureMainViewModel OStructureMainVM
        {
            get
            {
                if (_OStructureMainVM == null)
                {
                    _OStructureMainVM = new StructureMainViewModel();
                }
                return _OStructureMainVM;
            }
            set
            {
                _OStructureMainVM = value;
            }
        }

        private void CreateAction()
        {
            CreateOStructureView createOSDialog = new CreateOStructureView();
            bool? dialogResult = createOSDialog.ShowDialog();

            if (dialogResult.HasValue && dialogResult.Value)
            {
                CurrentViewModel = OStructureMainVM;
            }
        }

        private void LoadFromServerAction()
        {
            CurrentViewModel = new StartViewModel();
            LoadOStructureView loadOSDialog = new LoadOStructureView(Models.Enums.LoadType.LoadFromServer);
            bool? dialogResult = loadOSDialog.ShowDialog();

            if (dialogResult.HasValue && dialogResult.Value)
            {
                CurrentViewModel = OStructureMainVM;
            }
        }

        private void LoadFromLocalAction()
        {
            CurrentViewModel = new StartViewModel();
            LoadOStructureView loadOSDialog = new LoadOStructureView(Models.Enums.LoadType.LoadFromLocal);
            bool? dialogResult = loadOSDialog.ShowDialog();

            if (dialogResult.HasValue && dialogResult.Value)
            {
                CurrentViewModel = OStructureMainVM;
            }
        }
    }
}