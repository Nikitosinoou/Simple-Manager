using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;

namespace Simple_Manager
{
    partial class EditorWindow : Window
    {
        private GoodsManager gm;
        private SqlManager sql;
        private string id = "-1";

        internal EditorWindow(GoodsManager gm, SqlManager sql)
        {
            this.sql = sql;
            this.gm = gm;
            this.gm.status = -2;

            InitializeComponent();
        }

        internal EditorWindow(GoodsManager gm, SqlManager sql, string id, string name, string price, string fdate, string tdate)
        {
            this.sql = sql;
            this.id = id;
            this.gm = gm;

            InitializeComponent();

            NameTextBox.Text = name;
            PriceTextBox.Text = price;
            FDateTextBox.Text = fdate;
            TDateTextBox.Text = tdate;
        }

        private void AcceptButton_Click(object sender, RoutedEventArgs e)
        {
            string[] values = new string[4] { NameTextBox.Text, PriceTextBox.Text, FDateTextBox.Text, TDateTextBox.Text };

            if (!Validate(values)) gm.status = 5;
            else gm.status = (id == "-1") ? sql.Add(values, "goods") : sql.Replace(id, values, "goods");

            Close();
        }

        private void RejectButton_Click(object sender, RoutedEventArgs e) { Close(); }

        private bool Validate(string[] values)
        {
            if (values[0].Length > 32) return false;

            Regex reg = new Regex(@"^([\d.]+)$");
            if (!reg.IsMatch(values[1])) return false;

            var formats = new[] { "yyyy-M-d", "yyyy-M-dd", "yyyy-MM-d", "yyyy-MM-dd", "yyyy/M/d", "yyyy/M/dd", "yyyy/MM/d", "yyyy/MM/dd" };
            if (!DateTime.TryParseExact(values[2], formats, null, DateTimeStyles.None, out DateTime dt)) return false;
            if (!DateTime.TryParseExact(values[3], formats, null, DateTimeStyles.None, out dt)) return false;

            return true;
        }

        private void Editor_Closed(object sender, EventArgs e) { gm.Update(); }
    }
}
