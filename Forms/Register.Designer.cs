
namespace OAP_CS
{
    partial class Register
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Register));
            this.label1 = new System.Windows.Forms.Label();
            this.passEntryBox = new System.Windows.Forms.TextBox();
            this.userEntryBox = new System.Windows.Forms.TextBox();
            this.passwordLabel = new System.Windows.Forms.Label();
            this.loginLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.passConfBox = new System.Windows.Forms.TextBox();
            this.registerBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Gray;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 99.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.Color.Gray;
            this.label1.Location = new System.Drawing.Point(25, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(480, 175);
            this.label1.TabIndex = 1;
            this.label1.Text = "..............";
            // 
            // passEntryBox
            // 
            this.passEntryBox.Location = new System.Drawing.Point(247, 118);
            this.passEntryBox.MaxLength = 20;
            this.passEntryBox.Name = "passEntryBox";
            this.passEntryBox.PasswordChar = '*';
            this.passEntryBox.Size = new System.Drawing.Size(221, 23);
            this.passEntryBox.TabIndex = 8;
            // 
            // userEntryBox
            // 
            this.userEntryBox.Location = new System.Drawing.Point(247, 76);
            this.userEntryBox.Name = "userEntryBox";
            this.userEntryBox.Size = new System.Drawing.Size(221, 23);
            this.userEntryBox.TabIndex = 7;
            // 
            // passwordLabel
            // 
            this.passwordLabel.AutoSize = true;
            this.passwordLabel.BackColor = System.Drawing.Color.Gray;
            this.passwordLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.passwordLabel.Location = new System.Drawing.Point(63, 120);
            this.passwordLabel.Name = "passwordLabel";
            this.passwordLabel.Size = new System.Drawing.Size(79, 21);
            this.passwordLabel.TabIndex = 6;
            this.passwordLabel.Text = "Password:";
            // 
            // loginLabel
            // 
            this.loginLabel.AutoSize = true;
            this.loginLabel.BackColor = System.Drawing.Color.Gray;
            this.loginLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.loginLabel.Location = new System.Drawing.Point(63, 78);
            this.loginLabel.Name = "loginLabel";
            this.loginLabel.Size = new System.Drawing.Size(84, 21);
            this.loginLabel.TabIndex = 5;
            this.loginLabel.Text = "Username:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Gray;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(63, 162);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(140, 21);
            this.label2.TabIndex = 9;
            this.label2.Text = "Confirm Password:";
            // 
            // passConfBox
            // 
            this.passConfBox.Location = new System.Drawing.Point(247, 162);
            this.passConfBox.MaxLength = 20;
            this.passConfBox.Name = "passConfBox";
            this.passConfBox.PasswordChar = '*';
            this.passConfBox.Size = new System.Drawing.Size(221, 23);
            this.passConfBox.TabIndex = 10;
            // 
            // registerBtn
            // 
            this.registerBtn.Location = new System.Drawing.Point(215, 234);
            this.registerBtn.Name = "registerBtn";
            this.registerBtn.Size = new System.Drawing.Size(101, 23);
            this.registerBtn.TabIndex = 11;
            this.registerBtn.Text = "Register / Login";
            this.registerBtn.UseVisualStyleBackColor = true;
            this.registerBtn.Click += new System.EventHandler(this.registerBtn_Click);
            // 
            // Register
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(534, 271);
            this.Controls.Add(this.registerBtn);
            this.Controls.Add(this.passConfBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.passEntryBox);
            this.Controls.Add(this.userEntryBox);
            this.Controls.Add(this.passwordLabel);
            this.Controls.Add(this.loginLabel);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(550, 310);
            this.MinimumSize = new System.Drawing.Size(550, 310);
            this.Name = "Register";
            this.Text = "Registration Form";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox passEntryBox;
        private System.Windows.Forms.TextBox userEntryBox;
        private System.Windows.Forms.Label passwordLabel;
        private System.Windows.Forms.Label loginLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox passConfBox;
        private System.Windows.Forms.Button registerBtn;
    }
}

