using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Contacts
{
    public partial class SplahScreen : Form
    {
        public SplahScreen()
        {
            InitializeComponent();
            progressBar1.Visible = false;



        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Start();
            progressBar1.Increment(1);

            if(progressBar1.Value == 100)
            {
                timer1.Stop();
                this.Hide();
                LoginForm mfrom = new LoginForm();
                mfrom.Show();
            }
        }

        private void SplahScreen_Load(object sender, EventArgs e)
        {

        }
    }
}
