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

namespace Contacts
{
    public partial class MainForm : Form
    {
        int screen, screenX, screenY;
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void addNewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNewForm Anf = new AddNewForm();
            Anf.ShowDialog();
        }

        private void addNewToolStripMenuItem_MouseHover(object sender, EventArgs e)
        {
            addNewToolStripMenuItem.ForeColor = Color.Black;
            addNewToolStripMenuItem.BackColor = Color.White;
        }

        private void addNewToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            addNewToolStripMenuItem.ForeColor = Color.White;
            addNewToolStripMenuItem.BackColor = Color.Red;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            ContactsData("");
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (screen == 1)
            {
                SetDesktopLocation(MousePosition.X - screenX, MousePosition.Y - screenY);
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            screen = 0;
        }

        private void ExitBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
           
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
 
                EditForm Efrm = new EditForm();
                Efrm.xds = dataGridView1;
                Efrm.ShowDialog();
            
        }

        private void EDITEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ContactsData("");
        }

        private void EDITEToolStripMenuItem_MouseHover(object sender, EventArgs e)
        {
            eDITEToolStripMenuItem.ForeColor = Color.Black;
            eDITEToolStripMenuItem.BackColor = Color.White;


        }

        private void EDITEToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            eDITEToolStripMenuItem.ForeColor = Color.White;
            eDITEToolStripMenuItem.BackColor = Color.Red;
        }

        private void SEARCHToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            ContactsData(textBox1.Text);
        }

        public void ContactsData(string Searchvalue)
        {
            Connect conn = new Connect();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand();
            String query = "SELECT * From contact Where Concat(NAME,NUMBER,EMAIL) Like '%" + Searchvalue + "%'";
            command.CommandText = query;
            command.Connection = conn.GetConnection();
            adapter.SelectCommand = command;
            adapter.Fill(table);
            dataGridView1.RowTemplate.Height = 60;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.Red;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView1.BackgroundColor = Color.FromArgb(33,33,33);
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(18,18,18);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView1.DataSource = table;
            DataGridViewImageColumn imgcol = new DataGridViewImageColumn();
            imgcol = (DataGridViewImageColumn)dataGridView1.Columns[0];
            dataGridView1.Columns[0].Width = 80;
            dataGridView1.Columns[1].Width = 240;
            dataGridView1.Columns[2].Width = 240;
            dataGridView1.Columns[3].Width = 320;
            imgcol.ImageLayout = DataGridViewImageCellLayout.Stretch;
            //dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            

            
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            screen = 1;
            screenX = e.X;
            screenY = e.Y;
        }
    }
}
