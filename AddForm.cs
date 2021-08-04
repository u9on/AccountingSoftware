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
    public partial class AddForm : Form 
    { 
        public AddForm() 
        { 
            InitializeComponent(); 
        } 
 
 
        private void closeButton_Click(object sender, EventArgs e) 
        { 
            this.Hide(); 
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
 
80  
        private void rollUpButton_MouseLeave(object sender, EventArgs e) 
        { 
            rollUpButton.BackColor = Color.FromArgb(26, 168, 235); 
            rollUpButton.ForeColor = Color.Black; 
        } 
 
        private void addProdButton_Click(object sender, EventArgs e) 
        { 
            if (numProdField.Text == "") 
            { 
                MessageBox.Show("Введите номер продукта."); 
                return; 
            } 
 
            if (nameProdField.Text == "") 
            { 
                MessageBox.Show("Введите название продукта."); 
                return; 
            } 
 
            if (amountField.Text == "") 
            { 
                MessageBox.Show("Введите количество продукта."); 
                return; 
            } 
 
            if (priceField.Text == "") 
            { 
                MessageBox.Show("Введите цену продукта."); 
                return; 
            } 
 
            if (isProdExists()) 
                return; 
 
            DB db = new DB(); 
 
            MySqlCommand command = new MySqlCommand("INSERT INTO `products` (`Номер`, 
`Название продукта`," + 
                " `Кол-во`, `Цена`) VALUE (@number, @prodName, @amount, @price)", 
db.getConnection()); 
 
            command.Parameters.Add("@number", MySqlDbType.VarChar).Value = 
numProdField.Text; 
            command.Parameters.Add("@prodName", MySqlDbType.VarChar).Value = 
nameProdField.Text; 
            command.Parameters.Add("@amount", MySqlDbType.VarChar).Value = 
amountField.Text; 
            command.Parameters.Add("@price", MySqlDbType.VarChar).Value = priceField.Text; 
 
            db.openConnection(); 
 
            if (command.ExecuteNonQuery() == 1) 
            { 
                MessageBox.Show("Данные были добавлены."); 
            } 
            else 
            { 
                MessageBox.Show("Данные не были добавлены."); 
            } 
        } 
 
        public Boolean isProdExists() 
        { 
            DB db = new DB(); 
 
81  
            DataTable table = new DataTable(); 
 
            MySqlDataAdapter adapter = new MySqlDataAdapter(); 
 
            MySqlCommand command = new MySqlCommand("SELECT * FROM `products` WHERE `Номер` 
= @num " + 
                "OR `Название продукта` = @nProd", db.getConnection()); 
            command.Parameters.Add("@nProd", MySqlDbType.VarChar).Value = 
nameProdField.Text; 
            command.Parameters.Add("@num", MySqlDbType.VarChar).Value = numProdField.Text; 
 
 
            adapter.SelectCommand = command; 
            adapter.Fill(table); 
 
            if (table.Rows.Count > 0) 
            { 
                MessageBox.Show("Такой продукт, уже записан на складе."); 
                return true; 
            } 
            else 
                return false; 
        } 
         
    } 
} 
