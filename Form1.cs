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
    public partial class Form1 : Form
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
        Item asset;

        // Used for portfolios
        string username;

        public Form1(string u)
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

            clearPage();

            // -----Spaghetti code that initializes the factory pointer to point to the factory method of the desired asset and sets up labels-----
            if (selected == "-Bond")
            {
                factory = Bond.factory;

                priceAssetBtn.Visible = true;
                saveAssetBtn.Visible = true;
                resultsBox.Visible = true;

                for(int i = 0; i < 6; ++i)
                {
                    ((Label)labels[i]).Visible = true;
                    ((TextBox)textBoxes[i]).Visible = true;
                }

                // I've tried to move this junk down to each asset class but cannot figure out
                // If, for some reason, anyone other than me is reading this and can tell me how to do it, let me know
                label2.Text = "Face Value:";
                label3.Text = "Coupon Rate (%):";
                label4.Text = "Coupon Frequency:";
                label5.Text = "Time to Maturity (Yrs):";
                label6.Text = "Ann. Interest Rate (%):";
            }
            else if (selected == "-Callable Bond")
            {
                factory = CallableBond.factory;

                priceAssetBtn.Visible = true;
                saveAssetBtn.Visible = true;
                resultsBox.Visible = true;

                for (int i = 0; i < 9; ++i)
                {
                    ((Label)labels[i]).Visible = true;
                    ((TextBox)textBoxes[i]).Visible = true;
                }

                label2.Text = "Face Value:";
                label3.Text = "Coupon Rate (%):";
                label4.Text = "Coupon Frequency:";
                label5.Text = "Time to Maturity (Yrs):";
                label6.Text = "Ann. Interest Rate (%):";
                label7.Text = "Call Price:";
                label8.Text = "Ann. Forward Vol. (%):";
                label9.Text = "Time to Call (Yrs):";
            }
            else if (selected == "-Convertible Bond")
            {
                factory = ConvertibleBond.factory;

                priceAssetBtn.Visible = true;
                saveAssetBtn.Visible = true;
                resultsBox.Visible = true;

                for (int i = 0; i < 9; ++i)
                {
                    ((Label)labels[i]).Visible = true;
                    ((TextBox)textBoxes[i]).Visible = true;
                }

                label2.Text = "Face Value:";
                label3.Text = "Coupon Rate (%):";
                label4.Text = "Coupon Frequency:";
                label5.Text = "Time to Maturity (Yrs):";
                label6.Text = "Ann. Interest Rate (%):";
                label7.Text = "Stock Price:";
                label8.Text = "Conversion Price:";
                label9.Text = "Ann. Price Vol. (%):";
            }
            else if (selected == "-Zero Coupon Bond")
            {
                factory = ZeroCouponBond.factory;

                priceAssetBtn.Visible = true;
                saveAssetBtn.Visible = true;
                resultsBox.Visible = true;

                for (int i = 0; i < 4; ++i)
                {
                    ((Label)labels[i]).Visible = true;
                    ((TextBox)textBoxes[i]).Visible = true;
                }

                label2.Text = "Face Value:";
                label3.Text = "Time to Maturity (Yrs):";
                label4.Text = "Ann. Interest Rate (%):";
            }
            else if (selected == "-Perpetuity")
            {
                factory = Perpetuity.factory;

                priceAssetBtn.Visible = true;
                saveAssetBtn.Visible = true;
                resultsBox.Visible = true;

                for (int i = 0; i < 3; ++i)
                {
                    ((Label)labels[i]).Visible = true;
                    ((TextBox)textBoxes[i]).Visible = true;
                }

                label2.Text = "Payment:";
                label3.Text = "Ann. Interest Rate (%):";
            }
            else if (selected == "-American Option")
            {
                factory = AmericanOption.factory;

                priceAssetBtn.Visible = true;
                saveAssetBtn.Visible = true;
                resultsBox.Visible = true;

                for (int i = 0; i < 7; ++i)
                {
                    ((Label)labels[i]).Visible = true;
                    ((TextBox)textBoxes[i]).Visible = true;
                }
                putCheck.Visible = true;
                putCheckBox.Visible = true;

                label2.Text = "Stock Price:";
                label3.Text = "Strike Price:";
                label4.Text = "Time to Expiry (Yrs):";
                label5.Text = "Ann. Interest Rate (%):";
                label6.Text = "Ann. Price Vol. (%):";
                label7.Text = "Desired Time Steps:";
            }
            else if (selected == "-European Option")
            {
                factory = EuropeanOption.factory;

                priceAssetBtn.Visible = true;
                saveAssetBtn.Visible = true;
                resultsBox.Visible = true;

                for (int i = 0; i < 7; ++i)
                {
                    ((Label)labels[i]).Visible = true;
                    ((TextBox)textBoxes[i]).Visible = true;
                }
                putCheck.Visible = true;
                putCheckBox.Visible = true;

                label2.Text = "Stock Price:";
                label3.Text = "Strike Price:";
                label4.Text = "Time to Expiry (Yrs):";
                label5.Text = "Ann. Interest Rate (%):";
                label6.Text = "Ann. Price Vol. (%):";
                label7.Text = "Desired Time Steps:";
            }
            else if (selected == "-Bond Option")
            {
                factory = BondOption.factory;

                priceAssetBtn.Visible = true;
                saveAssetBtn.Visible = true;
                resultsBox.Visible = true;

                for (int i = 0; i < 6; ++i)
                {
                    ((Label)labels[i]).Visible = true;
                    ((TextBox)textBoxes[i]).Visible = true;
                }
                putCheck.Visible = true;
                putCheckBox.Visible = true;

                label2.Text = "Forward Price:";
                label3.Text = "Call Price:";
                label4.Text = "Ann. Forward Vol. (%):";
                label5.Text = "Time to Call (Yrs):";
                label6.Text = "Ann. Interest Rate (%):";
            }
            else
            {
                comboBox1.Text = "";
            }
        }

        // Triggers when user clicks button to price the asset they're creating
        private void priceAssetBtn_Click(object sender, EventArgs e)
        {
            // To hold the inputs that the user has entered in the text boxes
            inputs = new ArrayList();

            // Loops through text boxes to validate input
            for (int i = 1; i < 9; ++i)
            {
                /* dConvert is a """proprietary""" way to cast strings to doubles because C#s method sucks.
                   The number -262144.123456789 is used to indicate invalid input.
                   I am aware that this is a terrible way to validate input.*/
                if (Item.dConvert(((TextBox)textBoxes[i]).Text) == -262144.123456789)
                {
                    // Finds the "name" of the parameter for which the user has entered invalid input and removes the ":" at the end of it
                    string badInputName = (((Label)labels[i]).Text).Remove((((Label)labels[i]).Text).Length - 1, 1);
                    // Displays error message
                    MessageBox.Show("Your entry for " + ((Label)labels[i]).Text + " is invalid. Please enter a valid input.");
                    return;
                }
            }

            // Takes the inputs that the user has put in the text boxes and adds them to the input array list
            foreach (TextBox t in textBoxes)
            {
                if (t.Text != "")
                {
                    inputs.Add(t.Text);
                }
            }

            // If the asset has call/put variants, then the variant indicated is added to the input array list
            if(putCheckBox.Visible)
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


        // ------------------------------PORTFOLIOS TAB-----------------------------------

        private void portfolioList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
