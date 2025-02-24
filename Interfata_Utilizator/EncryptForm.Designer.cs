namespace PROIECT_CSD.Interfata_Utilizator
{
    partial class EncryptForm
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
            comboBox1 = new ComboBox();
            textBox1 = new TextBox();
            AlgLabel = new Label();
            KeyLabel = new Label();
            EncryptButton = new Button();
            SuspendLayout();
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "SHA256", "RA", "Caesar" });
            comboBox1.Location = new Point(121, 12);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(121, 23);
            comboBox1.TabIndex = 0;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(121, 41);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(100, 23);
            textBox1.TabIndex = 1;
            // 
            // AlgLabel
            // 
            AlgLabel.AutoSize = true;
            AlgLabel.Location = new Point(51, 15);
            AlgLabel.Name = "AlgLabel";
            AlgLabel.Size = new Size(64, 15);
            AlgLabel.TabIndex = 2;
            AlgLabel.Text = "Algorithm:";
            // 
            // KeyLabel
            // 
            KeyLabel.AutoSize = true;
            KeyLabel.Location = new Point(86, 44);
            KeyLabel.Name = "KeyLabel";
            KeyLabel.Size = new Size(29, 15);
            KeyLabel.TabIndex = 3;
            KeyLabel.Text = "Key:";
            // 
            // EncryptButton
            // 
            EncryptButton.Location = new Point(121, 70);
            EncryptButton.Name = "EncryptButton";
            EncryptButton.Size = new Size(75, 23);
            EncryptButton.TabIndex = 4;
            EncryptButton.Text = "Encrypt";
            EncryptButton.UseVisualStyleBackColor = true;
            EncryptButton.Click += EncryptButton_Click;
            // 
            // EncryptForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(318, 111);
            Controls.Add(EncryptButton);
            Controls.Add(KeyLabel);
            Controls.Add(AlgLabel);
            Controls.Add(textBox1);
            Controls.Add(comboBox1);
            Name = "EncryptForm";
            Text = "Encrypt File";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox comboBox1;
        private TextBox textBox1;
        private Label AlgLabel;
        private Label KeyLabel;
        private Button EncryptButton;
    }
}