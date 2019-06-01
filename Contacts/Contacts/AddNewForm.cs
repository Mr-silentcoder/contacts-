using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Contacts
{
    public partial class AddNewForm : Form
    {
        int screen, screenX, screenY;
        public AddNewForm()
        {
            InitializeComponent();
        }

        private void ExitBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            screen = 1;
            screenX = e.X;
            screenY = e.Y;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if(screen == 1)
            {
                SetDesktopLocation(MousePosition.X - screenX, MousePosition.Y - screenY);
            }
        }



        private void panel1_MouseUp_1(object sender, MouseEventArgs e)
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

        private void dataclear()
        {
            Username.Text = "";
            phone.Text = "";
            email.Text = "";
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
            string query = "INSERT INTO contact(NAME,NUMBER,EMAIL,IMAGE) VALUES(@name,@nbr,@email,@img)";
            command.CommandText = query;
            command.Connection = conn.GetConnection();

            command.Parameters.Add("@name", MySqlDbType.VarChar).Value = Username.Text;
            command.Parameters.Add("@nbr", MySqlDbType.VarChar).Value = phone.Text;
            command.Parameters.Add("@email", MySqlDbType.VarChar).Value = email.Text;
            command.Parameters.Add("@img", MySqlDbType.Blob).Value = img;
            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.HasErrors)
            {
                MessageBox.Show("Check Data..");
            }
            else
            {
                MessageBox.Show("New Contact Added....!");
                dataclear();

            }

           

        }

        private void AddNewForm_Load(object sender, EventArgs e)
        {

        }

        private void ExitBtn_MouseEnter(object sender, EventArgs e)
        {
            ExitBtn.ForeColor = Color.White;
            ExitBtn.BackColor = Color.Red;
        }

        private void ExitBtn_MouseLeave(object sender, EventArgs e)
        {
            ExitBtn.ForeColor = Color.Silver;
            ExitBtn.BackColor = Color.FromArgb(63, 63, 63);
        }
    }
}
