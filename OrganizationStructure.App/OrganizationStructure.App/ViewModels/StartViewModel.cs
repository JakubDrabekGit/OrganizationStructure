using OrganizationStructure.App.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OrganizationStructure.App.ViewModels
{
    public class StartViewModel :Base.BaseViewModel
    {
        public ICommand CreateCommand { get; set; }
        public ICommand LoadCommand { get; set; }
        public StartViewModel()
        {
            CreateCommand = new RelayCommand(Create);
            LoadCommand = new RelayCommand(Load);
        }

        private void Create(object data)
        {

        }

        private void Load(object data)
        {

        }
    }
}
