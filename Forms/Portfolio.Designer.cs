
namespace OAP_CS
{
    partial class Portfolio
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Portfolio));
            this.label1 = new System.Windows.Forms.Label();
            this.mainLabel = new System.Windows.Forms.Label();
            this.textBox = new System.Windows.Forms.TextBox();
            this.actionBtn = new System.Windows.Forms.Button();
            this.comboBox = new System.Windows.Forms.ComboBox();
            this.listBox = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Gray;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 110.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.Color.Gray;
            this.label1.Location = new System.Drawing.Point(31, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(466, 195);
            this.label1.TabIndex = 2;
            this.label1.Text = "............";
            // 
            // mainLabel
            // 
            this.mainLabel.AutoSize = true;
            this.mainLabel.BackColor = System.Drawing.Color.Gray;
            this.mainLabel.Font = new System.Drawing.Font("Bahnschrift", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.mainLabel.Location = new System.Drawing.Point(67, 91);
            this.mainLabel.Name = "mainLabel";
            this.mainLabel.Size = new System.Drawing.Size(122, 19);
            this.mainLabel.TabIndex = 3;
            this.mainLabel.Text = "Portfolio Name:";
            // 
            // textBox
            // 
            this.textBox.Location = new System.Drawing.Point(67, 130);
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(293, 23);
            this.textBox.TabIndex = 4;
            // 
            // actionBtn
            // 
            this.actionBtn.Font = new System.Drawing.Font("Bahnschrift Condensed", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.actionBtn.Location = new System.Drawing.Point(390, 126);
            this.actionBtn.Name = "actionBtn";
            this.actionBtn.Size = new System.Drawing.Size(77, 34);
            this.actionBtn.TabIndex = 5;
            this.actionBtn.Text = "Create";
            this.actionBtn.UseVisualStyleBackColor = true;
            this.actionBtn.Click += new System.EventHandler(this.actionBtn_Click);
            // 
            // comboBox
            // 
            this.comboBox.FormattingEnabled = true;
            this.comboBox.Location = new System.Drawing.Point(67, 130);
            this.comboBox.Name = "comboBox";
            this.comboBox.Size = new System.Drawing.Size(293, 23);
            this.comboBox.TabIndex = 6;
            // 
            // listBox
            // 
            this.listBox.Font = new System.Drawing.Font("Bahnschrift Condensed", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.listBox.FormattingEnabled = true;
            this.listBox.ItemHeight = 19;
            this.listBox.Location = new System.Drawing.Point(31, 270);
            this.listBox.Name = "listBox";
            this.listBox.Size = new System.Drawing.Size(466, 631);
            this.listBox.TabIndex = 7;
            // 
            // Portfolio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(534, 941);
            this.Controls.Add(this.listBox);
            this.Controls.Add(this.comboBox);
            this.Controls.Add(this.actionBtn);
            this.Controls.Add(this.textBox);
            this.Controls.Add(this.mainLabel);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(550, 980);
            this.MinimumSize = new System.Drawing.Size(550, 310);
            this.Name = "Portfolio";
            this.Text = "Portfolios";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label mainLabel;
        private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.Button actionBtn;
        private System.Windows.Forms.ComboBox comboBox;
        private System.Windows.Forms.ListBox listBox;
    }
}

