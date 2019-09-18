using System.Windows.Controls;
using System.Data;
using System.Windows.Media;


namespace Simple_Manager
{
    internal class GoodsManager
    {
        private string cnnStr = "Data Source=(local)\\SQLEXPRESS;Initial Catalog = SimpleManagerDB; Integrated Security = True";
        internal int status = 0;
        private EditorWindow editorWindow;
        private GoodsManagerWindow window;
        internal SqlManager sql = new SqlManager();

        public GoodsManager(GoodsManagerWindow goodsWindow)
        {
            window = goodsWindow;
            status = sql.Connect(cnnStr);
            Update();
        }

        public void Close() { if (editorWindow != null) editorWindow.Close(); }

        public void Add()
        {
            window.IsHitTestVisible = false;

            editorWindow = new EditorWindow(this, sql);
            editorWindow.Show();

            return;
        }

        public void Change()
        {
            window.IsHitTestVisible = false;

            if (window.DBDataGrid.SelectedIndex != -1)
            {
                DataGridRow row = (DataGridRow)window.DBDataGrid.ItemContainerGenerator.ContainerFromIndex(window.DBDataGrid.SelectedIndex);
                DataGridCell RowColumn = window.DBDataGrid.Columns[0].GetCellContent(row).Parent as DataGridCell;
                string idValue = ((TextBlock)RowColumn.Content).Text;

                RowColumn = window.DBDataGrid.Columns[1].GetCellContent(row).Parent as DataGridCell;
                string nameValue = ((TextBlock)RowColumn.Content).Text;

                RowColumn = window.DBDataGrid.Columns[2].GetCellContent(row).Parent as DataGridCell;
                string priceValue = ((TextBlock)RowColumn.Content).Text;

                RowColumn = window.DBDataGrid.Columns[3].GetCellContent(row).Parent as DataGridCell;
                string fdateValue = ((TextBlock)RowColumn.Content).Text;

                RowColumn = window.DBDataGrid.Columns[4].GetCellContent(row).Parent as DataGridCell;
                string tdateValue = ((TextBlock)RowColumn.Content).Text;

                editorWindow = new EditorWindow(this, sql, idValue, nameValue, priceValue, fdateValue, tdateValue);
                editorWindow.Show();
            }
            else
            {
                status = 6;
                Update();
            }

            return;
        }

        public void Remove()
        {
            if (window.DBDataGrid.SelectedItem != null)
            {
                DataGridRow row = (DataGridRow)window.DBDataGrid.ItemContainerGenerator.ContainerFromIndex(window.DBDataGrid.SelectedIndex);
                DataGridCell rowColumn = window.DBDataGrid.Columns[0].GetCellContent(row).Parent as DataGridCell;
                string value = ((TextBlock)rowColumn.Content).Text;

                status = sql.Remove(value, "goods");
            }
            else status = 6;

            Update();
            return;
        }

        public void Update()
        {
            DataTable dt = sql.GetTable("goods");
            window.IsHitTestVisible = true;

            if (dt != null) window.DBDataGrid.ItemsSource = dt.AsDataView();
            else status = 1;

            UpdateStatus();
        }

        private void UpdateStatus()
        {
            switch (status)
            {
                case -4:
                    {
                        window.StatusLabel.Foreground = Brushes.Green;
                        window.StatusLabel.Content = "Insert succesfull!";
                        break;
                    }
                case -3:
                    {
                        window.StatusLabel.Foreground = Brushes.Green;
                        window.StatusLabel.Content = "Replace succesfull!";
                        break;
                    }
                case -2:
                    {
                        window.StatusLabel.Foreground = Brushes.Green;
                        window.StatusLabel.Content = "Table change canceled!";
                        break;
                    }
                case -1:
                    {
                        window.StatusLabel.Foreground = Brushes.Green;
                        window.StatusLabel.Content = "Remove succesfull!";
                        break;
                    }
                case 1:
                    {
                        window.StatusLabel.Foreground = Brushes.Red;
                        window.StatusLabel.Content = "An error has occurred! Trouble with DB connect!";
                        break;
                    }
                case 2:
                    {
                        window.StatusLabel.Foreground = Brushes.Red;
                        window.StatusLabel.Content = "An error has occurred! Trouble with DB disconnect!";
                        break;
                    }
                case 3:
                    {
                        window.StatusLabel.Foreground = Brushes.Red;
                        window.StatusLabel.Content = "An error has occurred! Trouble with DB connection!";
                        break;
                    }
                case 4:
                    {
                        window.StatusLabel.Foreground = Brushes.Red;
                        window.StatusLabel.Content = "An error has occurred! Nothing selected!";
                        break;
                    }
                case 5:
                    {
                        window.StatusLabel.Foreground = Brushes.Red;
                        window.StatusLabel.Content = "An error has occurred! Wrong input!";
                        break;
                    }
                case 6:
                    {
                        window.StatusLabel.Foreground = Brushes.Red;
                        window.StatusLabel.Content = "An error has occurred! Nothing selected!";
                        break;
                    }
                default:
                    {
                        window.StatusLabel.Foreground = Brushes.Green;
                        window.StatusLabel.Content = "DB loaded!";
                        break;
                    }
            }
        }
    }
}
