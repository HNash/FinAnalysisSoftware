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
        // Function pointer. When the asset type is selected from the drop down menu, this will point to its factory function.
        Func<ArrayList, Item> factory = null;

        ArrayList labels = new ArrayList();
        ArrayList textBoxes = new ArrayList();

        ArrayList inputs = new ArrayList();

        Item asset;

        public Form1()
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
        }

        private void clearPage()
        {
            foreach (TextBox t in textBoxes)
            {
                if (t.Text != "")
                {
                    t.Clear();
                }
            }

            resultsBox.Items.Clear();
            factory = null;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected = comboBox1.Text;

            clearPage();

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
                for (int i = 6; i < 10; ++i)
                {
                    ((Label)labels[i]).Visible = false;
                    ((TextBox)textBoxes[i]).Visible = false;
                }
                putCheck.Visible = false;
                putCheckBox.Visible = false;

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
                for (int i = 9; i < 10; ++i)
                {
                    ((Label)labels[i]).Visible = false;
                    ((TextBox)textBoxes[i]).Visible = false;
                }
                putCheck.Visible = false;
                putCheckBox.Visible = false;

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
                for (int i = 9; i < 10; ++i)
                {
                    ((Label)labels[i]).Visible = false;
                    ((TextBox)textBoxes[i]).Visible = false;
                }
                putCheck.Visible = false;
                putCheckBox.Visible = false;

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
                for (int i = 4; i < 10; ++i)
                {
                    ((Label)labels[i]).Visible = false;
                    ((TextBox)textBoxes[i]).Visible = false;
                }
                putCheck.Visible = false;
                putCheckBox.Visible = false;

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
                for (int i = 3; i < 10; ++i)
                {
                    ((Label)labels[i]).Visible = false;
                    ((TextBox)textBoxes[i]).Visible = false;
                }
                putCheck.Visible = false;
                putCheckBox.Visible = false;

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
                for (int i = 7; i < 10; ++i)
                {
                    ((Label)labels[i]).Visible = false;
                    ((TextBox)textBoxes[i]).Visible = false;
                }

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
                for (int i = 7; i < 10; ++i)
                {
                    ((Label)labels[i]).Visible = false;
                    ((TextBox)textBoxes[i]).Visible = false;
                }

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
                for (int i = 6; i < 10; ++i)
                {
                    ((Label)labels[i]).Visible = false;
                    ((TextBox)textBoxes[i]).Visible = false;
                }

                label2.Text = "Forward Price:";
                label3.Text = "Call Price:";
                label4.Text = "Ann. Forward Vol. (%):";
                label5.Text = "Time to Call (Yrs):";
                label6.Text = "Ann. Interest Rate (%):";
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void priceAssetBtn_Click(object sender, EventArgs e)
        {
            inputs = new ArrayList();

            foreach (TextBox t in textBoxes)
            {
                if (t.Text != "")
                {
                    inputs.Add(t.Text);
                }
            }

            if(putCheckBox.Visible)
            {
                inputs.Add(putCheckBox.Checked ? "-1.0" : "1.0");
            }

            if (factory != null)
            {
                asset = factory(inputs);
                ArrayList displayList = asset.getResults();

                foreach (string s in displayList)
                {
                    resultsBox.Items.Add(s);
                }
                resultsBox.Items.Add("");
            }
        }
    }
}
