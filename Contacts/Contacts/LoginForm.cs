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
    public partial class LoginForm : Form
    {
        int screen, screenX, screenY;
        List<Panel> listPanel = new List<Panel>();
        int index;

        private void HeadPanel_MouseDown(object sender, MouseEventArgs e)
        {
            screen = 1;
            screenX = e.X;
            screenY = e.Y;
        }

        private void HeadPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if(screen == 1)
            {
                SetDesktopLocation(MousePosition.X - screenX, MousePosition.Y - screenY);
            }
        }

        private void HeadPanel_MouseUp(object sender, MouseEventArgs e)
        {
            screen = 0;
        }

        private void ExitBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
            
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

        private void label7_MouseEnter(object sender, EventArgs e)
        {
            label7.ForeColor = Color.Red;
        }

        private void label7_MouseLeave(object sender, EventArgs e)
        {
            label7.ForeColor = Color.White;
        }

        private void label4_Click(object sender, EventArgs e)
        {
            if(index < listPanel.Count - 1)
            {
                listPanel[++index].BringToFront();
            }
        }

        private void label12_Click(object sender, EventArgs e)
        {
            if(index > 0)
            {
                listPanel[--index].BringToFront();
            }
        }

        private void label8_MouseEnter(object sender, EventArgs e)
        {
            label8.ForeColor = Color.Red;
        }

        private void label8_MouseLeave(object sender, EventArgs e)
        {
            label8.ForeColor = Color.White;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Connect conn = new Connect();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand();
            String query = "SELECT * From user Where username=@usr AND password=@pass";

            command.CommandText = query;
            command.Connection = conn.GetConnection();

            command.Parameters.Add("@usr", MySqlDbType.VarChar).Value = Username.Text;
            command.Parameters.Add("@pass", MySqlDbType.VarChar).Value = Password.Text;
            adapter.SelectCommand = command;
            adapter.Fill(table);

            if(table.Rows.Count > 0)
            {
                MainForm mform = new MainForm();
                this.Hide();
                mform.Show();
            }
            else
            {
                if (Username.Text.Trim().Equals(""))
                {
                    MessageBox.Show("Enter Your Username To Login", "Empty Username", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                else if (Password.Text.Trim().Equals(""))
                {
                    MessageBox.Show("Enter Your Password To Login", "Empty Password", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                else
                {
                    MessageBox.Show("This Username Or Password Error...!", "Wrong Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }


        }

        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            listPanel.Add(panel2);
            listPanel.Add(panel7);
            listPanel[index].BringToFront();
        }
    }
}
