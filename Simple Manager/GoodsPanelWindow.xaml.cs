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

namespace Simple_Manager
{
    public partial class GoodsManagerWindow : Window
    {
        private GoodsManager goodsManager;

        public GoodsManagerWindow()
        {
            InitializeComponent();
            goodsManager = new GoodsManager(this);
        }

        private void AddButton_Click(object sender, RoutedEventArgs e) { goodsManager.Add(); }

        private void ChangeButton_Click(object sender, RoutedEventArgs e) { goodsManager.Change(); }

        private void RemoveButton_Click(object sender, RoutedEventArgs e) { goodsManager.Remove(); }

        private void DBDataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.Column.Header.ToString() == "Id") e.Column.Width = 0;
            if (e.Column.Header.ToString() == "FromDate") (e.Column as DataGridTextColumn).Binding.StringFormat = "yyyy-MM-dd";
            if (e.Column.Header.ToString() == "ToDate") (e.Column as DataGridTextColumn).Binding.StringFormat = "yyyy-MM-dd";
        }

        private void GoodsPanel_Closed(object sender, EventArgs e) { goodsManager.Close(); }
    }
}
