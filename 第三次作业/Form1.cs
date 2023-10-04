using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace 第三次作业
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "C# Source File (*.cs)|*.cs";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = openFileDialog.FileName;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string filePath = textBox1.Text;

            if (string.IsNullOrEmpty(filePath))
            {
                MessageBox.Show("请选择源文件");
                return;
            }

            if (!File.Exists(filePath))
            {
                MessageBox.Show("选择的源文件不存在");
                return;
            }
            string[] lines = File.ReadAllLines(filePath);
            int originalLineCount = lines.Length;

            // 删除空行和注释
            lines = lines.Where(line => !string.IsNullOrWhiteSpace(line) && !line.TrimStart().StartsWith("//")).ToArray();

            // 统计原始行数和单词数
            int formattedLineCount = lines.Length;
            int wordCount = lines.Sum(line => Regex.Matches(line, @"\b\w+\b").Count);

            textBox2.Text = originalLineCount.ToString();
            textBox3.Text = formattedLineCount.ToString();
            textBox4.Text = wordCount.ToString();

            // 统计单词出现次数
            Dictionary<string, int> wordFrequency = new Dictionary<string, int>();
            foreach (string line in lines)
            {
                MatchCollection matches = Regex.Matches(line, @"\b\w+\b");
                foreach (Match match in matches)
                {
                    string word = match.Value;
                    if (wordFrequency.ContainsKey(word))
                    {
                        wordFrequency[word]++;
                    }
                    else
                    {
                        wordFrequency[word] = 1;
                    }
                }
            }

            listBox1.Items.Clear();
            foreach (KeyValuePair<string, int> pair in wordFrequency)
            {
                listBox1.Items.Add($"{pair.Key}: {pair.Value}");
            }
        
        }
    }
}