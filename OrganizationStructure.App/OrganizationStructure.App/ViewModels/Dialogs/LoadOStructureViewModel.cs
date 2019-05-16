using OrganizationStructure.App.Models;
using OrganizationStructure.App.Models.Enums;
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

namespace OrganizationStructure.App.ViewModels.Dialogs
{
    public class LoadOStructureViewModel : Base.BaseViewModel
    {
        private OrganizationStructureService _StructureService;
        private ObservableCollection<OrganizationStructureModel> _OStructures;
        private OrganizationStructureModel _SelectedOStructure;

        public LoadOStructureViewModel(LoadType loadType)
        {
            LoadCommand = new RelayCommand<System.Windows.Window>(LoadAction);

            InitData(loadType);
        }

        private void InitData(LoadType loadType)
        {
            LoadType = loadType;
            List<OrganizationStructureModel> listStructures = new List<OrganizationStructureModel>();
            switch (LoadType)
            {
                case LoadType.LoadFromServer:
                    listStructures = StructureService.LoadStructureModelsFromServer();
                    break;
                case LoadType.LoadFromLocal:
                    listStructures = StructureService.LoadStructureModelsFromLocal();
                    break;
                default:
                    throw new NotImplementedException("LoadType");
                    
            }

            OStructures = new ObservableCollection<OrganizationStructureModel>(listStructures);
        }

        public ICommand LoadCommand { get; set; }
        public LoadType LoadType { get; set; }

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
        
        public ObservableCollection<OrganizationStructureModel> OStructures
        {
            get
            {
                return _OStructures;
            }
            set
            {
                _OStructures = value;

                if (_OStructures == null)
                    return;
                OnPropertyChanged();
            }
        }
        
        public OrganizationStructureModel SelectedOStructure
        {
            get
            {
                return _SelectedOStructure;
            }
            set
            {
                _SelectedOStructure = value;

                if (_SelectedOStructure == null)
                    return;
                OnPropertyChanged();
            }
        }

        private void LoadAction(System.Windows.Window window)
        {
            if (SelectedOStructure == null)
            {
                MessageBox.Show("Structure file is not selected.", "Load Structure", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                switch (LoadType)
                {
                    case LoadType.LoadFromServer:
                        StructureService.LoadModelToAppModelFromServer(SelectedOStructure.Id);
                        break;
                    case LoadType.LoadFromLocal:
                        StructureService.LoadModelToApp(SelectedOStructure.Id);
                        break;
                    default:
                        break;
                }
                
                window.DialogResult = true;
                window.Close();
            }
        }
    }
}