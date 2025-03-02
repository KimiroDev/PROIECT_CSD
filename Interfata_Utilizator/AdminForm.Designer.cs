namespace PROIECT_CSD.Interfata_Utilizator
{
    partial class AdminForm
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
            UserList = new ListView();
            ID = new ColumnHeader();
            Username = new ColumnHeader();
            Passhash = new ColumnHeader();
            btnRemoveUser = new Button();
            btnEditUser = new Button();
            btnAddUser = new Button();
            tableLayoutPanel1 = new TableLayoutPanel();
            UserTypeLabel = new Label();
            UserNameLabel = new Label();
            btnLogout = new Button();
            isadmin = new ColumnHeader();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // UserList
            // 
            UserList.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            UserList.Columns.AddRange(new ColumnHeader[] { ID, isadmin, Username, Passhash });
            UserList.FullRowSelect = true;
            UserList.Location = new Point(12, 42);
            UserList.Name = "UserList";
            UserList.Size = new Size(786, 329);
            UserList.TabIndex = 0;
            UserList.UseCompatibleStateImageBehavior = false;
            UserList.View = View.Details;
            UserList.SelectedIndexChanged += UserList_SelectedIndexChanged;
            // 
            // ID
            // 
            ID.Text = "ID";
            ID.Width = 100;
            // 
            // Username
            // 
            Username.Text = "Username";
            Username.Width = 200;
            // 
            // Passhash
            // 
            Passhash.Text = "Password Hash";
            Passhash.Width = 482;
            // 
            // btnRemoveUser
            // 
            btnRemoveUser.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnRemoveUser.Location = new Point(724, 377);
            btnRemoveUser.Name = "btnRemoveUser";
            btnRemoveUser.Size = new Size(75, 23);
            btnRemoveUser.TabIndex = 1;
            btnRemoveUser.Text = "Remove";
            btnRemoveUser.UseVisualStyleBackColor = true;
            // 
            // btnEditUser
            // 
            btnEditUser.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnEditUser.Location = new Point(643, 377);
            btnEditUser.Name = "btnEditUser";
            btnEditUser.Size = new Size(75, 23);
            btnEditUser.TabIndex = 2;
            btnEditUser.Text = "Edit";
            btnEditUser.UseVisualStyleBackColor = true;
            btnEditUser.Click += BtnEditUser_Click;
            // 
            // btnAddUser
            // 
            btnAddUser.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnAddUser.Location = new Point(562, 377);
            btnAddUser.Name = "btnAddUser";
            btnAddUser.Size = new Size(75, 23);
            btnAddUser.TabIndex = 3;
            btnAddUser.Text = "Add New User";
            btnAddUser.UseVisualStyleBackColor = true;
            btnAddUser.Click += BtnAddUser_Click;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(UserTypeLabel, 0, 0);
            tableLayoutPanel1.Controls.Add(UserNameLabel, 0, 1);
            tableLayoutPanel1.Location = new Point(555, 2);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RightToLeft = RightToLeft.No;
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new Size(163, 40);
            tableLayoutPanel1.TabIndex = 9;
            // 
            // UserTypeLabel
            // 
            UserTypeLabel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            UserTypeLabel.AutoSize = true;
            UserTypeLabel.Location = new Point(133, 0);
            UserTypeLabel.Name = "UserTypeLabel";
            UserTypeLabel.Size = new Size(27, 15);
            UserTypeLabel.TabIndex = 1;
            UserTypeLabel.Text = "null";
            UserTypeLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // UserNameLabel
            // 
            UserNameLabel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            UserNameLabel.AutoSize = true;
            UserNameLabel.Location = new Point(133, 20);
            UserNameLabel.Name = "UserNameLabel";
            UserNameLabel.Size = new Size(27, 15);
            UserNameLabel.TabIndex = 2;
            UserNameLabel.Text = "null";
            UserNameLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // btnLogout
            // 
            btnLogout.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnLogout.Location = new Point(724, 8);
            btnLogout.Name = "btnLogout";
            btnLogout.Size = new Size(75, 23);
            btnLogout.TabIndex = 10;
            btnLogout.Text = "Logout";
            btnLogout.UseVisualStyleBackColor = true;
            btnLogout.Click += BtnLogout_Click;
            // 
            // isadmin
            // 
            isadmin.Text = "Is Admin";
            // 
            // AdminForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(810, 409);
            Controls.Add(btnLogout);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(btnAddUser);
            Controls.Add(btnEditUser);
            Controls.Add(btnRemoveUser);
            Controls.Add(UserList);
            Name = "AdminForm";
            Text = "AdminForm";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private ListView UserList;
        private ColumnHeader ID;
        private ColumnHeader Username;
        private ColumnHeader Passhash;
        private Button btnRemoveUser;
        private Button btnEditUser;
        private Button btnAddUser;
        private TableLayoutPanel tableLayoutPanel1;
        private Label UserTypeLabel;
        private Label UserNameLabel;
        private Button btnLogout;
        private ColumnHeader isadmin;
    }
}