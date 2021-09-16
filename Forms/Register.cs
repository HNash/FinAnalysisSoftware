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
            foreach(char c in userEntryBox.Text)
            {
                if (!(c >= 48 && c <= 57) && !(c >= 65 && c <= 90) && !(c >= 97 && c <= 122) && !(c=='-') && !(c=='_'))
                {
                    MessageBox.Show("Usernames can only contain letters, numbers, underscores and dashes.", "Registration Failed");
                    return;
                }
            }

            string[] existingUsers = Directory.GetDirectories("data/");
            foreach (string s in existingUsers)
            {
                if (s.ToLower() == userEntryBox.Text.ToLower())
                {
                    MessageBox.Show("That username has already been registered.", "Registration Failed");
                    return;
                }
            }
            if (userEntryBox.Text == "" || passEntryBox.Text == "" || passConfBox.Text == "")
            {
                MessageBox.Show("Please do not leave any fields blank.", "Registration Failed");
                return;
            }
            else if (userEntryBox.Text.Length < 3 || userEntryBox.Text.Length > 20)
            {
                MessageBox.Show("The username must be between 3 and 20 characters long.", "Registration Failed");
                return;
            }
            else if (passEntryBox.Text.Length < 6 || passEntryBox.Text.Length > 20)
            {
                MessageBox.Show("The password must be between 6 and 20 characters long.", "Registration Failed");
                return;
            }
            else if(passEntryBox.Text != passConfBox.Text)
            {
                MessageBox.Show("The password confirmation does not match the password you have entered. Please try again.", "Registration Failed");
                return;
            }
            else
            {
                File.AppendAllText("data/userdata.dat", ("\n" + userEntryBox.Text + "," + Login.ComputeSha256Hash(passEntryBox.Text)));
                Directory.CreateDirectory("data/" + userEntryBox.Text);
                this.Visible = false;
                MainPage mainPage = new MainPage(userEntryBox.Text);
                mainPage.ShowDialog();
                this.Close();
                return;
            }
        }
    }
}
