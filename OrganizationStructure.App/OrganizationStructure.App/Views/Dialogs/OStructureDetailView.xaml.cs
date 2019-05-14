using OrganizationStructure.App.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace OrganizationStructure.App.Views
{
    /// <summary>
    /// Interaction logic for OStructureDetailView.xaml
    /// </summary>
    public partial class OStructureDetailView : Window
    {
        public OStructureDetailView(bool editMode, int? OStructureId = null)
        {
            InitializeComponent();
            this.DataContext = new OStructureDetailViewModel(OStructureId, editMode);

        }

        
        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            
            this.Close();
        }
      
    }
}
