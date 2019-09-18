using System.Data;
using System.Windows.Media;

namespace Simple_Manager
{
    internal class LoginManager
    {
        private string cnnStr = "Data Source=(local)\\SQLEXPRESS;Initial Catalog = SimpleManagerDB; Integrated Security = True";
        private int status = -1;
        private MainWindow window;
        SqlManager sql = new SqlManager();

        internal LoginManager(MainWindow loginWindow)
        {
            window = loginWindow;
            status = sql.Connect(cnnStr);
            Update();
        }

        ~LoginManager()
        {
            sql = null;
            cnnStr = null;
            status = -1;
        }

        internal void Proceed()
        {
            status = sql.Proceed(window.LoginComboBox.Text, window.PasswordTextBox.Text);

            if (status == 0)
            {
                GoodsManagerWindow newWindow = new GoodsManagerWindow();
                newWindow.Show();
                window.Close();
                return;
            }

            Update();
        }

        internal void Update()
        {
            DataTable dt = sql.GetRow("users", "login");

            if (dt != null)
            {
                window.LoginComboBox.Items.Clear();
                window.PasswordTextBox.Text = "";
                for (int i = 0; i < dt.Rows.Count; i++) window.LoginComboBox.Items.Add(dt.Rows[i][0]);
            }
            else status = 1;

            UpdateStatus();
        }

        private void UpdateStatus()
        {
            if (status != 0)
            {
                switch (status)
                {
                    case 1:
                        {
                            window.StatusLabel.Foreground = Brushes.Red;
                            window.StatusLabel.Content = "An error has occurred! \nTrouble with DB connect!";
                            break;
                        }
                    case 2:
                        {
                            window.StatusLabel.Foreground = Brushes.Red;
                            window.StatusLabel.Content = "An error has occurred! \nTrouble with DB disconnect!";
                            break;
                        }
                    case 3:
                        {
                            window.StatusLabel.Foreground = Brushes.Red;
                            window.StatusLabel.Content = "An error has occurred! \nTrouble with DB connection!";
                            break;
                        }
                    case 4:
                        {
                            window.StatusLabel.Foreground = Brushes.Red;
                            window.StatusLabel.Content = "An error has occurred! \nInvalid login or/and password!";
                            break;
                        }
                    case 5:
                        {
                            window.StatusLabel.Foreground = Brushes.Red;
                            window.StatusLabel.Content = "An error has occurred! \nWrong login/password";
                            break;
                        }
                    default:
                        {
                            window.StatusLabel.Foreground = Brushes.Green;
                            window.StatusLabel.Content = "DB connected!";
                            break;
                        }
                }
            }
        }
    }
}
