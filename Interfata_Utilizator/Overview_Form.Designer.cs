namespace PROIECT_CSD
{
    partial class Overview_Form
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
            EntryList = new ListView();
            FileNameColumn = new ColumnHeader();
            IsEncryptedColumn = new ColumnHeader();
            KeyColumn = new ColumnHeader();
            AlgColumn = new ColumnHeader();
            DurationColumn = new ColumnHeader();
            PathColumn = new ColumnHeader();
            UserTypeLabel = new Label();
            UserNameLabel = new Label();
            AddFileButton = new Button();
            EncryptButton = new Button();
            ModifyButton = new Button();
            DeleteButton = new Button();
            SuspendLayout();
            // 
            // EntryList
            // 
            EntryList.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            EntryList.Columns.AddRange(new ColumnHeader[] { FileNameColumn, IsEncryptedColumn, KeyColumn, AlgColumn, DurationColumn, PathColumn });
            EntryList.FullRowSelect = true;
            EntryList.Location = new Point(12, 42);
            EntryList.Name = "EntryList";
            EntryList.Size = new Size(925, 370);
            EntryList.TabIndex = 0;
            EntryList.UseCompatibleStateImageBehavior = false;
            EntryList.View = View.Details;
            EntryList.SelectedIndexChanged += EntryList_SelectedIndexChanged;
            // 
            // FileNameColumn
            // 
            FileNameColumn.Text = "File Name";
            FileNameColumn.Width = 140;
            // 
            // IsEncryptedColumn
            // 
            IsEncryptedColumn.Text = "Encrypted";
            IsEncryptedColumn.Width = 70;
            // 
            // KeyColumn
            //  
            KeyColumn.Text = "Key";
            KeyColumn.Width = 130; 
            // 
            // AlgColumn
            // 
            AlgColumn.Text = "Algorithm";
            AlgColumn.Width = 100;
            // 
            // DurationColumn
            // 
            DurationColumn.Text = "Duration [ms]";
            DurationColumn.Width = 110;
            // 
            // PathColumn
            // 
            PathColumn.Text = "Full Path";
            PathColumn.Width = 300;
            // 
            // UserTypeLabel
            // 
            UserTypeLabel.AutoSize = true;
            UserTypeLabel.Location = new Point(12, 9);
            UserTypeLabel.Name = "UserTypeLabel";
            UserTypeLabel.Size = new Size(27, 15);
            UserTypeLabel.TabIndex = 1;
            UserTypeLabel.Text = "null";
            // 
            // UserNameLabel
            // 
            UserNameLabel.AutoSize = true;
            UserNameLabel.Location = new Point(12, 24);
            UserNameLabel.Name = "UserNameLabel";
            UserNameLabel.Size = new Size(27, 15);
            UserNameLabel.TabIndex = 2;
            UserNameLabel.Text = "null";
            // 
            // AddFileButton
            // 
            AddFileButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            AddFileButton.Location = new Point(12, 418);
            AddFileButton.Name = "AddFileButton";
            AddFileButton.Size = new Size(75, 23);
            AddFileButton.TabIndex = 3;
            AddFileButton.Text = "Add File";
            AddFileButton.UseVisualStyleBackColor = true;
            AddFileButton.Click += AddFileButton_Click;
            // 
            // EncryptButton
            // 
            EncryptButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            EncryptButton.Enabled = false;
            EncryptButton.Location = new Point(93, 418);
            EncryptButton.Name = "EncryptButton";
            EncryptButton.Size = new Size(75, 23);
            EncryptButton.TabIndex = 4;
            EncryptButton.Text = "Encrypt";
            EncryptButton.UseVisualStyleBackColor = true;
            EncryptButton.Click += EncryptButton_Click;
            // 
            // ModifyButton
            // 
            ModifyButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            ModifyButton.Enabled = false;
            ModifyButton.Location = new Point(174, 418);
            ModifyButton.Name = "ModifyButton";
            ModifyButton.Size = new Size(75, 23);
            ModifyButton.TabIndex = 5;
            ModifyButton.Text = "Modify";
            ModifyButton.UseVisualStyleBackColor = true;
            ModifyButton.Click += ModifyButton_Click;
            // 
            // DeleteButton
            // 
            DeleteButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            DeleteButton.Enabled = false;
            DeleteButton.Location = new Point(255, 418);
            DeleteButton.Name = "DeleteButton";
            DeleteButton.Size = new Size(75, 23);
            DeleteButton.TabIndex = 6;
            DeleteButton.Text = "Delete";
            DeleteButton.UseVisualStyleBackColor = true;
            DeleteButton.Click += DeleteButton_Click;
            // 
            // Overview_Form
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(949, 451);
            Controls.Add(DeleteButton);
            Controls.Add(ModifyButton);
            Controls.Add(EncryptButton);
            Controls.Add(AddFileButton);
            Controls.Add(UserNameLabel);
            Controls.Add(UserTypeLabel);
            Controls.Add(EntryList);
            Name = "Overview_Form";
            Text = "Form1";
            Load += Overview_Form_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListView EntryList;
        private Label UserTypeLabel;
        private Label UserNameLabel;
        private Button AddFileButton;
        private Button EncryptButton;
        private Button ModifyButton;
        private Button DeleteButton;
        private ColumnHeader FileNameColumn;
        private ColumnHeader IsEncryptedColumn;
        private ColumnHeader KeyColumn;
        private ColumnHeader AlgColumn;
        private ColumnHeader DurationColumn;
        private ColumnHeader PathColumn;
    }
}
