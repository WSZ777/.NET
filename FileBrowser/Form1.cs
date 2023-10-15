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
            // 添加 "文件" 菜单项及子菜单项
            ToolStripMenuItem fileMenu = new ToolStripMenuItem("文件(&F)");
            fileMenu.DropDownItems.Add("打开(&O)...", null, OpenFile_Click);
            fileMenu.DropDownItems.Add("保存(&S)", null, SaveFile_Click);
            fileMenu.DropDownItems.Add("-");
            fileMenu.DropDownItems.Add("退出(&X)", null, Exit_Click);

            // 添加 "编辑" 菜单项及子菜单项
            ToolStripMenuItem editMenu = new ToolStripMenuItem("编辑(&E)");
            editMenu.DropDownItems.Add("复制(&C)", null, Copy_Click);
            editMenu.DropDownItems.Add("剪切(&X)", null, Cut_Click);
            editMenu.DropDownItems.Add("粘贴(&V)", null, Paste_Click);

            // 将菜单项添加到 MenuStrip 控件上
            menuStrip.Items.Add(fileMenu);
            menuStrip.Items.Add(editMenu);

            // 将 MenuStrip 控件添加到窗体中
            Controls.Add(menuStrip);

            // 添加工具栏及其元素
            toolStrip.Items.Add(new ToolStripButton("打开", null, OpenFile_Click));
            toolStrip.Items.Add(new ToolStripButton("保存", null, SaveFile_Click));
            toolStrip.Items.Add(new ToolStripSeparator());
            toolStrip.Items.Add(new ToolStripButton("复制", null, Copy_Click));
            toolStrip.Items.Add(new ToolStripButton("剪切", null, Cut_Click));
            toolStrip.Items.Add(new ToolStripButton("粘贴", null, Paste_Click));

            // 将工具栏添加到窗体中
            Controls.Add(toolStrip);

        }

        // 添加菜单项的点击事件处理程序
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
            // 添加保存文件的代码
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