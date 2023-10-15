using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace FileBrowser
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            PopulateTreeView("C:\\", treeView.Nodes);
            // ��� "�ļ�" �˵���Ӳ˵���
            ToolStripMenuItem fileMenu = new ToolStripMenuItem("�ļ�(&F)");
            fileMenu.DropDownItems.Add("��(&O)...", null, OpenFile_Click);
            fileMenu.DropDownItems.Add("����(&S)", null, SaveFile_Click);
            fileMenu.DropDownItems.Add("-");
            fileMenu.DropDownItems.Add("�˳�(&X)", null, Exit_Click);

            // ��� "�༭" �˵���Ӳ˵���
            ToolStripMenuItem editMenu = new ToolStripMenuItem("�༭(&E)");
            editMenu.DropDownItems.Add("����(&C)", null, Copy_Click);
            editMenu.DropDownItems.Add("����(&X)", null, Cut_Click);
            editMenu.DropDownItems.Add("ճ��(&V)", null, Paste_Click);

            // ���˵�����ӵ� MenuStrip �ؼ���
            menuStrip.Items.Add(fileMenu);
            menuStrip.Items.Add(editMenu);

            // �� MenuStrip �ؼ���ӵ�������
            Controls.Add(menuStrip);

            // ��ӹ���������Ԫ��
            toolStrip.Items.Add(new ToolStripButton("��", null, OpenFile_Click));
            toolStrip.Items.Add(new ToolStripButton("����", null, SaveFile_Click));
            toolStrip.Items.Add(new ToolStripSeparator());
            toolStrip.Items.Add(new ToolStripButton("����", null, Copy_Click));
            toolStrip.Items.Add(new ToolStripButton("����", null, Cut_Click));
            toolStrip.Items.Add(new ToolStripButton("ճ��", null, Paste_Click));

            // ����������ӵ�������
            Controls.Add(toolStrip);

        }

        // ��Ӳ˵���ĵ���¼��������
        private void OpenFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string selectedPath = openFileDialog.FileName;
                string selectedDirectory = Path.GetDirectoryName(selectedPath);
                PopulateTreeView(selectedDirectory, treeView.Nodes);
                treeView.SelectedNode = treeView.Nodes.Find(selectedPath, true)[0];
            }
        }

        private void SaveFile_Click(object sender, EventArgs e)
        {
            // ��ӱ����ļ��Ĵ���
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Copy_Click(object sender, EventArgs e)
        {

        }

        private void Cut_Click(object sender, EventArgs e)
        {

        }

        private void Paste_Click(object sender, EventArgs e)
        {

        }

        private void PopulateTreeView(string path, TreeNodeCollection parentNode)
        {
            try
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(path);
                TreeNode rootNode = new TreeNode(directoryInfo.Name);

                foreach (DirectoryInfo directory in directoryInfo.GetDirectories())
                {
                    TreeNode childNode = new TreeNode(directory.Name);
                    rootNode.Nodes.Add(childNode);
                }
                parentNode.Add(rootNode);
                rootNode.Expand();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void treeView_NodeMouseClick_1(object sender, TreeNodeMouseClickEventArgs e)
        {
            string selectedPath = e.Node.FullPath;
            PopulateListView(selectedPath);
        }

        private void PopulateListView(string path)
        {
            try
            {
                listView.Items.Clear();
                DirectoryInfo directoryInfo = new DirectoryInfo(path);

                foreach (DirectoryInfo directory in directoryInfo.GetDirectories())
                {
                    ListViewItem item = new ListViewItem(directory.Name, 0);
                    listView.Items.Add(item);
                }

                foreach (FileInfo file in directoryInfo.GetFiles())
                {
                    ListViewItem item = new ListViewItem(file.Name, 1);
                    listView.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void listView_ItemActivate_1(object sender, EventArgs e)
        {
            if (listView.SelectedItems.Count > 0)
            {
                string selectedItem = listView.SelectedItems[0].Text;
                string selectedPath = Path.Combine(treeView.SelectedNode.FullPath, selectedItem);
                string extension = Path.GetExtension(selectedPath).ToLower();

                if (extension == ".exe")
                {
                    Process.Start(selectedPath);
                }
                else if (extension == ".txt")
                {
                    Process.Start("notepad.exe", selectedPath);
                }
            }
        }


    }
}