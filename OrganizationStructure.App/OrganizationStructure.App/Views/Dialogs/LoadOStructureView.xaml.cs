using OrganizationStructure.App.Models.Enums;
using OrganizationStructure.App.ViewModels.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace OrganizationStructure.App.Views.Dialogs
{
    /// <summary>
    /// Interaction logic for LoadOStructureView.xaml
    /// </summary>
    public partial class LoadOStructureView : Window
    {
        public LoadOStructureView(LoadType loadType)
        {
            InitializeComponent();
            this.DataContext = new LoadOStructureViewModel(loadType);
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
