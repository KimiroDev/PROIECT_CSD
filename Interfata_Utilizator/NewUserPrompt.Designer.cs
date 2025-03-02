namespace PROIECT_CSD.Interfata_Utilizator
{
    partial class NewUserPrompt
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            IsAdminCheckBox = new CheckBox();
            UsernameTextBox = new TextBox();
            label1 = new Label();
            btnAdd = new Button();
            btnClose = new Button();
            PasswordTextBox = new TextBox();
            PassLabel = new Label();
            SuspendLayout();
            // 
            // IsAdminCheckBox
            // 
            IsAdminCheckBox.AutoSize = true;
            IsAdminCheckBox.Location = new Point(117, 92);
            IsAdminCheckBox.Name = "IsAdminCheckBox";
            IsAdminCheckBox.Size = new Size(73, 19);
            IsAdminCheckBox.TabIndex = 0;
            IsAdminCheckBox.Text = "Is Admin";
            IsAdminCheckBox.UseVisualStyleBackColor = true;
            // 
            // UsernameTextBox
            // 
            UsernameTextBox.Location = new Point(117, 36);
            UsernameTextBox.Name = "UsernameTextBox";
            UsernameTextBox.Size = new Size(100, 23);
            UsernameTextBox.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(43, 39);
            label1.Name = "label1";
            label1.Size = new Size(68, 15);
            label1.TabIndex = 2;
            label1.Text = "User Name:";
            // 
            // btnAdd
            // 
            btnAdd.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnAdd.Location = new Point(109, 133);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(75, 23);
            btnAdd.TabIndex = 3;
            btnAdd.Text = "Add";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += BtnAdd_Click;
            // 
            // btnClose
            // 
            btnClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnClose.Location = new Point(190, 133);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(75, 23);
            btnClose.TabIndex = 4;
            btnClose.Text = "Close";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += BtnClose_Click;
            // 
            // PasswordTextBox
            // 
            PasswordTextBox.Location = new Point(117, 65);
            PasswordTextBox.Name = "PasswordTextBox";
            PasswordTextBox.PasswordChar = '*';
            PasswordTextBox.Size = new Size(100, 23);
            PasswordTextBox.TabIndex = 5;
            // 
            // PassLabel
            // 
            PassLabel.AutoSize = true;
            PassLabel.Location = new Point(51, 68);
            PassLabel.Name = "PassLabel";
            PassLabel.Size = new Size(60, 15);
            PassLabel.TabIndex = 6;
            PassLabel.Text = "Password:";
            // 
            // NewUserPrompt
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(277, 164);
            Controls.Add(PassLabel);
            Controls.Add(PasswordTextBox);
            Controls.Add(btnClose);
            Controls.Add(btnAdd);
            Controls.Add(label1);
            Controls.Add(UsernameTextBox);
            Controls.Add(IsAdminCheckBox);
            Name = "NewUserPrompt";
            Text = "Add new user";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private CheckBox IsAdminCheckBox;
        private TextBox UsernameTextBox;
        private Label label1;
        private Button btnAdd;
        private Button btnClose;
        private TextBox PasswordTextBox;
        private Label PassLabel;
    }
}