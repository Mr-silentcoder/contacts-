using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using MySql.Data.MySqlClient;

namespace Contacts
{
    public partial class EditForm : Form
    {
        int screen, screenX, screenY;
        public DataGridView xds;
        public EditForm()
        {
            InitializeComponent();
        }

        private void EditForm_Load(object sender, EventArgs e)
        {
            Byte[] img = (Byte[])xds.CurrentRow.Cells[0].Value;
            MemoryStream ms = new MemoryStream(img);
            pictureBox1.Image = Image.FromStream(ms);
            Username.Text = xds.CurrentRow.Cells[1].Value.ToString();
            textBox2.Text = xds.CurrentRow.Cells[3].Value.ToString();
            textBox3.Text = xds.CurrentRow.Cells[2].Value.ToString();
        }

        private void ExitBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            screen = 1;
            screenX = e.X;
            screenY = e.Y;
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

        private void UploadBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog OFD = new OpenFileDialog();
            OFD.Filter = "Choose Image(*.JPG;*.PNG;*.GIF)|*.jpg;*.png;*.gif";

            if (OFD.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(OFD.FileName);
                panel2.BackColor = Color.Red;
                UploadBtn.BackColor = Color.Red;
                SBbutton.BackColor = Color.Red;
            }
        }

        private void SBbutton_Click(object sender, EventArgs e)
        {

            MemoryStream ms = new MemoryStream();
            pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
            byte[] img = ms.ToArray();

            Connect conn = new Connect();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand();
            string query = "UPDATE contact Set NAME=@name, EMAIL=@email, IMAGE=@img Where NUMBER=@nbr";
            command.CommandText = query;
            command.Connection = conn.GetConnection();

            command.Parameters.Add("@name", MySqlDbType.VarChar).Value = Username.Text;
            command.Parameters.Add("@nbr", MySqlDbType.VarChar).Value = textBox3.Text;
            command.Parameters.Add("@email", MySqlDbType.VarChar).Value = textBox2.Text;
            command.Parameters.Add("@img", MySqlDbType.Blob).Value = img;
            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.HasErrors)
            {
                MessageBox.Show("Try Again..");
            }
            else
            {
                MessageBox.Show("Contact Updated....!");
                this.Hide();
            }

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            Connect conn = new Connect();
            MySqlCommand command = new MySqlCommand();
            string query = "Delete From contact Where NUMBER=@nbr";
            command.CommandText = query;
            command.Connection = conn.GetConnection();

            command.Parameters.Add("@nbr", MySqlDbType.VarChar).Value = textBox3.Text;
            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.HasErrors)
            {
                MessageBox.Show("Try Again..");
            }
            else
            {
                
                MessageBox.Show("Contact Deleted....!");
                this.Hide();
            }
        }

        private void ExitBtn_MouseEnter(object sender, EventArgs e)
        {
            ExitBtn.BackColor = Color.Red;
            ExitBtn.ForeColor = Color.White;
        }

        private void ExitBtn_MouseLeave(object sender, EventArgs e)
        {
            ExitBtn.BackColor = Color.FromArgb(63, 63, 63);
            ExitBtn.ForeColor = Color.Silver;
        }
    }
}
