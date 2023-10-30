namespace StudentManagementSystem
{
    partial class Form1
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
            addSchoolButton = new Button();
            addClassButton = new Button();
            searchLogTextBox = new TextBox();
            schoolNameTextBox = new TextBox();
            schoolAddressTextBox = new TextBox();
            classNameTextBox = new TextBox();
            studentNameTextBox = new TextBox();
            studentAgeTextBox = new TextBox();
            studentGenderTextBox = new TextBox();
            classGradeTextBox = new TextBox();
            schoolTreeView = new TreeView();
            logDataGridView = new DataGridView();
            addStudentButton = new Button();
            deleteSchoolButton = new Button();
            deleteClassButton = new Button();
            deleteStudentButton = new Button();
            searchLogButton = new Button();
            ((System.ComponentModel.ISupportInitialize)logDataGridView).BeginInit();
            SuspendLayout();
            // 
            // addSchoolButton
            // 
            addSchoolButton.Location = new Point(445, 12);
            addSchoolButton.Name = "addSchoolButton";
            addSchoolButton.Size = new Size(94, 29);
            addSchoolButton.TabIndex = 0;
            addSchoolButton.Text = "button1";
            addSchoolButton.UseVisualStyleBackColor = true;
            addSchoolButton.Click += addSchoolButton_Click;
            // 
            // addClassButton
            // 
            addClassButton.Location = new Point(445, 47);
            addClassButton.Name = "addClassButton";
            addClassButton.Size = new Size(94, 29);
            addClassButton.TabIndex = 1;
            addClassButton.Text = "button2";
            addClassButton.UseVisualStyleBackColor = true;
            addClassButton.Click += addClassButton_Click;
            // 
            // searchLogTextBox
            // 
            searchLogTextBox.Location = new Point(147, 14);
            searchLogTextBox.Name = "searchLogTextBox";
            searchLogTextBox.Size = new Size(125, 27);
            searchLogTextBox.TabIndex = 2;
            // 
            // schoolNameTextBox
            // 
            schoolNameTextBox.Location = new Point(147, 56);
            schoolNameTextBox.Name = "schoolNameTextBox";
            schoolNameTextBox.Size = new Size(125, 27);
            schoolNameTextBox.TabIndex = 3;
            // 
            // schoolAddressTextBox
            // 
            schoolAddressTextBox.Location = new Point(147, 93);
            schoolAddressTextBox.Name = "schoolAddressTextBox";
            schoolAddressTextBox.Size = new Size(125, 27);
            schoolAddressTextBox.TabIndex = 4;
            // 
            // classNameTextBox
            // 
            classNameTextBox.Location = new Point(147, 135);
            classNameTextBox.Name = "classNameTextBox";
            classNameTextBox.Size = new Size(125, 27);
            classNameTextBox.TabIndex = 5;
            // 
            // studentNameTextBox
            // 
            studentNameTextBox.Location = new Point(147, 211);
            studentNameTextBox.Name = "studentNameTextBox";
            studentNameTextBox.Size = new Size(125, 27);
            studentNameTextBox.TabIndex = 6;
            // 
            // studentAgeTextBox
            // 
            studentAgeTextBox.Location = new Point(147, 254);
            studentAgeTextBox.Name = "studentAgeTextBox";
            studentAgeTextBox.Size = new Size(125, 27);
            studentAgeTextBox.TabIndex = 7;
            // 
            // studentGenderTextBox
            // 
            studentGenderTextBox.Location = new Point(147, 298);
            studentGenderTextBox.Name = "studentGenderTextBox";
            studentGenderTextBox.Size = new Size(125, 27);
            studentGenderTextBox.TabIndex = 8;
            // 
            // classGradeTextBox
            // 
            classGradeTextBox.Location = new Point(147, 175);
            classGradeTextBox.Name = "classGradeTextBox";
            classGradeTextBox.Size = new Size(125, 27);
            classGradeTextBox.TabIndex = 9;
            // 
            // schoolTreeView
            // 
            schoolTreeView.Location = new Point(317, 250);
            schoolTreeView.Name = "schoolTreeView";
            schoolTreeView.Size = new Size(222, 117);
            schoolTreeView.TabIndex = 10;
            // 
            // logDataGridView
            // 
            logDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            logDataGridView.Location = new Point(550, 250);
            logDataGridView.Name = "logDataGridView";
            logDataGridView.RowHeadersWidth = 51;
            logDataGridView.RowTemplate.Height = 29;
            logDataGridView.Size = new Size(213, 117);
            logDataGridView.TabIndex = 11;
            // 
            // addStudentButton
            // 
            addStudentButton.Location = new Point(445, 82);
            addStudentButton.Name = "addStudentButton";
            addStudentButton.Size = new Size(94, 29);
            addStudentButton.TabIndex = 12;
            addStudentButton.Text = "button3";
            addStudentButton.UseVisualStyleBackColor = true;
            addStudentButton.Click += addStudentButton_Click;
            // 
            // deleteSchoolButton
            // 
            deleteSchoolButton.Location = new Point(445, 117);
            deleteSchoolButton.Name = "deleteSchoolButton";
            deleteSchoolButton.Size = new Size(94, 29);
            deleteSchoolButton.TabIndex = 13;
            deleteSchoolButton.Text = "button4";
            deleteSchoolButton.UseVisualStyleBackColor = true;
            deleteSchoolButton.Click += deleteSchoolButton_Click;
            // 
            // deleteClassButton
            // 
            deleteClassButton.Location = new Point(445, 152);
            deleteClassButton.Name = "deleteClassButton";
            deleteClassButton.Size = new Size(94, 29);
            deleteClassButton.TabIndex = 14;
            deleteClassButton.Text = "button5";
            deleteClassButton.UseVisualStyleBackColor = true;
            deleteClassButton.Click += deleteClassButton_Click;
            // 
            // deleteStudentButton
            // 
            deleteStudentButton.Location = new Point(445, 187);
            deleteStudentButton.Name = "deleteStudentButton";
            deleteStudentButton.Size = new Size(94, 29);
            deleteStudentButton.TabIndex = 15;
            deleteStudentButton.Text = "button6";
            deleteStudentButton.UseVisualStyleBackColor = true;
            deleteStudentButton.Click += deleteStudentButton_Click;
            // 
            // searchLogButton
            // 
            searchLogButton.Location = new Point(656, 14);
            searchLogButton.Name = "searchLogButton";
            searchLogButton.Size = new Size(107, 31);
            searchLogButton.TabIndex = 16;
            searchLogButton.Text = "button1";
            searchLogButton.UseVisualStyleBackColor = true;
            searchLogButton.Click += searchLogButton_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(searchLogButton);
            Controls.Add(deleteStudentButton);
            Controls.Add(deleteClassButton);
            Controls.Add(deleteSchoolButton);
            Controls.Add(addStudentButton);
            Controls.Add(logDataGridView);
            Controls.Add(schoolTreeView);
            Controls.Add(classGradeTextBox);
            Controls.Add(studentGenderTextBox);
            Controls.Add(studentAgeTextBox);
            Controls.Add(studentNameTextBox);
            Controls.Add(classNameTextBox);
            Controls.Add(schoolAddressTextBox);
            Controls.Add(schoolNameTextBox);
            Controls.Add(searchLogTextBox);
            Controls.Add(addClassButton);
            Controls.Add(addSchoolButton);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)logDataGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button addSchoolButton;
        private Button addClassButton;
        private TextBox searchLogTextBox;
        private TextBox schoolNameTextBox;
        private TextBox schoolAddressTextBox;
        private TextBox classNameTextBox;
        private TextBox studentNameTextBox;
        private TextBox studentAgeTextBox;
        private TextBox studentGenderTextBox;
        private TextBox classGradeTextBox;
        private TreeView schoolTreeView;
        private DataGridView logDataGridView;
        private Button addStudentButton;
        private Button deleteSchoolButton;
        private Button deleteClassButton;
        private Button deleteStudentButton;
        private Button searchLogButton;
    }
}