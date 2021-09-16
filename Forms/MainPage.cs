using System;
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
    public partial class MainPage : Form
    {
        // ------------------------------ASSET PRICER TAB-----------------------------------

        // Function pointer. When the asset type is selected from the drop down menu, this will point to its factory function.
        Func<ArrayList, Item> factory = null;

        // Holds the (initially blank and invisible) labels and textboxes that are in the page
        ArrayList labels = new ArrayList();
        ArrayList textBoxes = new ArrayList();

        // Will hold user inputs
        ArrayList inputs = new ArrayList();

        // Placeholder asset that will be initialized once the user submits their inputs
        public static Item asset = null;

        // Used for portfolios
        public static string username;

        // Used to match user's selection from drop down menu to a factory method
        public static Dictionary<string, Func<ArrayList, Item>> factoryBank = new Dictionary<string, Func<ArrayList, Item>>()
        {
            {"-Bond", Bond.factory},
            {"-Callable Bond", CallableBond.factory},
            {"-Convertible Bond", ConvertibleBond.factory},
            {"-Perpetuity", Perpetuity.factory},
            {"-Zero Coupon Bond", ZeroCouponBond.factory},
            {"-American Option", AmericanOption.factory},
            {"-European Option", EuropeanOption.factory},
            {"-Bond Option", BondOption.factory}
        };

        // Used to match user's selection from drop down menu to a list of parameter names
        public static Dictionary<string, string[]> parameterBank = new Dictionary<string, string[]>()
        {
            {"-Bond", Bond.parameterNames},
            {"-Callable Bond", CallableBond.parameterNames},
            {"-Convertible Bond", ConvertibleBond.parameterNames},
            {"-Perpetuity", Perpetuity.parameterNames},
            {"-Zero Coupon Bond", ZeroCouponBond.parameterNames},
            {"-American Option", AmericanOption.parameterNames},
            {"-European Option", EuropeanOption.parameterNames},
            {"-Bond Option", BondOption.parameterNames}
        };

        public MainPage(string u)
        {
            InitializeComponent();

            textBoxes.Add(textBox1);
            textBoxes.Add(textBox2);
            textBoxes.Add(textBox3);
            textBoxes.Add(textBox4);
            textBoxes.Add(textBox5);
            textBoxes.Add(textBox6);
            textBoxes.Add(textBox7);
            textBoxes.Add(textBox8);
            textBoxes.Add(textBox9);
            textBoxes.Add(textBox10);

            labels.Add(nameLabel);
            labels.Add(label2);
            labels.Add(label3);
            labels.Add(label4);
            labels.Add(label5);
            labels.Add(label6);
            labels.Add(label7);
            labels.Add(label8);
            labels.Add(label9);
            labels.Add(label10);

            username = u;

            if (u == "")
            {
                saveAssetBtn.Enabled = false;
            }
        }

        // Helper method that removes contents of page when needed
        private void clearPage()
        {
            foreach (TextBox t in textBoxes)
            {
                if (t.Text != "")
                {
                    t.Clear();
                }
            }

            for (int i = 0; i < 10; ++i)
            {
                ((Label)labels[i]).Visible = false;
                ((TextBox)textBoxes[i]).Visible = false;
            }

            putCheck.Visible = false;
            putCheckBox.Visible = false;

            resultsBox.Items.Clear();
            factory = null;
        }

        // Triggers when the user selects an asset to create from the dropdown box
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected = comboBox1.Text;
            string[] parameterNames;

            clearPage();

            foreach (KeyValuePair<string, Func<ArrayList, Item>> kvp in factoryBank)
            {
                if (kvp.Key == selected)
                {
                    factory = kvp.Value;
                    break;
                }
            }
            foreach (KeyValuePair<string, string[]> kvp in parameterBank)
            {
                if (kvp.Key == selected)
                {
                    parameterNames = kvp.Value;
                    goto skip;
                }
            }
            comboBox1.Text = "";
            return;
        skip:;

            for (int i = 0; i < parameterNames.Length; ++i)
            {
                if (parameterNames[i] == "Put?:")
                {
                    putCheck.Visible = true;
                    putCheckBox.Visible = true;
                }
                else
                {
                    ((Label)(labels[i])).Visible = true;
                    ((Label)(labels[i])).Text = parameterNames[i];
                    ((TextBox)(textBoxes[i])).Visible = true;
                }
            }

            priceAssetBtn.Visible = true;
            saveAssetBtn.Visible = true;
            resultsBox.Visible = true;
        }

        // Triggers when user clicks button to price the asset they're creating
        private void priceAssetBtn_Click(object sender, EventArgs e)
        {
            asset = null;

            // To hold the inputs that the user has entered in the text boxes
            inputs = new ArrayList();

            // Takes the inputs that the user has put in the text boxes and adds them to the input array list
            inputs.Add(textBox1.Text);
            for(int i = 1; i < textBoxes.Count; ++i)
            {
                string entry = ((TextBox)(textBoxes[i])).Text;
                if (entry != "")
                {
                    double entryConverted = Item.dConvert(entry);
                    /* dConvert is a """proprietary""" way to cast strings to doubles because C#s method sucks.
                       The number -262144.123456789 is used to indicate invalid input.
                       I am aware that this is a terrible way to validate input.*/
                    if (entryConverted == -262144.123456789)
                    {
                        // Finds the "name" of the parameter for which the user has entered invalid input and removes the ":" at the end of it
                        string badInputName = (((Label)labels[i]).Text).Remove((((Label)labels[i]).Text).Length - 1, 1);
                        // Displays error message
                        MessageBox.Show("Your entry for " + badInputName + " is invalid. Please enter a valid input.");
                        return;
                    }
                    else
                    {
                        inputs.Add(entryConverted);
                    }
                }
            }

            // If the asset has call/put variants, then the variant indicated is added to the input array list
            if (putCheckBox.Visible)
            {
                inputs.Add(putCheckBox.Checked ? "-1.0" : "1.0");
            }

            // Sets up the asset (i.e. prices it) by initializing it with the factory pointer
            if (factory != null)
            {
                asset = factory(inputs); // The asset is priced in its constructor at creation
                ArrayList displayList = asset.getResults(); // Array list that stores the results of the valuation

                foreach (string s in displayList)
                {
                    resultsBox.Items.Add(s); // Displays results in display box (listbox)
                }
                resultsBox.Items.Add(""); // Appends new line at the bottom of the display box
            }
        }
        private void saveAssetBtn_Click(object sender, EventArgs e)
        {
            if (asset == null)
            {
                MessageBox.Show("You have not yet priced the asset. Price it first to save it", "No Asset Found");
                return;
            }
            Portfolio createPage = new Portfolio(Portfolio.mode.SAVE);
            createPage.ShowDialog();
        }

        // ------------------------------PORTFOLIOS TAB-----------------------------------

        private void portBtn_Click(object sender, EventArgs e)
        {
            if(username == "")
            {
                MessageBox.Show("You are using OAP as a Guest. Login/Register to use portfolios", "Guest");
                return;
            }

            Portfolio createPage = null;

            if ((Button)sender == createPortBtn)
            {
                createPage = new Portfolio(Portfolio.mode.CREATE);
            }
            else if ((Button)sender == loadPortBtn)
            {
                createPage = new Portfolio(Portfolio.mode.LOAD);
            }
            else if ((Button)sender == editPortBtn)
            {
                createPage = new Portfolio(Portfolio.mode.EDIT);
            }
            else if ((Button)sender == deletePortBtn)
            {
                createPage = new Portfolio(Portfolio.mode.DELETE);
            }
            else
            {
                return;
            }

            createPage.ShowDialog();
        }
    }
}
