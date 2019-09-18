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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Simple_Manager
{
    public partial class MainWindow : Window
    {
        LoginManager loginManager;

        public MainWindow()
        {
            InitializeComponent();

            loginManager = new LoginManager(this);
        }

        private void AcceptButton_Click(object sender, RoutedEventArgs e) { loginManager.Proceed(); }

        private void RejectButton_Click(object sender, RoutedEventArgs e) { loginManager.Update(); }
    }
}
