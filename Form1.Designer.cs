
namespace OAP_CS
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tabControl = new System.Windows.Forms.TabControl();
            this.assetPage = new System.Windows.Forms.TabPage();
            this.saveAssetBtn = new System.Windows.Forms.Button();
            this.priceAssetBtn = new System.Windows.Forms.Button();
            this.resultsBox = new System.Windows.Forms.ListBox();
            this.putCheckBox = new System.Windows.Forms.CheckBox();
            this.putCheck = new System.Windows.Forms.Label();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textBox10 = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.nameLabel = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.portfolioPage = new System.Windows.Forms.TabPage();
            this.tabControl.SuspendLayout();
            this.assetPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.assetPage);
            this.tabControl.Controls.Add(this.portfolioPage);
            this.tabControl.Font = new System.Drawing.Font("Bahnschrift", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tabControl.Location = new System.Drawing.Point(37, 35);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(810, 752);
            this.tabControl.TabIndex = 0;
            // 
            // assetPage
            // 
            this.assetPage.BackColor = System.Drawing.Color.Gray;
            this.assetPage.Controls.Add(this.saveAssetBtn);
            this.assetPage.Controls.Add(this.priceAssetBtn);
            this.assetPage.Controls.Add(this.resultsBox);
            this.assetPage.Controls.Add(this.putCheckBox);
            this.assetPage.Controls.Add(this.putCheck);
            this.assetPage.Controls.Add(this.textBox8);
            this.assetPage.Controls.Add(this.label8);
            this.assetPage.Controls.Add(this.textBox9);
            this.assetPage.Controls.Add(this.label9);
            this.assetPage.Controls.Add(this.textBox10);
            this.assetPage.Controls.Add(this.label10);
            this.assetPage.Controls.Add(this.textBox5);
            this.assetPage.Controls.Add(this.label5);
            this.assetPage.Controls.Add(this.textBox6);
            this.assetPage.Controls.Add(this.label6);
            this.assetPage.Controls.Add(this.textBox7);
            this.assetPage.Controls.Add(this.label7);
            this.assetPage.Controls.Add(this.textBox4);
            this.assetPage.Controls.Add(this.label4);
            this.assetPage.Controls.Add(this.textBox3);
            this.assetPage.Controls.Add(this.label3);
            this.assetPage.Controls.Add(this.textBox2);
            this.assetPage.Controls.Add(this.label2);
            this.assetPage.Controls.Add(this.textBox1);
            this.assetPage.Controls.Add(this.nameLabel);
            this.assetPage.Controls.Add(this.comboBox1);
            this.assetPage.Font = new System.Drawing.Font("Bahnschrift Condensed", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.assetPage.Location = new System.Drawing.Point(4, 28);
            this.assetPage.Name = "assetPage";
            this.assetPage.Padding = new System.Windows.Forms.Padding(3);
            this.assetPage.Size = new System.Drawing.Size(802, 720);
            this.assetPage.TabIndex = 0;
            this.assetPage.Text = "Asset Pricer";
            // 
            // saveAssetBtn
            // 
            this.saveAssetBtn.Font = new System.Drawing.Font("Bahnschrift", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.saveAssetBtn.Location = new System.Drawing.Point(613, 105);
            this.saveAssetBtn.Name = "saveAssetBtn";
            this.saveAssetBtn.Size = new System.Drawing.Size(138, 49);
            this.saveAssetBtn.TabIndex = 31;
            this.saveAssetBtn.Text = "Save Asset";
            this.saveAssetBtn.UseVisualStyleBackColor = true;
            this.saveAssetBtn.Visible = false;
            // 
            // priceAssetBtn
            // 
            this.priceAssetBtn.Font = new System.Drawing.Font("Bahnschrift", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.priceAssetBtn.Location = new System.Drawing.Point(461, 105);
            this.priceAssetBtn.Name = "priceAssetBtn";
            this.priceAssetBtn.Size = new System.Drawing.Size(138, 49);
            this.priceAssetBtn.TabIndex = 30;
            this.priceAssetBtn.Text = "Price Asset";
            this.priceAssetBtn.UseVisualStyleBackColor = true;
            this.priceAssetBtn.Visible = false;
            this.priceAssetBtn.Click += new System.EventHandler(this.priceAssetBtn_Click);
            // 
            // resultsBox
            // 
            this.resultsBox.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.resultsBox.Font = new System.Drawing.Font("Bahnschrift", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.resultsBox.FormattingEnabled = true;
            this.resultsBox.ItemHeight = 19;
            this.resultsBox.Location = new System.Drawing.Point(461, 195);
            this.resultsBox.Name = "resultsBox";
            this.resultsBox.Size = new System.Drawing.Size(290, 479);
            this.resultsBox.TabIndex = 28;
            this.resultsBox.Visible = false;
            // 
            // putCheckBox
            // 
            this.putCheckBox.AutoSize = true;
            this.putCheckBox.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.putCheckBox.Location = new System.Drawing.Point(239, 660);
            this.putCheckBox.Name = "putCheckBox";
            this.putCheckBox.Size = new System.Drawing.Size(15, 14);
            this.putCheckBox.TabIndex = 27;
            this.putCheckBox.UseVisualStyleBackColor = false;
            this.putCheckBox.Visible = false;
            // 
            // putCheck
            // 
            this.putCheck.AutoSize = true;
            this.putCheck.Font = new System.Drawing.Font("Bahnschrift", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.putCheck.Location = new System.Drawing.Point(58, 660);
            this.putCheck.Name = "putCheck";
            this.putCheck.Size = new System.Drawing.Size(40, 19);
            this.putCheck.TabIndex = 26;
            this.putCheck.Text = "Put?";
            this.putCheck.Visible = false;
            // 
            // textBox8
            // 
            this.textBox8.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.textBox8.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox8.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBox8.Location = new System.Drawing.Point(239, 497);
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new System.Drawing.Size(164, 15);
            this.textBox8.TabIndex = 25;
            this.textBox8.Visible = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Bahnschrift", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label8.Location = new System.Drawing.Point(58, 493);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(55, 19);
            this.label8.TabIndex = 24;
            this.label8.Text = "label8";
            this.label8.Visible = false;
            // 
            // textBox9
            // 
            this.textBox9.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.textBox9.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox9.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBox9.Location = new System.Drawing.Point(239, 546);
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new System.Drawing.Size(164, 15);
            this.textBox9.TabIndex = 23;
            this.textBox9.Visible = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Bahnschrift", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label9.Location = new System.Drawing.Point(58, 546);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(54, 19);
            this.label9.TabIndex = 22;
            this.label9.Text = "label9";
            this.label9.Visible = false;
            // 
            // textBox10
            // 
            this.textBox10.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.textBox10.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox10.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBox10.Location = new System.Drawing.Point(239, 603);
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new System.Drawing.Size(164, 15);
            this.textBox10.TabIndex = 21;
            this.textBox10.Visible = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Bahnschrift", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label10.Location = new System.Drawing.Point(58, 599);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(60, 19);
            this.label10.TabIndex = 20;
            this.label10.Text = "label10";
            this.label10.Visible = false;
            // 
            // textBox5
            // 
            this.textBox5.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.textBox5.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox5.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBox5.Location = new System.Drawing.Point(239, 347);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(164, 15);
            this.textBox5.TabIndex = 19;
            this.textBox5.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Bahnschrift", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label5.Location = new System.Drawing.Point(58, 343);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 19);
            this.label5.TabIndex = 18;
            this.label5.Text = "label5";
            this.label5.Visible = false;
            // 
            // textBox6
            // 
            this.textBox6.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.textBox6.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox6.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBox6.Location = new System.Drawing.Point(239, 396);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(164, 15);
            this.textBox6.TabIndex = 17;
            this.textBox6.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Bahnschrift", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label6.Location = new System.Drawing.Point(58, 392);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(54, 19);
            this.label6.TabIndex = 16;
            this.label6.Text = "label6";
            this.label6.Visible = false;
            // 
            // textBox7
            // 
            this.textBox7.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.textBox7.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox7.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBox7.Location = new System.Drawing.Point(239, 444);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(164, 15);
            this.textBox7.TabIndex = 15;
            this.textBox7.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Bahnschrift", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label7.Location = new System.Drawing.Point(58, 440);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(54, 19);
            this.label7.TabIndex = 14;
            this.label7.Text = "label7";
            this.label7.Visible = false;
            this.label7.Click += new System.EventHandler(this.label7_Click);
            // 
            // textBox4
            // 
            this.textBox4.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.textBox4.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox4.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBox4.Location = new System.Drawing.Point(239, 297);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(164, 15);
            this.textBox4.TabIndex = 13;
            this.textBox4.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Bahnschrift", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label4.Location = new System.Drawing.Point(58, 293);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 19);
            this.label4.TabIndex = 12;
            this.label4.Text = "label4";
            this.label4.Visible = false;
            // 
            // textBox3
            // 
            this.textBox3.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.textBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox3.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBox3.Location = new System.Drawing.Point(239, 245);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(164, 15);
            this.textBox3.TabIndex = 9;
            this.textBox3.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Bahnschrift", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(58, 241);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 19);
            this.label3.TabIndex = 8;
            this.label3.Text = "label3";
            this.label3.Visible = false;
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBox2.Location = new System.Drawing.Point(239, 197);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(164, 15);
            this.textBox2.TabIndex = 7;
            this.textBox2.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Bahnschrift", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(58, 193);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 19);
            this.label2.TabIndex = 6;
            this.label2.Text = "label2";
            this.label2.Visible = false;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBox1.Location = new System.Drawing.Point(239, 124);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(164, 15);
            this.textBox1.TabIndex = 5;
            this.textBox1.Visible = false;
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Font = new System.Drawing.Font("Bahnschrift", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.nameLabel.Location = new System.Drawing.Point(58, 120);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(56, 19);
            this.nameLabel.TabIndex = 1;
            this.nameLabel.Text = "Name:";
            this.nameLabel.Visible = false;
            // 
            // comboBox1
            // 
            this.comboBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBox1.Font = new System.Drawing.Font("Bahnschrift", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Fixed Income:",
            "-Bond",
            "-Callable Bond",
            "-Convertible Bond",
            "-Zero Coupon Bond",
            "-Perpetuity",
            "",
            "Options:",
            "-American Option",
            "-European Option",
            "-Bond Option",
            "",
            "Swaps:",
            "-Interest Rate Swap"});
            this.comboBox1.Location = new System.Drawing.Point(58, 49);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(345, 27);
            this.comboBox1.TabIndex = 0;
            this.comboBox1.Text = "Create new asset";
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // portfolioPage
            // 
            this.portfolioPage.BackColor = System.Drawing.Color.Gray;
            this.portfolioPage.Location = new System.Drawing.Point(4, 28);
            this.portfolioPage.Name = "portfolioPage";
            this.portfolioPage.Padding = new System.Windows.Forms.Padding(3);
            this.portfolioPage.Size = new System.Drawing.Size(802, 720);
            this.portfolioPage.TabIndex = 1;
            this.portfolioPage.Text = "Portfolios";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(884, 820);
            this.Controls.Add(this.tabControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Open Asset Pricer";
            this.tabControl.ResumeLayout(false);
            this.assetPage.ResumeLayout(false);
            this.assetPage.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage assetPage;
        private System.Windows.Forms.TabPage portfolioPage;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.TextBox textBox8;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox9;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBox10;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.ListBox resultsBox;
        private System.Windows.Forms.CheckBox putCheckBox;
        private System.Windows.Forms.Label putCheck;
        private System.Windows.Forms.Button priceAssetBtn;
        private System.Windows.Forms.Button saveAssetBtn;
    }
}

