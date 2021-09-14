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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        // Hides this page and opens main page
        private void guestBtn_Click(object sender, EventArgs e)
        {
            this.Visible = false; 
            Form1 mainPage = new Form1("");
            mainPage.ShowDialog();
            this.Close();
        }

        private void loginBtn_Click(object sender, EventArgs e)
        {
            // Takes the username and password entered by the user
            string inputUser = userEntryBox.Text;
            string inputPass = passEntryBox.Text;

            // Checks if fields were left blank
            if(inputUser == "" || inputPass == "")
            {
                MessageBox.Show("Please enter a username and password.", "Login Failed");
                return;
            }

            // Will store the contents of the username/password data file
            string fileText;
            StreamReader SR = new StreamReader("userdata.bat");
            fileText = SR.ReadToEnd();
            SR.Close();
            SR.Dispose();

            // Splits the contents of the file into username/hashed-password pairs
            string[] fileSplit = fileText.Split("\n");

            // Will store the user/hashed-password pair that the user wants, if they entered the correct username
            string userHashCombo = "";

            // Loops over logins looking for the one that the user entered
            foreach (string s in fileSplit)
            {
                if (s.Split(",")[0] == inputUser.ToLower())
                {
                    userHashCombo = s;
                }
            }

            // If the username was not found, an error message is displayed
            if(userHashCombo == "")
            {
                MessageBox.Show("The username you have entered is not registered. Please try again.", "Login Failed");
                return;
            }
            // Otherwise, if the username was found, the hash of the correct password is compared to the hash of the one entered by the user
            else if (userHashCombo.Split(",")[1] == ComputeSha256Hash(inputPass))
            {
                this.Visible = false; 
                Form1 mainPage = new Form1(userEntryBox.Text);
                mainPage.ShowDialog();
                this.Close();
            }
            // If the else-if fails, the password is incorrect
            else
            {
                MessageBox.Show("The password you have entered is incorrect. Please try again.", "Login Failed");
                return;
            }
        }

        // Helper function to compute hash of entered password
        public static string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        // Triggers if the user clicks the register link
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Visible = false;
            Register registerPage = new Register();
            registerPage.ShowDialog();
            this.Close();
        }
    }
}
