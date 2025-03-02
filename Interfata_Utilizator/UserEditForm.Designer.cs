namespace PROIECT_CSD.Interfata_Utilizator
{
    partial class UserEditForm
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
            PassLabel = new Label();
            PasswordTextBox = new TextBox();
            btnClose = new Button();
            btnAdd = new Button();
            label1 = new Label();
            UsernameTextBox = new TextBox();
            IsAdminCheckBox = new CheckBox();
            openFileDialog1 = new OpenFileDialog();
            SuspendLayout();
            // 
            // PassLabel
            // 
            PassLabel.AutoSize = true;
            PassLabel.Location = new Point(31, 44);
            PassLabel.Name = "PassLabel";
            PassLabel.Size = new Size(60, 15);
            PassLabel.TabIndex = 13;
            PassLabel.Text = "Password:";
            // 
            // PasswordTextBox
            // 
            PasswordTextBox.Location = new Point(97, 41);
            PasswordTextBox.Name = "PasswordTextBox";
            PasswordTextBox.PasswordChar = '*';
            PasswordTextBox.Size = new Size(100, 23);
            PasswordTextBox.TabIndex = 12;
            // 
            // btnClose
            // 
            btnClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnClose.Location = new Point(157, 99);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(75, 23);
            btnClose.TabIndex = 11;
            btnClose.Text = "Close";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += BtnClose_Click;
            // 
            // btnAdd
            // 
            btnAdd.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnAdd.Location = new Point(76, 99);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(75, 23);
            btnAdd.TabIndex = 10;
            btnAdd.Text = "Apply";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += BtnAdd_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(23, 15);
            label1.Name = "label1";
            label1.Size = new Size(68, 15);
            label1.TabIndex = 9;
            label1.Text = "User Name:";
            // 
            // UsernameTextBox
            // 
            UsernameTextBox.Location = new Point(97, 12);
            UsernameTextBox.Name = "UsernameTextBox";
            UsernameTextBox.Size = new Size(100, 23);
            UsernameTextBox.TabIndex = 8;
            // 
            // IsAdminCheckBox
            // 
            IsAdminCheckBox.AutoSize = true;
            IsAdminCheckBox.Location = new Point(97, 68);
            IsAdminCheckBox.Name = "IsAdminCheckBox";
            IsAdminCheckBox.Size = new Size(73, 19);
            IsAdminCheckBox.TabIndex = 7;
            IsAdminCheckBox.Text = "Is Admin";
            IsAdminCheckBox.UseVisualStyleBackColor = true;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // UserEditForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(244, 134);
            Controls.Add(PassLabel);
            Controls.Add(PasswordTextBox);
            Controls.Add(btnClose);
            Controls.Add(btnAdd);
            Controls.Add(label1);
            Controls.Add(UsernameTextBox);
            Controls.Add(IsAdminCheckBox);
            Name = "UserEditForm";
            Text = "UserEditForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label PassLabel;
        private TextBox PasswordTextBox;
        private Button btnClose;
        private Button btnAdd;
        private Label label1;
        private TextBox UsernameTextBox;
        private CheckBox IsAdminCheckBox;
        private OpenFileDialog openFileDialog1;
    }
}