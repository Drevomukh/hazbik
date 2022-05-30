using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace WindowsFormsApp1
{
    public partial class RegForm1 : Form
    {
        SuperUser admin = new SuperUser();

        /// <summary>
        /// \brief Конструктор для запуска первой экранной формы регистрации 
        /// </summary>
        public RegForm1()
        {
            InitializeComponent();
            loginfield.Text = "Введите логин";
            loginfield.ForeColor = Color.White;
            passfield.Text = "Введите пароль";
            passfield.ForeColor = Color.White;
            passfield.UseSystemPasswordChar = false;
        }

        /// <summary>
        /// \brief Переопределенный конструктор, принимающий класс SuperUser
        /// </summary>
        /// <param name="admin"></param>
        public RegForm1(SuperUser admin)
        {
            InitializeComponent();
            loginfield.Text = "Введите логин";
            loginfield.ForeColor = Color.White;
            passfield.Text = "Введите пароль";
            passfield.ForeColor = Color.White;
            passfield.UseSystemPasswordChar = false;
            this.admin = admin;
        }


        private void loginfield_Enter(object sender, EventArgs e)
        {
            if (loginfield.Text == "Введите логин")
            {
                loginfield.Text = "";
                loginfield.ForeColor = Color.White;
            }
        }

        private void loginfield_Leave(object sender, EventArgs e)
        {
            if (loginfield.Text == "")
            {
                loginfield.Text = "Введите логин";
                loginfield.ForeColor = Color.White;
            }
        }

        private void passfield_Enter(object sender, EventArgs e)
        {
            if (passfield.Text == "Введите пароль")
            {
                passfield.Text = "";
                passfield.ForeColor = Color.White;
                passfield.UseSystemPasswordChar = true;
            }
        }

        private void passfield_Leave(object sender, EventArgs e)
        {
            if (passfield.Text == "")
            {
                passfield.Text = "Введите пароль";
                passfield.ForeColor = Color.White;
                passfield.UseSystemPasswordChar = false;
            }
        }
       

        private void login_button_Click(object sender, EventArgs e)
        {

            if (loginfield.Text == "Введите логин")
            {
                MessageBox.Show("Логин не может быть пустым"); 
                return;
            }

            if (passfield.Text == "Введите пароль")
            {
                MessageBox.Show("Пароль не может быть пустым"); 
                return;
            }

            User newUser = new User();
            newUser.setRegFormInfo1(loginfield.Text, passfield.Text);
            DataBase DB = new DataBase();

            if (DB.isUserExist(newUser))
                return;

            if (admin.RegistrationForm1(newUser, DB))
            {
                this.Hide();
                RegForm2 regform = new RegForm2(newUser, admin);
                regform.Show();
            }
            else
                MessageBox.Show("Аккаунт не был создан");
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void passfield_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void loginfield_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
