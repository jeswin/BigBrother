namespace ExampleApp
{
    partial class MainForm
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label8 = new System.Windows.Forms.Label();
            this.peopleTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.loadPeopleLocationTextBox = new System.Windows.Forms.TextBox();
            this.loadPeopleButton = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.personTextBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.loadPersonLocationTextBox = new System.Windows.Forms.TextBox();
            this.loadPersonButton = new System.Windows.Forms.Button();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.saveButton = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.book2TextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.book1TextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.locationTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ageTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(17, 14);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(772, 303);
            this.tabControl1.TabIndex = 11;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label8);
            this.tabPage2.Controls.Add(this.peopleTextBox);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.loadPeopleLocationTextBox);
            this.tabPage2.Controls.Add(this.loadPeopleButton);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(764, 277);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Get People";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.Gray;
            this.label8.Location = new System.Drawing.Point(255, 15);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(81, 13);
            this.label8.TabIndex = 4;
            this.label8.Text = "ignored anyway";
            // 
            // peopleTextBox
            // 
            this.peopleTextBox.Location = new System.Drawing.Point(16, 81);
            this.peopleTextBox.Multiline = true;
            this.peopleTextBox.Name = "peopleTextBox";
            this.peopleTextBox.Size = new System.Drawing.Size(731, 181);
            this.peopleTextBox.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 15);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Location";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // loadPeopleLocationTextBox
            // 
            this.loadPeopleLocationTextBox.Location = new System.Drawing.Point(67, 12);
            this.loadPeopleLocationTextBox.Name = "loadPeopleLocationTextBox";
            this.loadPeopleLocationTextBox.Size = new System.Drawing.Size(182, 20);
            this.loadPeopleLocationTextBox.TabIndex = 1;
            this.loadPeopleLocationTextBox.Text = "Timbuktu";
            this.loadPeopleLocationTextBox.TextChanged += new System.EventHandler(this.loadPeopleLocationTextBox_TextChanged);
            // 
            // loadPeopleButton
            // 
            this.loadPeopleButton.Location = new System.Drawing.Point(16, 43);
            this.loadPeopleButton.Name = "loadPeopleButton";
            this.loadPeopleButton.Size = new System.Drawing.Size(135, 23);
            this.loadPeopleButton.TabIndex = 0;
            this.loadPeopleButton.Text = "Load some people";
            this.loadPeopleButton.UseVisualStyleBackColor = true;
            this.loadPeopleButton.Click += new System.EventHandler(this.loadPeopleButton_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.personTextBox);
            this.tabPage3.Controls.Add(this.label7);
            this.tabPage3.Controls.Add(this.label9);
            this.tabPage3.Controls.Add(this.loadPersonLocationTextBox);
            this.tabPage3.Controls.Add(this.loadPersonButton);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(764, 277);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Get Person";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // personTextBox
            // 
            this.personTextBox.Location = new System.Drawing.Point(20, 81);
            this.personTextBox.Multiline = true;
            this.personTextBox.Name = "personTextBox";
            this.personTextBox.Size = new System.Drawing.Size(731, 181);
            this.personTextBox.TabIndex = 9;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.Gray;
            this.label7.Location = new System.Drawing.Point(259, 16);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(81, 13);
            this.label7.TabIndex = 8;
            this.label7.Text = "ignored anyway";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(17, 16);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(48, 13);
            this.label9.TabIndex = 7;
            this.label9.Text = "Location";
            // 
            // loadPersonLocationTextBox
            // 
            this.loadPersonLocationTextBox.Location = new System.Drawing.Point(71, 13);
            this.loadPersonLocationTextBox.Name = "loadPersonLocationTextBox";
            this.loadPersonLocationTextBox.Size = new System.Drawing.Size(182, 20);
            this.loadPersonLocationTextBox.TabIndex = 6;
            this.loadPersonLocationTextBox.Text = "Timbuktu";
            // 
            // loadPersonButton
            // 
            this.loadPersonButton.Location = new System.Drawing.Point(20, 46);
            this.loadPersonButton.Name = "loadPersonButton";
            this.loadPersonButton.Size = new System.Drawing.Size(135, 23);
            this.loadPersonButton.TabIndex = 5;
            this.loadPersonButton.Text = "Load a person";
            this.loadPersonButton.UseVisualStyleBackColor = true;
            this.loadPersonButton.Click += new System.EventHandler(this.loadPersonButton_Click);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.saveButton);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.book2TextBox);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.book1TextBox);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.locationTextBox);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.ageTextBox);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.nameTextBox);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(764, 277);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Save Person";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(67, 158);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(113, 23);
            this.saveButton.TabIndex = 21;
            this.saveButton.Text = "Save Person";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 119);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 20;
            this.label4.Text = "Book2";
            // 
            // book2TextBox
            // 
            this.book2TextBox.Location = new System.Drawing.Point(67, 119);
            this.book2TextBox.Name = "book2TextBox";
            this.book2TextBox.Size = new System.Drawing.Size(188, 20);
            this.book2TextBox.TabIndex = 19;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 93);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 13);
            this.label5.TabIndex = 18;
            this.label5.Text = "Book1";
            // 
            // book1TextBox
            // 
            this.book1TextBox.Location = new System.Drawing.Point(67, 93);
            this.book1TextBox.Name = "book1TextBox";
            this.book1TextBox.Size = new System.Drawing.Size(188, 20);
            this.book1TextBox.TabIndex = 17;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Location";
            // 
            // locationTextBox
            // 
            this.locationTextBox.Location = new System.Drawing.Point(67, 67);
            this.locationTextBox.Name = "locationTextBox";
            this.locationTextBox.Size = new System.Drawing.Size(188, 20);
            this.locationTextBox.TabIndex = 15;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Age";
            // 
            // ageTextBox
            // 
            this.ageTextBox.Location = new System.Drawing.Point(67, 41);
            this.ageTextBox.Name = "ageTextBox";
            this.ageTextBox.Size = new System.Drawing.Size(75, 20);
            this.ageTextBox.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Name";
            // 
            // nameTextBox
            // 
            this.nameTextBox.Location = new System.Drawing.Point(67, 15);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(188, 20);
            this.nameTextBox.TabIndex = 11;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(801, 329);
            this.Controls.Add(this.tabControl1);
            this.Name = "MainForm";
            this.Text = "Save Person";
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox loadPeopleLocationTextBox;
        private System.Windows.Forms.Button loadPeopleButton;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox book2TextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox book1TextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox locationTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox ageTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.TextBox peopleTextBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox loadPersonLocationTextBox;
        private System.Windows.Forms.Button loadPersonButton;
        private System.Windows.Forms.TextBox personTextBox;

    }
}

