using MySql.Data.MySqlClient; 
using System; 
using System.Collections.Generic; 
using System.ComponentModel; 
using System.Data; 
using System.Drawing; 
using System.Linq; 
using System.Text; 
using System.Threading.Tasks; 
using System.Windows.Forms; 
 
namespace Diplom1 
{ 
    public partial class RegisterForm : Form 
    { 
        public RegisterForm() 
        { 
            InitializeComponent(); 
            this.passField.AutoSize = false; 
            this.passField.Size = new Size(this.passField.Size.Width, 32); 
 
            userNameField.Text = "Enter name.(Введите имя.)"; 
            userNameField.ForeColor = Color.Gray; 
            userSurnameField.Text = "Enter surname.(Введите фамилию.)"; 
            userSurnameField.ForeColor = Color.Gray; 
            passField.Text = "Enter password.(Введите пароль)"; 
            passField.ForeColor = Color.Gray; 
            loginField1.Text = "Enter login.(Введите логин)"; 
            loginField1.ForeColor = Color.Gray; 
            passField.UseSystemPasswordChar = false; 
        } 
 
        private void label1_Click(object sender, EventArgs e) 
        { 
 
        } 
 
        private void closeButton_Click(object sender, EventArgs e) 
        { 
            Application.Exit(); 
        } 
 
        private void rollUpButton_Click(object sender, EventArgs e) 
        { 
            this.WindowState = FormWindowState.Minimized; 
        } 
 
        private void closeButton_MouseEnter(object sender, EventArgs e) 
        { 
            closeButton.BackColor = Color.FromArgb(229, 57, 20); 
        } 
 
        private void closeButton_MouseLeave(object sender, EventArgs e) 
        { 
            closeButton.BackColor = Color.FromArgb(245, 91, 91); 
        } 
 
        private void rollUpButton_MouseEnter(object sender, EventArgs e) 
        { 
            rollUpButton.BackColor = Color.FromArgb(29, 22, 222); 
            rollUpButton.ForeColor = Color.White; 
        } 
65  
 
        private void rollUpButton_MouseLeave(object sender, EventArgs e) 
        { 
            rollUpButton.BackColor = Color.FromArgb(26, 168, 235); 
            rollUpButton.ForeColor = Color.Black; 
        } 
 
        Point lastPoint; 
        private void panel2_MouseMove(object sender, MouseEventArgs e) 
        { 
            if (e.Button == MouseButtons.Left) 
            { 
                this.Left += e.X - lastPoint.X; 
                this.Top += e.Y - lastPoint.Y; 
            } 
        } 
 
        private void panel2_MouseDown(object sender, MouseEventArgs e) 
        { 
            lastPoint = new Point(e.X, e.Y); 
        } 
 
        private void userNameField_Enter(object sender, EventArgs e) 
        { 
            if (userNameField.Text == "Enter name.(Введите имя.)") 
            { 
                userNameField.Text = ""; 
                userNameField.ForeColor = Color.Black; 
            } 
        } 
 
        private void userNameField_Leave(object sender, EventArgs e) 
        { 
            if(userNameField.Text == "") 
            { 
                userNameField.Text = "Enter name.(Введите имя.)"; 
                userNameField.ForeColor = Color.Gray; 
            } 
        } 
 
        private void userSurnameField_Enter(object sender, EventArgs e) 
        { 
            if (userSurnameField.Text == "Enter surname.(Введите фамилию.)") 
            { 
                userSurnameField.Text = ""; 
                userSurnameField.ForeColor = Color.Black; 
            } 
        } 
 
        private void userSurnameField_Leave(object sender, EventArgs e) 
        { 
            if (userSurnameField.Text == "") 
            { 
                userSurnameField.Text = "Enter surname.(Введите фамилию.)"; 
                userSurnameField.ForeColor = Color.Gray; 
            } 
        } 
 
        private void passField_Enter(object sender, EventArgs e) 
        { 
            if (passField.Text == "Enter password.(Введите пароль)") 
            { 
                passField.Text = ""; 
                passField.ForeColor = Color.Black; 
                passField.UseSystemPasswordChar = true; 
            } 
66  
        } 
 
        private void passField_Leave(object sender, EventArgs e) 
        { 
            if (passField.Text == "") 
            { 
                passField.Text = "Enter password.(Введите пароль)"; 
                passField.ForeColor = Color.Gray; 
                passField.UseSystemPasswordChar = false; 
            } 
        } 
 
        private void loginField_Enter(object sender, EventArgs e) 
        { 
            if (loginField1.Text == "Enter login.(Введите логин)") 
            { 
                loginField1.Text = ""; 
                loginField1.ForeColor = Color.Black; 
            } 
        } 
 
        private void loginField_Leave(object sender, EventArgs e) 
        { 
            if (loginField1.Text == "") 
            { 
                loginField1.Text = "Enter login.(Введите логин)"; 
                loginField1.ForeColor = Color.Gray; 
            } 
        } 
 
        private void buttonRegister_Click(object sender, EventArgs e) 
        { 
            if(userNameField.Text == "Enter name.(Введите имя.)") 
            { 
                MessageBox.Show("Введите имя"); 
                return; 
            } 
 
            if(userSurnameField.Text == "Enter surname.(Введите фамилию.)") 
            { 
                MessageBox.Show("Введите фамилию"); 
                return; 
            } 
 
            if(loginField1.Text == "Enter login.(Введите логин)") 
            { 
                MessageBox.Show("Введите логин"); 
                return; 
            } 
 
            if(passField.Text == "Enter password.(Введите пароль)") 
            { 
                MessageBox.Show("Введите пароль"); 
                return; 
            } 
 
            if (isUserExists()) 
                return; 
 
            DB db = new DB(); 
            MySqlCommand command = new MySqlCommand("INSERT INTO `users` (`login`, `pass`, 
`Name`, `Surname`) " + 
                "VALUES (@login, @pass, @name, @surname)", db.getConnection()); 
 
            command.Parameters.Add("@login", MySqlDbType.Text).Value = loginField1.Text; 
            command.Parameters.Add("@pass", MySqlDbType.Text).Value = passField.Text; 
67  
            command.Parameters.Add("@name", MySqlDbType.Text).Value = userNameField.Text; 
            command.Parameters.Add("@surname", MySqlDbType.Text).Value = 
userSurnameField.Text; 
 
            db.openConnection(); 
 
            if(command.ExecuteNonQuery() == 1) 
            { 
                MessageBox.Show("Аккаунт был создан"); 
            } 
            else 
            { 
                MessageBox.Show("Аккаунт не был создан"); 
            } 
 
            db.closeConnection(); 
        } 
 
        public Boolean isUserExists() 
        { 
            DB db = new DB(); 
 
            DataTable table = new DataTable(); 
 
            MySqlDataAdapter adapter = new MySqlDataAdapter(); 
 
            MySqlCommand command = new MySqlCommand("SELECT * FROM `users` WHERE `login` = 
@uL", db.getConnection()); 
            command.Parameters.Add("@uL", MySqlDbType.Text).Value = loginField1.Text; 
 
 
            adapter.SelectCommand = command; 
            adapter.Fill(table); 
 
            if (table.Rows.Count > 0) 
            { 
                MessageBox.Show("Такой логин уже есть, введите другой"); 
                return true; 
            } 
            else 
                return false; 
        } 
 
        private void loginLable_Click(object sender, EventArgs e) 
        { 
            this.Hide(); 
            LoginForm loginForm = new LoginForm(); 
            loginForm.Show(); 
        } 
 
        private void loginField1_KeyPress(object sender, KeyPressEventArgs e) 
        { 
            if (e.KeyChar >= 'A' && e.KeyChar <= 'Z' || e.KeyChar >= 'a' && e.KeyChar <= 
'z' || (Keys)e.KeyChar == Keys.Back || e.KeyChar >= '0' && e.KeyChar <= '9' || 
(Keys)e.KeyChar == Keys.Enter) 
            { 
                 
            } 
            else 
            { 
                e.Handled = true; 
                MessageBox.Show("Допустимы только латинские буквы и цыфры!"); 
            } 
        } 
 
        private void passField_KeyPress(object sender, KeyPressEventArgs e) 
68  
        { 
            if (e.KeyChar >= 'A' && e.KeyChar <= 'Z' || e.KeyChar >= 'a' && e.KeyChar <= 
'z' || (Keys)e.KeyChar == Keys.Back || e.KeyChar >= '0' && e.KeyChar <= '9' || 
(Keys)e.KeyChar == Keys.Enter) 
            { 
            } 
            else 
            { 
                e.Handled = true; 
                MessageBox.Show("Допустимы только латинские буквы и цыфры!"); 
            } 
        } 
    } 
} 
