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
    /// Interaction logic for CreateOStructureView.xaml
    /// </summary>
    public partial class CreateOStructureView : Window
    {
        public CreateOStructureView()
        {
            InitializeComponent();
            this.DataContext = new CreateOStructureViewModel();
        }


        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
