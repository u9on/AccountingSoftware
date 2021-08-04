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
    public partial class LoginForm : Form 
    { 
        public LoginForm() 
        { 
            InitializeComponent(); 
 
            this.passField.AutoSize = false; 
            this.passField.Size = new Size(this.passField.Size.Width, 32); 
        } 
 
        private void closeButton_Click(object sender, EventArgs e) 
        { 
            Application.Exit(); 
        } 
 
        private void closeButton_MouseEnter(object sender, EventArgs e) 
        { 
            closeButton.BackColor = Color.FromArgb(229, 57, 20); 
        } 
 
        private void closeButton_MouseLeave(object sender, EventArgs e) 
        { 
            closeButton.BackColor = Color.FromArgb(245, 91, 91); 
        } 
 
        Point lastPoint; 
        private void panel2_MouseMove(object sender, MouseEventArgs e) 
        { 
            if(e.Button == MouseButtons.Left) 
            { 
                this.Left += e.X - lastPoint.X; 
                this.Top += e.Y - lastPoint.Y; 
            } 
        } 
 
        private void panel2_MouseDown(object sender, MouseEventArgs e) 
        { 
            lastPoint = new Point(e.X, e.Y); 
        } 
 
        private void rollUpButton_Click(object sender, EventArgs e) 
        { 
            
            this.WindowState = FormWindowState.Minimized; 
        } 
 
        private void rollUpButton_MouseEnter(object sender, EventArgs e) 
62  
        { 
            rollUpButton.BackColor = Color.FromArgb(29, 22, 222); 
            rollUpButton.ForeColor = Color.White; 
        } 
 
        private void rollUpButton_MouseLeave(object sender, EventArgs e) 
        { 
            rollUpButton.BackColor = Color.FromArgb(26, 168, 235); 
            rollUpButton.ForeColor = Color.Black; 
        } 
 
 
        private void buttonLogin_Click(object sender, EventArgs e) 
        { 
            String loginUser = loginField.Text; 
            String passUser = passField.Text; 
 
            DB db = new DB(); 
 
            DataTable table = new DataTable(); 
 
            MySqlDataAdapter adapter = new MySqlDataAdapter(); 
 
            MySqlCommand command = new MySqlCommand("SELECT * FROM `users` WHERE `login` = 
@uL AND `pass` = @uP", db.getConnection()); 
            command.Parameters.Add("@uL", MySqlDbType.Text).Value = loginUser; 
            command.Parameters.Add("@uP", MySqlDbType.Text).Value = passUser; 
 
            adapter.SelectCommand = command; 
            adapter.Fill(table); 
 
            if(table.Rows.Count > 0) 
            { 
                this.Hide(); 
                MainForm mainForm = new MainForm(); 
                mainForm.Show(); 
            } 
            else 
            { 
                MessageBox.Show("Такого пользователя нет или пароль неверный."); 
            } 
        } 
 
        private void registerLable_Click(object sender, EventArgs e) 
        { 
            this.Hide(); 
            RegisterForm registerForm = new RegisterForm(); 
            registerForm.Show(); 
        } 
 
        private void loginField_KeyPress(object sender, KeyPressEventArgs e) 
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
        { 
63  
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
