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
    public class StructureViewModel : Base.BaseViewModel
    {
        private OrganizationStructureService _StructureService;
        private StructureModel _selectedStructureNode;

        public StructureViewModel()
        {
            AddCommand = new RelayCommand<int?>(AddStructureAction);
            EditCommand = new RelayCommand<int>(EditStructureAction);
            DeleteCommand = new RelayCommand<StructureModel>(DeleteStructureAction);
        }

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

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
        public ObservableCollection<StructureModel> StructureModelRoot
        {
            get
            {
                return GlobalInstance.Instance.Model.StructureModel.RootStructure;

            }
            set
            {
                GlobalInstance.Instance.Model.StructureModel.RootStructure = value;
                OnPropertyChanged();
            }
        }
        
        public StructureModel SelectedStructureNode
        {
            get
            {
                return _selectedStructureNode;
            }
            set
            {
                _selectedStructureNode = value;

                if (_selectedStructureNode == null)
                    return;
                OnPropertyChanged();

                OnPropertyChanged(nameof(IsSelectedStructureNode));
            }
        }

        public bool IsSelectedStructureNode
        {
            get
            {
                if (SelectedStructureNode != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        private void AddStructureAction(int? selectedStructureId)
        {
            OStructureDetailView personDetail = new OStructureDetailView(false, selectedStructureId);
            personDetail.Closed += DetailDialog_Closed;
            personDetail.Show();
        }

        private void EditStructureAction(int selectedStructureId)
        {
            OStructureDetailView personDetail = new OStructureDetailView(true, selectedStructureId);
            personDetail.Closed += DetailDialog_Closed;
            personDetail.Show();
        }

        private void DeleteStructureAction(StructureModel selectedStructureModel)
        {
            //var msgStructure = StructureService.GetStructureModel(selectedStructureId).ToString();
            MessageBoxResult msgResult = MessageBox.Show($"Are you sure delete structure - {selectedStructureModel.ToString()}", "Delete structure", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (msgResult == MessageBoxResult.Yes)
            {
                DeleteStructure(selectedStructureModel);

                SelectedStructureNode = null;
                OnPropertyChanged("");
                //selectedStructureModel.UpdateModel();
            }
        }
        private void DeleteStructure(StructureModel selectedStructureModel)
        {
            StructureService.DeleteStructure(selectedStructureModel);
        }

        private void DetailDialog_Closed(object sender, EventArgs e)
        {
            OnPropertyChanged("");
        }
    }
}