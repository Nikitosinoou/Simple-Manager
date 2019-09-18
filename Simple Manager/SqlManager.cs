using System.Data;
using System.Data.SqlClient;

namespace Simple_Manager
{
    internal class SqlManager
    {
        private SqlConnection con;
        private SqlDataAdapter sda;

        public int Connect(string cnnStr)
        {
            try
            {
                con = new SqlConnection(cnnStr);
                con.Open();
                return 0;
            }
            catch (SqlException)
            {
                con = null;
                return 1;
            }
        }

        public int Disconnect()
        {
            if (con != null)
            {
                sda = null;
                con.Close();
                con = null;
                return 0;
            }
            else return 2;

        }

        public int Add(string[] values, string table)
        {
            try
            {
                if (values[0] != null && values[1] != null && values[2] != null && values[3] != null)
                {
                    sda.InsertCommand = con.CreateCommand();
                    sda.InsertCommand.CommandText = "INSERT INTO " + table + " (Name, Price, FromDate, ToDate) VALUES ('" + values[0] + "', " +
                        values[1] + ", '" +
                        values[2] + "', '" +
                        values[3] + "')";
                    sda.InsertCommand.ExecuteNonQuery();
                    return -4;
                }
                else return 4;
            }
            catch (SqlException)
            {
                return 1;
            }
        }

        public int Replace(string id, string[] values, string table)
        {
            try
            {
                if (id != "-1")
                {
                    sda.UpdateCommand = con.CreateCommand();
                    sda.UpdateCommand.CommandText = "UPDATE " + table + " SET Name = '" + values[0] + "' WHERE ID = " + id;
                    sda.UpdateCommand.ExecuteNonQuery();
                    sda.UpdateCommand.CommandText = "UPDATE " + table + " SET Price = " + values[1] + " WHERE ID = " + id;
                    sda.UpdateCommand.ExecuteNonQuery();
                    sda.UpdateCommand.CommandText = "UPDATE " + table + " SET FromDate = '" + values[2] + "' WHERE ID = " + id;
                    sda.UpdateCommand.ExecuteNonQuery();
                    sda.UpdateCommand.CommandText = "UPDATE " + table + " SET ToDate = '" + values[3] + "' WHERE ID = " + id;
                    sda.UpdateCommand.ExecuteNonQuery();
                    return -3;
                }
                else return 4;
            }
            catch (SqlException)
            {
                return 1;
            }
        }

        public int Remove(string value, string table)
        {
            try
            {
                if (value != "-1")
                {
                    sda.DeleteCommand = con.CreateCommand();
                    sda.DeleteCommand.CommandText = "DELETE FROM " + table + " WHERE Id = " + value;
                    sda.DeleteCommand.ExecuteNonQuery();
                    return -1;
                }
                else return 4;
            }
            catch (SqlException) { return 1; }
        }

        public DataTable GetRow(string name, string row)
        {
            try
            {
                sda = new SqlDataAdapter("SELECT " + row + " FROM " + name, con);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                return dt;
            }
            catch (SqlException) { return null; }
        }

        public DataTable GetTable(string name)
        {
            try
            {
                sda = new SqlDataAdapter("SELECT * FROM " + name, con);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                return dt;
            }
            catch (SqlException) { return null; }
        }

        public int Proceed(string login, string password)
        {
            try
            {
                if (login != null && password != null && login != "" && password != "")
                {
                    return (Authenticate(login, password)) ? 0 : 5;
                }
                else
                {
                    return 4;
                }
            }
            catch (SqlException) { return 1; }
        }

        private bool Authenticate (string login, string password)
        {
            sda = new SqlDataAdapter("SELECT COUNT(*) FROM users WHERE login='" + login + "' AND password='" + password + "'", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            return dt.Rows[0][0].ToString() == "1" ? true : false;
        }
    }
}
