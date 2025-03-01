namespace PROIECT_CSD.Interfata_Utilizator
{
    partial class EntryEditForm
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
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            tbFilename = new TextBox();
            tbPath = new TextBox();
            tbAlg = new TextBox();
            tbDuration = new TextBox();
            tbEncKey = new TextBox();
            tbEncrypted = new TextBox();
            btnApply = new Button();
            btnClose = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(64, 20);
            label1.Name = "label1";
            label1.Size = new Size(66, 15);
            label1.TabIndex = 0;
            label1.Text = "File Name: ";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(64, 44);
            label2.Name = "label2";
            label2.Size = new Size(63, 15);
            label2.TabIndex = 1;
            label2.Text = "Encrypted:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(38, 73);
            label3.Name = "label3";
            label3.Size = new Size(92, 15);
            label3.TabIndex = 2;
            label3.Text = "Encryption Key::";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(66, 128);
            label4.Name = "label4";
            label4.Size = new Size(64, 15);
            label4.TabIndex = 3;
            label4.Text = "Algorithm:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(71, 102);
            label5.Name = "label5";
            label5.Size = new Size(56, 15);
            label5.TabIndex = 4;
            label5.Text = "Duration:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(18, 160);
            label6.Name = "label6";
            label6.Size = new Size(56, 15);
            label6.TabIndex = 5;
            label6.Text = "Full Path:";
            // 
            // tbFilename
            // 
            tbFilename.Location = new Point(142, 12);
            tbFilename.Name = "tbFilename";
            tbFilename.Size = new Size(171, 23);
            tbFilename.TabIndex = 6;
            // 
            // tbPath
            // 
            tbPath.Location = new Point(80, 157);
            tbPath.Name = "tbPath";
            tbPath.Size = new Size(233, 23);
            tbPath.TabIndex = 7;
            // 
            // tbAlg
            // 
            tbAlg.Location = new Point(142, 128);
            tbAlg.Name = "tbAlg";
            tbAlg.Size = new Size(171, 23);
            tbAlg.TabIndex = 8;
            // 
            // tbDuration
            // 
            tbDuration.Location = new Point(142, 99);
            tbDuration.Name = "tbDuration";
            tbDuration.Size = new Size(171, 23);
            tbDuration.TabIndex = 9;
            // 
            // tbEncKey
            // 
            tbEncKey.Location = new Point(142, 70);
            tbEncKey.Name = "tbEncKey";
            tbEncKey.Size = new Size(171, 23);
            tbEncKey.TabIndex = 10;
            // 
            // tbEncrypted
            // 
            tbEncrypted.Location = new Point(142, 41);
            tbEncrypted.Name = "tbEncrypted";
            tbEncrypted.Size = new Size(171, 23);
            tbEncrypted.TabIndex = 11;
            // 
            // btnApply
            // 
            btnApply.Location = new Point(178, 198);
            btnApply.Name = "btnApply";
            btnApply.Size = new Size(75, 23);
            btnApply.TabIndex = 12;
            btnApply.Text = "Apply";
            btnApply.UseVisualStyleBackColor = true;
            btnApply.Click += BtnApply_Click;
            // 
            // btnClose
            // 
            btnClose.Location = new Point(259, 198);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(75, 23);
            btnClose.TabIndex = 13;
            btnClose.Text = "Close";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += BtnClose_Click;
            // 
            // EntryEditForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(346, 233);
            Controls.Add(btnClose);
            Controls.Add(btnApply);
            Controls.Add(tbEncrypted);
            Controls.Add(tbEncKey);
            Controls.Add(tbDuration);
            Controls.Add(tbAlg);
            Controls.Add(tbPath);
            Controls.Add(tbFilename);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "EntryEditForm";
            Text = "Edit File Metadata";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private TextBox tbFilename;
        private TextBox tbPath;
        private TextBox tbAlg;
        private TextBox tbDuration;
        private TextBox tbEncKey;
        private TextBox tbEncrypted;
        private Button btnApply;
        private Button btnClose;
    }
}