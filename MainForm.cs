71  
                fullscreenButton.Text = ">-<"; 
                this.FormBorderStyle = FormBorderStyle.Sizable; 
                this.WindowState = FormWindowState.Maximized; 
                this.FormBorderStyle = FormBorderStyle.None; 
            } 
            else 
            { 
                fullscreenButton.Text = "<->"; 
                this.FormBorderStyle = FormBorderStyle.Sizable; 
                this.WindowState = FormWindowState.Normal; 
                this.FormBorderStyle = FormBorderStyle.None; 
            } 
        } 
 
        private void fullscreenButton_MouseEnter(object sender, EventArgs e) 
        { 
            fullscreenButton.BackColor = Color.FromArgb(29, 22, 222); 
            fullscreenButton.ForeColor = Color.White; 
        } 
 
        private void fullscreenButton_MouseLeave(object sender, EventArgs e) 
        { 
            fullscreenButton.BackColor = Color.FromArgb(26, 168, 235); 
            fullscreenButton.ForeColor = Color.Black; 
        } 
 
        private void button1_Click(object sender, EventArgs e) 
        { 
            AddForm addForm = new AddForm(); 
            addForm.Show(); 
            addForm.TopMost = true; 
        } 
 
        private void deleteButton_Click(object sender, EventArgs e) 
        { 
            if (!string.IsNullOrEmpty(deleteBox.Text) && 
!string.IsNullOrWhiteSpace(deleteBox.Text)) 
            { 
                    DB db = new DB(); 
                    MySqlCommand command = new MySqlCommand("DELETE FROM `products` WHERE `ID`= 
@id", db.getConnection()); 
 
                    command.Parameters.AddWithValue("@id", deleteBox.Text); 
 
                    db.openConnection(); 
 
                    command.ExecuteNonQuery(); 
72  
 
                    db.closeConnection(); 
            } 
            else 
            { 
                MessageBox.Show("Введите другое ID продукта!"); 
            } 
        } 
 
        private void refreshButton_Click(object sender, EventArgs e) 
        { 
            string connStr = 
"server=localhost;user=admin;password=Gfdkhfrnegjq228;database=warehouse"; 
            BindingSource bindingSource = new BindingSource(); 
            MySqlConnection connection = new MySqlConnection(connStr); 
            MySqlCommand cmd = new MySqlCommand(); 
            cmd.Connection = connection; 
            cmd.CommandText = "SELECT * FROM `products`"; 
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd); 
            DataSet dataset = new DataSet(); 
            adapter.Fill(dataset); 
            bindingSource.DataSource = dataset.Tables[0]; 
            dataGridView1.DataSource = bindingSource; 
            connection.Close(); 
        } 
        private void MainForm_Load(object sender, EventArgs e) 
        { 
        } 
 
        private void buttonUpdate_Click(object sender, EventArgs e) 
        { 
            if (!string.IsNullOrEmpty(textBox1.Text) && 
!string.IsNullOrWhiteSpace(textBox1.Text) && 
                !string.IsNullOrEmpty(textBox2.Text) && 
!string.IsNullOrWhiteSpace(textBox2.Text) && 
                 !string.IsNullOrEmpty(textBox3.Text) && 
!string.IsNullOrWhiteSpace(textBox3.Text) && 
                 !string.IsNullOrEmpty(textBox4.Text) && 
!string.IsNullOrWhiteSpace(textBox4.Text) && 
                 !string.IsNullOrEmpty(textBox5.Text) && 
!string.IsNullOrWhiteSpace(textBox5.Text)) 
            { 
                    DB db = new DB(); 
                    MySqlCommand command = new MySqlCommand("UPDATE `products` SET `Номер`=@num, 
`Название продукта`=@nProd," + 
                        " `Кол-во`=@quan, `Цена`=@price WHERE `ID`=@id", db.getConnection()); 
                    command.Parameters.AddWithValue("@id", textBox1.Text); 
73  
                    command.Parameters.AddWithValue("@num", textBox2.Text); 
                    command.Parameters.AddWithValue("@nProd", textBox3.Text); 
                    command.Parameters.AddWithValue("@quan", textBox4.Text); 
                    command.Parameters.AddWithValue("@price", textBox5.Text); 
 
                    db.openConnection(); 
 
                    command.ExecuteNonQuery(); 
 
                    db.closeConnection(); 
            } 
            else 
            { 
                MessageBox.Show("Все поля должны быть заполены!"); 
            } 
        } 
            private void buttonSave_Click(object sender, EventArgs e) 
        { 
            if (radioExcel.Checked) 
            { 
                Excel.Application exApp = new Excel.Application(); 
 
                exApp.Workbooks.Add(); 
                Excel.Worksheet wsh = (Excel.Worksheet)exApp.ActiveSheet; 
 
                int i, j; 
                for (i = 0; i <= dataGridView1.RowCount - 2; i++) 
                { 
                    for (j = 0; j <= dataGridView1.ColumnCount - 1; j++) 
                    { 
                        wsh.Cells[i + 2, j + 1] = dataGridView1[j, i].Value.ToString(); 
                        wsh.Cells[1, j + 1] = dataGridView1.Columns[j].Name; 
                    } 
                } 
 
                exApp.Visible = true; 
            } 
            if (radioWord.Checked) 
            { 
                SaveFileDialog sfd = new SaveFileDialog(); 
 
                sfd.Filter = "Word Documents (*.docx)|*.docx"; 
 
                sfd.FileName = "Report.docx"; 
 
                if (sfd.ShowDialog() == DialogResult.OK) 
                { 
74  
 
                    ExportDataToWord(dataGridView1, sfd.FileName); 
                } 
            } 
            if (radioTxt.Checked) 
            { 
                System.IO.StreamWriter file = new 
System.IO.StreamWriter(@"C:\Users\Женя\Desktop\sample.txt"); 
                try 
                { 
                    string sLine = ""; 
                    for (int r = 0; r <= dataGridView1.Rows.Count - 1; r++) 
                    { 
                        for (int c = 0; c <= dataGridView1.Columns.Count - 1; c++) 
                        { 
                            sLine += dataGridView1.Columns[c].Name + " "; 
                            sLine = sLine + dataGridView1.Rows[r].Cells[c].Value; 
                            if (c != dataGridView1.Columns.Count - 1) 
                            { 
                                sLine = sLine + "  "; 
                            } 
                        } 
                        file.WriteLine(sLine); 
                        sLine = ""; 
                    } 
 
                    file.Close(); 
                    System.Windows.Forms.MessageBox.Show("Данные  занесены  в  файл.",  "Program 
Info", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information); 
                } 
                catch (System.Exception err) 
                { 
                    System.Windows.Forms.MessageBox.Show(err.Message, "Error",  
                        MessageBoxButtons.OK, MessageBoxIcon.Error); 
                    file.Close(); 
                } 
            } 
        } 
 
        public void ExportDataToWord(DataGridView DGV, string filename) 
        { 
            if (DGV.Rows.Count != 0) 
            { 
                int RowCount = DGV.Rows.Count; 
                int ColumnCount = DGV.Columns.Count; 
                Object[,] DataArray = new object[RowCount + 1, ColumnCount + 1]; 
75  
 
                int r = 0; 
                for (int c = 0; c <= ColumnCount - 1; c++) 
                { 
                    for (r = 0; r <= RowCount - 1; r++) 
                    { 
                        DataArray[r, c] = DGV.Rows[r].Cells[c].Value; 
                    } 
                } 
 
                Word.Document oDoc = new Word.Document(); 
                oDoc.Application.Visible = true; 
 
                oDoc.PageSetup.Orientation = Word.WdOrientation.wdOrientLandscape; 
 
 
                dynamic oRange = oDoc.Content.Application.Selection.Range; 
                string oTemp = ""; 
                for (r = 0; r <= RowCount - 1; r++) 
                { 
                    for (int c = 0; c <= ColumnCount - 1; c++) 
                    { 
                        oTemp = oTemp + DataArray[r, c] + "\t"; 
 
                    } 
                } 
 
                oRange.Text = oTemp; 
 
                object Separator = Word.WdTableFieldSeparator.wdSeparateByTabs; 
                object ApplyBorders = true; 
                object AutoFit = true; 
                object AutoFitBehavior = Word.WdAutoFitBehavior.wdAutoFitContent; 
 
                oRange.ConvertToTable(ref Separator, ref RowCount, ref ColumnCount, 
                                      Type.Missing, Type.Missing, ref ApplyBorders, 
                                      Type.Missing, Type.Missing, Type.Missing, 
                                      Type.Missing, Type.Missing, Type.Missing, 
                                      Type.Missing, ref AutoFit, ref AutoFitBehavior, 
Type.Missing); 
 
                oRange.Select(); 
 
                oDoc.Application.Selection.Tables[1].Select(); 
                oDoc.Application.Selection.Tables[1].Rows.AllowBreakAcrossPages = 0; 
                oDoc.Application.Selection.Tables[1].Rows.Alignment = 0; 
                oDoc.Application.Selection.Tables[1].Rows[1].Select(); 
76  
                oDoc.Application.Selection.InsertRowsAbove(1); 
                oDoc.Application.Selection.Tables[1].Rows[1].Select(); 
 
                oDoc.Application.Selection.Tables[1].Rows[1].Range.Bold = 1; 
                oDoc.Application.Selection.Tables[1].Rows[1].Range.Font.Name = "Times New 
Roman"; 
                oDoc.Application.Selection.Tables[1].Rows[1].Range.Font.Size = 14; 
 
                for (int c = 0; c <= ColumnCount - 1; c++) 
                { 
                    oDoc.Application.Selection.Tables[1].Cell(1, c + 1).Range.Text = 
DGV.Columns[c].HeaderText; 
                } 
 
                oDoc.Application.Selection.Tables[1].set_Style("Grid Table 4 - Accent 5"); 
                oDoc.Application.Selection.Tables[1].Rows[1].Select(); 
                oDoc.Application.Selection.Cells.VerticalAlignment =  
                    Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter; 
 
                foreach (Word.Section section in oDoc.Application.ActiveDocument.Sections) 
                { 
                    Word.Range headerRange = 
section.Headers[Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].Range; 
                    headerRange.Fields.Add(headerRange, Word.WdFieldType.wdFieldPage); 
                    headerRange.Text = "your header text"; 
                    headerRange.Font.Size = 14; 
                    headerRange.ParagraphFormat.Alignment = 
Word.WdParagraphAlignment.wdAlignParagraphCenter; 
                } 
 
                oDoc.SaveAs2(filename); 
            } 
        } 
 
        private void printDocument1_PrintPage(object sender, 
System.Drawing.Printing.PrintPageEventArgs e) 
        { 
            Graphics g = e.Graphics; 
            int x = 0; 
            int y = 20; 
            int cell_height = 0; 
 
            int colCount = dataGridView1.ColumnCount; 
            int rowCount = dataGridView1.RowCount - 1; 
 
            Font font = new Font("Times New Roman", 14, FontStyle.Bold, GraphicsUnit.Point); 
 
77  
            int[] widthC = new int[colCount]; 
 
            int current_col = 0; 
            int current_row = 0; 
 
            while (current_col < colCount) 
            { 
                if (g.MeasureString(dataGridView1.Columns[current_col].HeaderText.ToString(), 
font).Width > widthC[current_col]) 
                { 
                    widthC[current_col] = 
(int)g.MeasureString(dataGridView1.Columns[current_col].HeaderText.ToString(),  
                        font).Width; 
                } 
                current_col++; 
            } 
 
            while (current_row < rowCount) 
            { 
                while (current_col < colCount) 
                { 
                    if (g.MeasureString(dataGridView1[current_col, 
current_row].Value.ToString(), 
                        font).Width > widthC[current_col]) 
                    { 
                        widthC[current_col] = (int)g.MeasureString(dataGridView1[current_col, 
current_row].Value.ToString(),  
                            font).Width; 
                    } 
                    current_col++; 
                } 
                current_col = 0; 
                current_row++; 
            } 
 
            current_col = 0; 
            current_row = 0; 
 
            string value = ""; 
 
            int width = widthC[current_col]; 
            int height = dataGridView1[current_col, current_row].Size.Height; 
 
            Rectangle cell_border; 
            SolidBrush brush = new SolidBrush(Color.Black); 
 
 
78  
            while (current_col < colCount) 
            { 
                width = widthC[current_col]; 
                cell_height = dataGridView1[current_col, current_row].Size.Height; 
                cell_border = new Rectangle(x, y, width, height); 
                value = dataGridView1.Columns[current_col].HeaderText.ToString(); 
                g.DrawRectangle(new Pen(Color.Black), cell_border); 
                g.DrawString(value, font, brush, x, y); 
                x += widthC[current_col]; 
                current_col++; 
            } 
            while (current_row < rowCount) 
            { 
                while (current_col < colCount) 
                { 
                    width = widthC[current_col]; 
                    cell_height = dataGridView1[current_col, current_row].Size.Height; 
                    cell_border = new Rectangle(x, y, width, height); 
                    value = dataGridView1[current_col, current_row].Value.ToString(); 
                    g.DrawRectangle(new Pen(Color.Black), cell_border); 
                    g.DrawString(value, font, brush, x, y); 
                    x += widthC[current_col]; 
                    current_col++; 
                } 
                current_col = 0; 
                current_row++; 
                x = 0; 
                y += cell_height; 
            } 
        } 
 
        private void buttonPrint_Click(object sender, EventArgs e) 
        { 
            PrintDocument Document = new PrintDocument(); 
            Document.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage); 
            PrintPreviewDialog dlg = new PrintPreviewDialog(); 
            dlg.Document = Document; 
            dlg.ShowDialog(); 
            Document.Print(); 
        } 
    } 
}
