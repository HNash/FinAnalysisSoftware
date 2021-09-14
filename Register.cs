using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Runtime.Serialization.Formatters.Binary;

namespace OAP_CS
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }

        private void registerBtn_Click(object sender, EventArgs e)
        {
            if(userEntryBox.Text == "" || passEntryBox.Text == "" || passConfBox.Text == "")
            {
                MessageBox.Show("Please do not leave any fields blank.", "Login Failed");
                return;
            }
            else if(passEntryBox.Text != passConfBox.Text)
            {
                MessageBox.Show("The password confirmation does not match the password you have entered. Please try again.", "Login Failed");
                return;
            }
            else
            {
                File.AppendAllText("userdata.bat", ("\n" + userEntryBox.Text + "," + Login.ComputeSha256Hash(passEntryBox.Text)));
                this.Visible = false;
                Form1 mainPage = new Form1(userEntryBox.Text);
                mainPage.ShowDialog();
                this.Close();
            }
        }
    }
}
