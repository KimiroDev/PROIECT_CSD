using Microsoft.Data.Sqlite;

namespace PROIECT_CSD.Interfata_Utilizator
{
    partial class EncryptForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private void LoadAlgorithmsIntoComboBox(ComboBox comboBox)
        {
            string dbPath = Path.Combine(Directory.GetCurrentDirectory(), "..\\..\\..\\hello.db");
            string connectionString = $"Data Source={dbPath};";

            List<string> algorithms = new List<string>();

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT name FROM algos WHERE name IS NOT NULL AND name != '-'";

                using (var command = new SqliteCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        algorithms.Add(reader.GetString(0));
                    }
                }
            }

            // Clear previous items and add new ones
            comboBox.Items.Clear();
            comboBox.Items.AddRange(algorithms.ToArray());
        }

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
            button1 = new Button();
            SuspendLayout();
            // 
            // comboBox1
            // 
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "AES-128", "RSA" });
            comboBox1.Location = new Point(82, 12);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(152, 23);
            comboBox1.TabIndex = 0;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(82, 40);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(152, 23);
            textBox1.TabIndex = 1;
            // 
            // AlgLabel
            // 
            AlgLabel.AutoSize = true;
            AlgLabel.Location = new Point(12, 15);
            AlgLabel.Name = "AlgLabel";
            AlgLabel.Size = new Size(64, 15);
            AlgLabel.TabIndex = 2;
            AlgLabel.Text = "Algorithm:";
            // 
            // KeyLabel
            // 
            KeyLabel.AutoSize = true;
            KeyLabel.Location = new Point(47, 44);
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
            // button1
            // 
            button1.Enabled = false;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Location = new Point(240, 40);
            button1.Name = "button1";
            button1.Size = new Size(24, 23);
            button1.TabIndex = 5;
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // EncryptForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(318, 111);
            Controls.Add(button1);
            Controls.Add(EncryptButton);
            Controls.Add(KeyLabel);
            Controls.Add(AlgLabel);
            Controls.Add(textBox1);
            Controls.Add(comboBox1);
            Name = "EncryptForm";
            Text = "Encrypt File";
            Load += EncryptForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox comboBox1;
        private TextBox textBox1;
        private Label AlgLabel;
        private Label KeyLabel;
        private Button EncryptButton;
        private Button button1;
    }
}