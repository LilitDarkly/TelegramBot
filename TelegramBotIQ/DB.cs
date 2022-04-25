using MySql.Data.MySqlClient;
using MySql.Data.Types;
using System.Data;
using System.Data.SqlClient;

namespace TelegramBotIQ
{
    internal class DB
    {

        private readonly string conStr =        "Server=slava2012z.beget.tech;" +
                                                "Database=slava2012z_ed1;" +
                                                "User Id=slava2012z_ed1;" +
                                                "Password=edgar979%;" +
                                                "Allow User Variables=True;" +
                                                "Convert zero datetime=True";

        public DataTable AutoSQL(UserDB userDB)
        {
            using (var connection = new MySqlConnection(conStr))
            {
                var table = new DataTable();

                connection.Open();

                table = ExecutionCommandsSQL(connection, userDB);

                return table;
            }

        }

        public void RegSQL(UserDB userDB)
        {
            using (var connection = new MySqlConnection(conStr))
            {
                connection.Open();

                MySqlCommand cmd = new MySqlCommand($"INSERT INTO Users (UserName, Password) VALUES " +
                    $"('{userDB.GetUser()}', '{userDB.GetPass()}')", connection);

                int rowAffected = cmd.ExecuteNonQuery();
            }
        }

        private DataTable ExecutionCommandsSQL(MySqlConnection connection, UserDB userDB)
        {
            var table = new DataTable();

            var cmd = new MySqlCommand($"SELECT * FROM Users WHERE UserName = '{userDB.GetUser()}' AND Password = '{userDB.GetPass()}' ", connection);

            var reader = cmd.ExecuteReader();

            table.Load(reader);

            return table;
        }

        public DB() { }
    }
}
