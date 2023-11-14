using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace FAP
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Home_Load(object sender, EventArgs e)
        {
            //button1.BackColor = Color.Orange;
            //button2.BackColor = Color.Orange;
        }

        

        private void button4_Click(object sender, EventArgs e)
        {

            if (comboBox1.SelectedIndex > -1)
            {
                HomePage form1 = new HomePage();
                this.Hide();
                form1.ShowDialog();
                this.Close();
                
            }
            else
            {
                
                Login login = new Login();
                this.Hide();
                login.ShowDialog();
                this.Close();
                
            }
        }
    }
}
