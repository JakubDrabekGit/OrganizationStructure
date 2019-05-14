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

namespace OrganizationStructure.App.CustomWindows
{
    /// <summary>
    /// Interaction logic for MessageBoxWindow.xaml
    /// </summary>
    public partial class MessageBoxWindow : Window
    {
        public MessageBoxWindow()
        {
            InitializeComponent();
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            //this.MessageResult = WindowMessageBoxResultEnum.Close;
            this.Close();
        }

        private void BtnYes_Click(object sender, RoutedEventArgs e)
        {
            //this.MessageResult = WindowMessageBoxResultEnum.Yes;
            this.Close();
        }

        private void BtnNo_Click(object sender, RoutedEventArgs e)
        {
            //this.MessageResult = WindowMessageBoxResultEnum.No;
            this.Close();
        }
    }
}
