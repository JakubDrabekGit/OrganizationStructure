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
    /// Interaction logic for PersonDetailView.xaml
    /// </summary>
    public partial class PersonDetailView : Window
    {
        public PersonDetailView(int? personId = null)
        {
            InitializeComponent();
            this.DataContext = new PersonDetailViewModel(personId);

        }
        
        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}