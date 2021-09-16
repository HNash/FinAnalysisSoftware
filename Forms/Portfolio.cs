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
            EDIT = 2,
            SAVE = 3,
            DELETE = 4,
            DELETEASSET = 5
        }
        private mode pageMode;

        private string selectedPortfolio;

        // Used to fill out the combo box (list of portfolio names)
        private string[] portfolios;

        // Page contents differ based on mode
        public Portfolio(mode m)
        {
            pageMode = m;
            
            // Sets up page contents based on initialization mode
            if (pageMode == mode.CREATE) // For creation of new portfolio
            {
                InitializeComponent();
                mainLabel.Text = "Enter Portfolio Name:";
                actionBtn.Text = "Create";
                textBox.Visible = true;
                comboBox.Visible = false;
                listBox.Visible = false;
                this.Size = new Size(550, 310);
            }
            else if (pageMode == mode.LOAD) // To load an existing portfolio 
            {
                InitializeComponent();
                mainLabel.Text = "Select Portfolio:";
                actionBtn.Text = "Load";
                textBox.Visible = false;
                comboBox.Visible = true;
                listBox.Visible = true;
                this.Size = new Size(550, 980);

                // Fetch portfolio names in user's data directory
                portfolios = Directory.GetDirectories("data/" + MainPage.username);
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
            else if (pageMode == mode.EDIT) // To edit an existing portfolio
            {
                InitializeComponent();
                mainLabel.Text = "Select Portfolio:";
                actionBtn.Text = "Load";
                textBox.Visible = false;
                comboBox.Visible = true;
                listBox.Visible = true;
                this.Size = new Size(550, 980);

                // Fetch portfolio names in user's data directory
                portfolios = Directory.GetDirectories("data/" + MainPage.username);
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
            else if (pageMode == mode.SAVE) // For saving of an asset to an existing portfolio
            {
                InitializeComponent();
                mainLabel.Text = "Select Portfolio:";
                actionBtn.Text = "Save";
                textBox.Visible = false;
                comboBox.Visible = true;
                listBox.Visible = false;
                this.Size = new Size(550, 310);

                // Fetch portfolio names in user's data directory
                portfolios = Directory.GetDirectories("data/" + MainPage.username);
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
            else if (pageMode == mode.DELETE) // For deletion of a portfolio
            {
                InitializeComponent();
                mainLabel.Text = "Select Portfolio:";
                actionBtn.Text = "Delete";
                textBox.Visible = false;
                comboBox.Visible = true;
                listBox.Visible = false;
                this.Size = new Size(550, 310);

                // Fetch portfolio names in user's data directory
                portfolios = Directory.GetDirectories("data/" + MainPage.username);
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
            // ----- This statement is true if the portfolio window was opened from the "Create Portfolio" button
            if (pageMode == mode.CREATE)
            {
                //Fetches the name entered by the user into the textbox
                string name = textBox.Text;
                
                // Input validation
                if (name.Length < 3 || name.Length > 20)
                {
                    MessageBox.Show("Portfolio name must be between 3 and 20 characters. Please try again.", "Invalid Input");
                    return;
                }
                foreach (char c in textBox.Text) // Only allows letters, numbers and underscores/dashes
                {
                    if (!(c >= 48 && c <= 57) && !(c >= 65 && c <= 90) && !(c >= 97 && c <= 122) && !(c == '-') && !(c == '_'))
                    {
                        MessageBox.Show("Portfolio names can only contain letters, numbers, underscores and dashes.", "Invalid Input");
                        return;
                    }
                }

                // Makes a new folder in the user's directory for the portfolio
                Directory.CreateDirectory("data/" + MainPage.username + "/" + textBox.Text);

                // Closes portfolio window
                this.Visible = false;
                MessageBox.Show("Portfolio successfully created.", "Success");
                this.Close();
                return;
            }

            // ----- This statement is true if the portfolio window was opened from the "Load Existing Portfolio" button
            else if (pageMode == mode.LOAD)
            {
                // Clears the listbox used to display contents of portfolio
                listBox.Items.Clear();

                // Checks that a portfolio has actually been selected
                if (comboBox.Text == "")
                {
                    MessageBox.Show("You have not selected a portfolio to load.", "Select Portfolio");
                    return;
                }
                
                // List of paths to asset binary files
                string[] assets = Directory.GetFiles("data/" + MainPage.username + "/" + comboBox.Text + "/");

                // Loops over paths to asset files and converts them to Item objects, storing them in loadedPortfolio
                foreach (string s in assets)
                {
                    Item i = ReadFromBinaryFile<Item>(s);

                    string[] inputs = i.getParameters();

                    foreach (string param in inputs)
                    {
                        listBox.Items.Add(param);
                    }

                    listBox.Items.Add("");

                    ArrayList results = i.getResults();

                    foreach (string result in results)
                    {
                        listBox.Items.Add(result);
                    }

                    // Blank lines to separate assets from each other
                    listBox.Items.Add("");
                    listBox.Items.Add("");
                }
                comboBox.Text = "";
            }

            // ----- This statement is true if the portfolio window was opened from the "Edit Existing Portfolio" button
            // CAREFUL: Same as load up until //***
            else if (pageMode == mode.EDIT)
            {
                // Clears the listbox used to display contents of portfolio
                listBox.Items.Clear();

                // Checks that a portfolio has actually been selected
                if (comboBox.Text == "")
                {
                    MessageBox.Show("You have not selected a portfolio to load.", "Select Portfolio");
                    return;
                }

                // List of paths to asset binary files
                string[] assets = Directory.GetFiles("data/" + MainPage.username + "/" + comboBox.Text);

                // Loops over paths to asset files and converts them to Item objects, storing them in loadedPortfolio
                foreach (string s in assets)
                {
                    Item i = ReadFromBinaryFile<Item>(s);

                    string[] inputs = i.getParameters();

                    foreach (string param in inputs)
                    {
                        listBox.Items.Add(param);
                    }

                    listBox.Items.Add("");

                    ArrayList results = i.getResults();

                    foreach (string result in results)
                    {
                        listBox.Items.Add(result);
                    }

                    // Blank lines to separate assets from each other
                    listBox.Items.Add("");
                    listBox.Items.Add("");
                }
                //***
                comboBox.Items.Clear();

                foreach (string s in assets)
                {
                    string t = s.Split("\\")[s.Split("\\").Length-1];
                    t = t.Split(".")[0];
                    comboBox.Items.Add(t);
                }

                comboBox.Text = "";
                mainLabel.Text = "Select Asset to Delete:";
                actionBtn.Text = "Delete";
                pageMode = mode.DELETEASSET;
            }

            // ----- This statement is true if the portfolio window was opened from the "Save Asset" button
            else if (pageMode == mode.SAVE) 
            {
                // Constructs the path string to the asset's soon to be created binary .dat file
                string path = "data/" + MainPage.username + "/" + comboBox.Text + "/" + MainPage.asset.name + ".dat";

                // Checks if a portfolio has actually been selected for the asset to be saved
                if (comboBox.Text == "")
                {
                    MessageBox.Show("You have not selected a portfolio.", "Select Portfolio");
                    return;
                }
                // Checks if the file already exists
                else if (File.Exists(path))
                {
                    this.Visible = false;
                    MessageBox.Show("An asset with that name has already been saved to the portfolio.", "Error");
                    this.Close();
                    return;
                }
                // If checks are passed, write the asset to a new binary .dat file
                else
                {
                    WriteToBinaryFile<Item>(path, MainPage.asset);
                    this.Visible = false;
                    MessageBox.Show("Asset has been saved successfully.", "Success");
                    this.Close();
                    return;
                }
            }

            // ----- This statement is true if the portfolio window was opened from the "Delete Portfolio" button
            else if (pageMode == mode.DELETE)
            {
                // Checks if a portfolio has actually been selected for deletion
                if(comboBox.Text == "")
                {
                    MessageBox.Show("You have not selected a portfolio to delete.", "Select Portfolio");
                    return;
                }

                // If the check has passed, the portfolio directory is emptied then deleted
                string[] portfolioContents = Directory.GetFiles("data/" + MainPage.username + "/" + comboBox.Text);
                foreach(string s in portfolioContents)
                {
                    File.Delete(s);
                }
                Directory.Delete("data/" + MainPage.username + "/" + comboBox.Text);

                // Refreshes drop down menu of portfolio names by fetching portfolio directory names
                portfolios = Directory.GetDirectories("data/" + MainPage.username + "/");

                comboBox.Items.Clear();

                foreach (string s in portfolios)
                {
                    comboBox.Items.Add(s);
                }

                // Close portfolio window
                this.Visible = false;
                MessageBox.Show("Portfolio successfully deleted.", "Success");
                this.Close();
                return;
            }

            else // If pageMode == DELETEASSET, a secondary mode from EDIT
            {
                // First the asset binary file is simply deleted
                string path = "data/" + MainPage.username + "/" + selectedPortfolio + "/" + comboBox.Text + ".dat";
                File.Delete(path);

                // Then the remaining asset file names are fetched
                string[] assets = Directory.GetFiles("data/" + MainPage.username + "/" + selectedPortfolio);

                // Fist the listbox is cleared...
                listBox.Items.Clear();

                // ...then the listbox is filled with the new portfolio contents
                foreach (string s in assets)
                {
                    Item i = ReadFromBinaryFile<Item>(s);

                    string[] inputs = i.getParameters();

                    foreach (string param in inputs)
                    {
                        listBox.Items.Add(param);
                    }

                    listBox.Items.Add("");

                    ArrayList results = i.getResults();

                    foreach (string result in results)
                    {
                        listBox.Items.Add(result);
                    }

                    // Blank lines to separate assets from each other
                    listBox.Items.Add("");
                    listBox.Items.Add("");
                }

                // The combo box is cleared...
                comboBox.Items.Clear();

                // ...then it is filled with the remaining asset names
                foreach (string s in assets)
                {
                    string t = s.Split("\\")[s.Split("\\").Length-1];
                    t = t.Split(".")[0];
                    comboBox.Items.Add(t);
                }

                MessageBox.Show("Asset successfully deleted.", "Success");
                comboBox.Text = "";
                return;
            }
        }

        private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(pageMode != mode.DELETEASSET)
            {
                selectedPortfolio = comboBox.Text;
            }
        }
    }
}
