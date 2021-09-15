using System;
using System.IO;
using System.Windows.Forms;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAP_CS
{
    public partial class Portfolio : Form
    {
        /* _________________________ FILE STRUCTURE: _________________________
           ----- data/
           ---------- [USERNAME]/
           --------------- [PORTFOLIO]/
           -------------------- [ASSET NAME].dat                                */

        // Used to indicate purpose of window during initialization
        public enum mode
        {
            CREATE = 0,
            LOAD = 1,
            SAVE = 2,
            DELETE = 3
        }
        private mode pageMode;
        
        // Used to fill out the combo box (list of portfolio names)
        private string[] portfolios;
        // Used when mode = LOAD. Contents will be converted to strings and displayed on main page
        public ArrayList loadedPortfolio;

        // Page contents differ based on mode
        public Portfolio(mode m)
        {
            InitializeComponent();

            // If the user is logged in as a guest, an error dialog is displayed and the page is closed
            if (Form1.username == "")
            {
                this.Visible = false;
                MessageBox.Show("You are using OAP as a guest. Login or register to use portfolios.", "Error");
                this.Close();
                return;
            }

            pageMode = m;

            // Sets up page contents based on initialization mode
            if (pageMode == mode.CREATE) // For creation of new portfolio
            {
                mainLabel.Text = "Enter Portfolio Name:";
                actionBtn.Text = "Create";
                textBox.Visible = true;
                comboBox.Visible = false;
                listBox.Visible = false;
                this.Size = new Size(550, 310);
            }
            else if (pageMode == mode.LOAD) // To load an existing portfolio on the main page
            {
                mainLabel.Text = "Select Portfolio:";
                actionBtn.Text = "Load";
                textBox.Visible = false;
                comboBox.Visible = true;
                listBox.Visible = true;
                this.Size = new Size(550, 980);

                // Fetch portfolio names in user's data directory
                portfolios = Directory.GetDirectories("data/" + Form1.username);
                for(int i = 0; i < portfolios.Length; ++i)
                {
                    portfolios[i] = portfolios[i].Split("\\")[1];
                }

                // Clear portfolio combo box and fill it out with fetched portfolio names
                comboBox.Items.Clear();
                foreach (string s in portfolios)
                {
                    comboBox.Items.Add(s);
                }
            }
            else if (pageMode == mode.SAVE) // For saving of an asset to an existing portfolio
            {
                mainLabel.Text = "Select Portfolio:";
                actionBtn.Text = "Save";
                textBox.Visible = false;
                comboBox.Visible = true;
                listBox.Visible = false;
                this.Size = new Size(550, 310);

                // Fetch portfolio names in user's data directory
                portfolios = Directory.GetDirectories("data/" + Form1.username);
                for (int i = 0; i < portfolios.Length; ++i)
                {
                    portfolios[i] = portfolios[i].Split("\\")[1];
                }

                // Clear portfolio combo box and fill it out with fetched portfolio names
                comboBox.Items.Clear();
                foreach (string s in portfolios)
                {
                    comboBox.Items.Add(s);
                }
            }
            else // For deletion of a portfolio
            {
                mainLabel.Text = "Select Portfolio:";
                actionBtn.Text = "Delete";
                textBox.Visible = false;
                comboBox.Visible = true;
                listBox.Visible = false;
                this.Size = new Size(550, 310);

                // Fetch portfolio names in user's data directory
                portfolios = Directory.GetDirectories("data/" + Form1.username);
                for (int i = 0; i < portfolios.Length; ++i)
                {
                    portfolios[i] = portfolios[i].Split("\\")[1];
                }

                // Clear portfolio combo box and fill it out with fetched portfolio names
                comboBox.Items.Clear();
                foreach (string s in portfolios)
                {
                    comboBox.Items.Add(s);
                }
            }
        }

        public ArrayList getPort()
        {
            return loadedPortfolio;
        }

        // Method to save individual asset as a binary file
        public static void WriteToBinaryFile<Item>(string filePath, Item objectToWrite)
        {
            if (!File.Exists(filePath))
            {
                BinaryWriter binWriter = new BinaryWriter(File.Open(filePath, FileMode.Create));
                binWriter.Close();
            }

            using (Stream stream = File.Open(filePath, FileMode.Append))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                binaryFormatter.Serialize(stream, objectToWrite);
            }
        }

        // Method to load an individual asset from its binary file
        public static Item ReadFromBinaryFile<Item>(string filePath)
        {
            using (Stream stream = File.Open(filePath, FileMode.Open))
            {
                stream.Position = 0;
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                return (Item)binaryFormatter.Deserialize(stream);
            }
        }

        public static void CreateBinaryFile(string filePath)
        {
            BinaryWriter binWriter = new BinaryWriter(File.Open(filePath, FileMode.Create));
        }

        private void actionBtn_Click(object sender, EventArgs e)
        {
            if (pageMode == mode.CREATE)
            {
                string name = textBox.Text;
                
                if(Form1.username == "")
                {
                    MessageBox.Show("You are using OAP as a guest. Login or register to create portfolios.", "Error");
                    return;
                }
                else if (name.Length < 3 || name.Length > 20)
                {
                    MessageBox.Show("Portfolio name must be between 3 and 20 characters. Please try again.", "Invalid Input");
                    return;
                }
                foreach (char c in textBox.Text)
                {
                    if (!(c >= 48 && c <= 57) && !(c >= 65 && c <= 90) && !(c >= 97 && c <= 122) && !(c == '-') && !(c == '_'))
                    {
                        MessageBox.Show("Portfolio names can only contain letters, numbers, underscores and dashes.", "Invalid Input");
                        return;
                    }
                }
                Directory.CreateDirectory("data/" + Form1.username + "/" + textBox.Text);
                this.Visible = false;
                MessageBox.Show("Portfolio successfully created.", "Success");
                this.Close();
                return;
            }
            else if (pageMode == mode.LOAD)
            {
                loadedPortfolio = new ArrayList();
                listBox.Items.Clear();

                if (comboBox.Text == "")
                {
                    MessageBox.Show("You have not selected a portfolio to load.", "Select Portfolio");
                    return;
                }
                string[] assets = Directory.GetFiles("data/" + Form1.username + "/" + comboBox.Text + "/");
                for (int i = 0; i < assets.Length; ++i)
                {
                    assets[i] = assets[i].Split("/")[assets[i].Split("/").Length-1];
                }
                foreach (string s in assets)
                {
                    Item i = ReadFromBinaryFile<Item>("data/" + Form1.username + "/" + comboBox.Text + "/" + s);
                    loadedPortfolio.Add(i);
                }
                for (int i = 0; i < loadedPortfolio.Count; ++i)
                {
                    for (int j = 0; j < ((Item)(loadedPortfolio[i])).parameterNames.Length; ++j)
                    {
                        listBox.Items.Add(((Item)(loadedPortfolio[i])).parameterNames[j] + " " + ((Item)(loadedPortfolio[i])).parameters[j]);
                    }

                    listBox.Items.Add("");

                    ArrayList results = ((Item)(loadedPortfolio[i])).getResults();

                    foreach(string s in results)
                    {
                        listBox.Items.Add(s);
                    }

                    listBox.Items.Add("");
                    listBox.Items.Add("");
                }                
            }

            else if (pageMode == mode.SAVE) 
            {
                string path = "data/" + Form1.username + "/" + comboBox.Text + "/" + Form1.asset.name + ".dat";
                if (comboBox.Text == "")
                {
                    MessageBox.Show("You have not selected a portfolio.", "Select Portfolio");
                    return;
                }
                else if (File.Exists(path))
                {
                    this.Visible = false;
                    MessageBox.Show("An asset with that name has already been saved to the portfolio.", "Error");
                    this.Close();
                    return;
                }
                else
                {
                    WriteToBinaryFile<Item>(path, Form1.asset);
                    this.Visible = false;
                    MessageBox.Show("Asset has been saved successfully.", "Success");
                    this.Close();
                    return;
                }
            }
            else
            {
                if(comboBox.Text == "")
                {
                    MessageBox.Show("You have not selected a portfolio to delete.", "Select Portfolio");
                    return;
                }

                Directory.Delete("data/" + Form1.username + "/" + comboBox.Text);

                portfolios = Directory.GetDirectories("data/" + Form1.username + "/");

                comboBox.Items.Clear();

                foreach (string s in portfolios)
                {
                    comboBox.Items.Add(s);
                }

                this.Visible = false;
                MessageBox.Show("Portfolio successfully deleted.", "Success");
                this.Close();
                return;
            }
        }
    }
}
