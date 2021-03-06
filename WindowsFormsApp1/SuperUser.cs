using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace WindowsFormsApp1
{
    public class SuperUser : User
    {
        /// <summary>
        /// \brief Конструктор класса SuperUser, явно вызывающий конструктор базового класса User
        /// </summary>
        public SuperUser() : base()
        {
        }

        /// <summary>
        /// \brief Метод загружает в базу данных информацию, которую ввел пользоваетль в первой регистрационной форме
        /// </summary>
        /// <param name="NewUser"></param>
        /// <param name="DB"></param>
        /// <returns></returns>
        public bool RegistrationForm1(User NewUser, DataBase DB)
        {
            string auth_script = "INSERT INTO Authorization (login, password) VALUES (@log, @pass);";
            DB.connection.Open();

            DB.command = new MySqlCommand(auth_script, DB.connection);
            DB.command.Parameters.Add("@log", MySqlDbType.VarChar).Value = NewUser.GetLoginAcc();
            DB.command.Parameters.Add("@pass", MySqlDbType.VarChar).Value = NewUser.GetPassword();

            if (DB.command.ExecuteNonQuery() == 1) 
            {
                DB.connection.Close();
                return true;
            }
            else 
            {
                DB.connection.Close();
                return false;
            }
        }

        /// <summary>
        /// \brief Метод загружает в базу данных информацию, которую ввел пользоваетль во второй регистрационной форме
        /// </summary>
        /// <param name="NewUser"></param>
        /// <returns></returns>
        public bool RegistrationForm2(User NewUser) 
        {
            DataBase DB = new DataBase();

            DB.connection.Open();

            string set_ID_script = "SELECT account_id FROM Authorization WHERE login = @log;";

            DB.command = new MySqlCommand(set_ID_script, DB.connection);
            DB.command.Parameters.Add("@log", MySqlDbType.VarChar).Value = NewUser.GetLoginAcc();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter(set_ID_script, DB.connection);
            adapter.SelectCommand = DB.command;
            adapter.Fill(table);
            NewUser.setID(Convert.ToInt32(table.Rows[0][0]));

            string user_script = "INSERT INTO Users (user_id, account, familiya, imya, otchestvo, role)" +
                " VALUES (@U, @A, @F, @I, @O, @R);";

            DB.command = new MySqlCommand(user_script, DB.connection);

            DB.command.Parameters.Add("@U", MySqlDbType.Int32).Value = NewUser.GetID();
            DB.command.Parameters.Add("@A", MySqlDbType.Int32).Value = NewUser.GetID();
            DB.command.Parameters.Add("@F", MySqlDbType.VarChar).Value = NewUser.GetSurname();
            DB.command.Parameters.Add("@I", MySqlDbType.VarChar).Value = NewUser.GetName();
            DB.command.Parameters.Add("@O", MySqlDbType.VarChar).Value = NewUser.GetPatronymic();
            DB.command.Parameters.Add("@R", MySqlDbType.VarChar).Value = NewUser.GetRole();

            if (DB.command.ExecuteNonQuery() == 1)
            {
                DB.connection.Close();
                return true;
            }
            else
            {
                DB.connection.Close();
                return false;
            }
        }

        /// <summary>
        /// \brief Метод загружает в базу данных информацию, которую ввел пользоваетль при добавлении нового устройства
        /// </summary>
        /// <param name="newDevice"></param>
        /// <param name="DB"></param>
        /// <returns></returns>
        public bool AddNewDevice(Devices newDevice, DataBase DB) 
        {
            string script = "INSERT INTO Device (name, ON_OFF) VALUES (@n, @on_off);";

            DB.connection.Open();

            DB.command = new MySqlCommand(script, DB.connection);

            DB.command.Parameters.Add("@n", MySqlDbType.VarChar).Value = newDevice.getName();
            DB.command.Parameters.Add("@on_off", MySqlDbType.Int16).Value = newDevice.getON_OFF();


            if (DB.command.ExecuteNonQuery() == 1)
            {
                DB.connection.Close();
                return true;
            }
            else
            {
                DB.connection.Close();
                return false;
            }
        }
    }
}
