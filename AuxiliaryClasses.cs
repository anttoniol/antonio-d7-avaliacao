using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace AuxiliaryClasses
{
    internal class Conexao
    {
        private string connStr;

        internal Conexao()
        {
            connStr = "server=localhost;user id=root;password=root;database=antonio-d7-avaliacao";
        }

        internal bool Select(string email, string password)
        {
            try
            {
                string query = "SELECT id, nome, senha FROM login WHERE email = @email;";

                MySqlConnection connection = new MySqlConnection(connStr);
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@email", email);
                command.Parameters.AddWithValue("@password", password);
                connection.Open();

                MySqlDataReader dataReader;
                dataReader = command.ExecuteReader();

                DataTable dataTable = new DataTable();
                dataTable.Load(dataReader);

                connection.Close();

                if (dataTable.Rows.Count == 0)
                    return false;

                return BCrypt.Net.BCrypt.Verify(password, dataTable.Rows[0]["senha"].ToString());
            }
            catch
            {
                Console.WriteLine("\nHouve um erro na conexão com o banco de dados\n");
                return false;
            }
        }
    }

    public class AccessControl
    {
        private string[] malicious =
        {
            "drop", "delete", "select", ";", "--", "insert", "delete", "xp_", "'", "update", "/*", "*/"
        };

        private string validateData(string data)
        {
            string secureData = data;
            foreach (string bad in malicious)
                secureData = secureData.Replace(bad, "", true, null);
            return secureData;
        }

        public bool checkData(string email, string password)
        {
            email = validateData(email);
            password = validateData(password);

            return new Conexao().Select(email, password);
        }
    }
}

