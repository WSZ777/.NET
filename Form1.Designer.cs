namespace 第五次作业
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
            crawledUrlsListBox = new ListBox();
            keywordTextBox = new TextBox();
            searchButton = new Button();
            label1 = new Label();
            SuspendLayout();
            // 
            // crawledUrlsListBox
            // 
            crawledUrlsListBox.FormattingEnabled = true;
            crawledUrlsListBox.ItemHeight = 20;
            crawledUrlsListBox.Location = new Point(105, 138);
            crawledUrlsListBox.Name = "crawledUrlsListBox";
            crawledUrlsListBox.Size = new Size(724, 344);
            crawledUrlsListBox.TabIndex = 0;
            // 
            // keywordTextBox
            // 
            keywordTextBox.Location = new Point(234, 62);
            keywordTextBox.Name = "keywordTextBox";
            keywordTextBox.Size = new Size(268, 27);
            keywordTextBox.TabIndex = 1;
            // 
            // searchButton
            // 
            searchButton.Location = new Point(674, 60);
            searchButton.Name = "searchButton";
            searchButton.Size = new Size(155, 29);
            searchButton.TabIndex = 2;
            searchButton.Text = "搜索";
            searchButton.UseVisualStyleBackColor = true;
            searchButton.Click += searchButton_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(105, 69);
            label1.Name = "label1";
            label1.Size = new Size(114, 20);
            label1.TabIndex = 3;
            label1.Text = "输入搜索内容：";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(961, 494);
            Controls.Add(label1);
            Controls.Add(searchButton);
            Controls.Add(keywordTextBox);
            Controls.Add(crawledUrlsListBox);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox crawledUrlsListBox;
        private TextBox keywordTextBox;
        private Button searchButton;
        private Label label1;
    }
}