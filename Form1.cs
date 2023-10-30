using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;

namespace StudentManagementSystem
{
    public partial class Form1 : Form
    {
        private SQLiteConnection connection;
        public Form1()
        {
            InitializeComponent();
            InitializeDatabase();
            LoadSchools();
        }

        private void InitializeDatabase()
        {
            // ���ӵ�SQLite���ݿ�
            connection = new SQLiteConnection("Data Source=student.db;Version=3;");
            connection.Open();

            // ����School��
            string createSchoolTableQuery = "CREATE TABLE IF NOT EXISTS School (Id INTEGER PRIMARY KEY AUTOINCREMENT, Name TEXT, Address TEXT)";
            SQLiteCommand createSchoolTableCommand = new SQLiteCommand(createSchoolTableQuery, connection);
            createSchoolTableCommand.ExecuteNonQuery();

            // ����Class��
            string createClassTableQuery = "CREATE TABLE IF NOT EXISTS Class (Id INTEGER PRIMARY KEY AUTOINCREMENT, Name TEXT, Grade TEXT, SchoolId INTEGER)";
            SQLiteCommand createClassTableCommand = new SQLiteCommand(createClassTableQuery, connection);
            createClassTableCommand.ExecuteNonQuery();

            // ����Student��
            string createStudentTableQuery = "CREATE TABLE IF NOT EXISTS Student (Id INTEGER PRIMARY KEY AUTOINCREMENT, Name TEXT, Age INTEGER, Gender TEXT, ClassId INTEGER)";
            SQLiteCommand createStudentTableCommand = new SQLiteCommand(createStudentTableQuery, connection);
            createStudentTableCommand.ExecuteNonQuery();

            // ����Log��
            string createLogTableQuery = "CREATE TABLE IF NOT EXISTS Log (Id INTEGER PRIMARY KEY AUTOINCREMENT, Timestamp DATETIME DEFAULT CURRENT_TIMESTAMP, Action TEXT)";
            SQLiteCommand createLogTableCommand = new SQLiteCommand(createLogTableQuery, connection);
            createLogTableCommand.ExecuteNonQuery();
        }

        private void LoadSchools()
        {
            // ��ѯ����ѧУ
            string query = "SELECT * FROM School";
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, connection);
            DataTable table = new DataTable();
            adapter.Fill(table);

            // ��ѧУ�б�TreeView�ؼ���
            schoolTreeView.Nodes.Clear();
            foreach (DataRow row in table.Rows)
            {
                int schoolId = Convert.ToInt32(row["Id"]);
                string schoolName = row["Name"].ToString();
                string schoolAddress = row["Address"].ToString();

                TreeNode schoolNode = new TreeNode(schoolName);
                schoolNode.Tag = schoolId;

                LoadClasses(schoolNode);

                schoolTreeView.Nodes.Add(schoolNode);
            }
        }

        private void LoadClasses(TreeNode schoolNode)
        {
            int schoolId = Convert.ToInt32(schoolNode.Tag);

            // ��ѯָ��ѧУ�İ༶
            string query = "SELECT * FROM Class WHERE SchoolId = @SchoolId";
            SQLiteCommand command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@SchoolId", schoolId);
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);

            // �󶨰༶�б�ѧУ�ڵ��µ��ӽڵ���
            schoolNode.Nodes.Clear();
            foreach (DataRow row in table.Rows)
            {
                int classId = Convert.ToInt32(row["Id"]);
                string className = row["Name"].ToString();
                string classGrade = row["Grade"].ToString();

                TreeNode classNode = new TreeNode(className + " (" + classGrade + ")");
                classNode.Tag = classId;

                LoadStudents(classNode);

                schoolNode.Nodes.Add(classNode);
            }
        }

        private void LoadStudents(TreeNode classNode)
        {
            int classId = Convert.ToInt32(classNode.Tag);

            // ��ѯָ���༶��ѧ��
            string query = "SELECT * FROM Student WHERE ClassId = @ClassId";
            SQLiteCommand command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@ClassId", classId);
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);

            // ��ѧ���б��༶�ڵ��µ��ӽڵ���
            classNode.Nodes.Clear();
            foreach (DataRow row in table.Rows)
            {
                int studentId = Convert.ToInt32(row["Id"]);
                string studentName = row["Name"].ToString();
                int studentAge = Convert.ToInt32(row["Age"]);
                string studentGender = row["Gender"].ToString();

                TreeNode studentNode = new TreeNode(studentName + " (" + studentAge + ", " + studentGender + ")");
                studentNode.Tag = studentId;

                classNode.Nodes.Add(studentNode);
            }
        }

        private void addSchoolButton_Click(object sender, EventArgs e)
        {
            string schoolName = schoolNameTextBox.Text;
            string schoolAddress = schoolAddressTextBox.Text;

            // ����ѧУ��Ϣ�����ݿ�
            string insertQuery = "INSERT INTO School (Name, Address) VALUES (@Name, @Address)";
            SQLiteCommand command = new SQLiteCommand(insertQuery, connection);
            command.Parameters.AddWithValue("@Name", schoolName);
            command.Parameters.AddWithValue("@Address", schoolAddress);
            command.ExecuteNonQuery();

            // ��Ӳ�����¼��Log��
            string logMessage = "���ѧУ��" + schoolName;
            LogAction(logMessage);

            LoadSchools();
        }

        private void addClassButton_Click(object sender, EventArgs e)
        {
            string className = classNameTextBox.Text;
            string classGrade = classGradeTextBox.Text;
            int schoolId = Convert.ToInt32(schoolTreeView.SelectedNode.Tag);

            // ����༶��Ϣ�����ݿ�
            string insertQuery = "INSERT INTO Class (Name, Grade, SchoolId) VALUES (@Name, @Grade, @SchoolId)";
            SQLiteCommand command = new SQLiteCommand(insertQuery, connection);
            command.Parameters.AddWithValue("@Name", className);
            command.Parameters.AddWithValue("@Grade", classGrade);
            command.Parameters.AddWithValue("@SchoolId", schoolId);
            command.ExecuteNonQuery();

            // ��Ӳ�����¼��Log��
            string logMessage = "��Ӱ༶��" + className;
            LogAction(logMessage);

            LoadClasses(schoolTreeView.SelectedNode);
        }

        private void addStudentButton_Click(object sender, EventArgs e)
        {
            string studentName = studentNameTextBox.Text;
            int studentAge = Convert.ToInt32(studentAgeTextBox.Text);
            string studentGender = studentGenderTextBox.Text;
            int classId = Convert.ToInt32(schoolTreeView.SelectedNode.Tag);

            // ����ѧ����Ϣ�����ݿ�
            string insertQuery = "INSERT INTO Student (Name, Age, Gender, ClassId) VALUES (@Name, @Age, @Gender, @ClassId)";
            SQLiteCommand command = new SQLiteCommand(insertQuery, connection);
            command.Parameters.AddWithValue("@Name", studentName);
            command.Parameters.AddWithValue("@Age", studentAge);
            command.Parameters.AddWithValue("@Gender", studentGender);
            command.Parameters.AddWithValue("@ClassId", classId);
            command.ExecuteNonQuery();

            // ��Ӳ�����¼��Log��
            string logMessage = "���ѧ����" + studentName;
            LogAction(logMessage);

            LoadStudents(schoolTreeView.SelectedNode);
        }

        private void deleteSchoolButton_Click(object sender, EventArgs e)
        {
            int schoolId = Convert.ToInt32(schoolTreeView.SelectedNode.Tag);

            // ɾ��ѧУ��Ϣ��������İ༶��ѧ����Ϣ
            string deleteQuery = "DELETE FROM School WHERE Id = @SchoolId";
            SQLiteCommand command = new SQLiteCommand(deleteQuery, connection);
            command.Parameters.AddWithValue("@SchoolId", schoolId);
            command.ExecuteNonQuery();

            // ��Ӳ�����¼��Log��
            string logMessage = "ɾ��ѧУ��" + schoolTreeView.SelectedNode.Text;
            LogAction(logMessage);

            LoadSchools();
        }

        private void deleteClassButton_Click(object sender, EventArgs e)
        {
            int classId = Convert.ToInt32(schoolTreeView.SelectedNode.Tag);

            // ɾ���༶��Ϣ���������ѧ����Ϣ
            string deleteQuery = "DELETE FROM Class WHERE Id = @ClassId";
            SQLiteCommand command = new SQLiteCommand(deleteQuery, connection);
            command.Parameters.AddWithValue("@ClassId", classId);
            command.ExecuteNonQuery();

            // ��Ӳ�����¼��Log��
            string logMessage = "ɾ���༶��" + schoolTreeView.SelectedNode.Text;
            LogAction(logMessage);
            LoadSchools();
        }

        private void deleteStudentButton_Click(object sender, EventArgs e)
        {
            int studentId = Convert.ToInt32(schoolTreeView.SelectedNode.Tag);

            // ɾ��ѧ����Ϣ
            string deleteQuery = "DELETE FROM Student WHERE Id = @StudentId";
            SQLiteCommand command = new SQLiteCommand(deleteQuery, connection);
            command.Parameters.AddWithValue("@StudentId", studentId);
            command.ExecuteNonQuery();

            // ��Ӳ�����¼��Log��
            string logMessage = "ɾ��ѧ����" + schoolTreeView.SelectedNode.Text;
            LogAction(logMessage);

            LoadStudents(schoolTreeView.SelectedNode.Parent);
        }

        private void LogAction(string message)
        {
            // ���������¼��Log��
            string insertQuery = "INSERT INTO Log (Action) VALUES (@Action)";
            SQLiteCommand command = new SQLiteCommand(insertQuery, connection);
            command.Parameters.AddWithValue("@Action", message);
            command.ExecuteNonQuery();
        }

        private void searchLogButton_Click(object sender, EventArgs e)
        {
            string keyword = searchLogTextBox.Text;

            // ��ѯLog���а����ؼ��ֵĲ�����¼
            string query = "SELECT * FROM Log WHERE Action LIKE @Keyword";
            SQLiteCommand command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@Keyword", "%" + keyword + "%");
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);

            // ��DataGridView�ؼ�����ʾ��ѯ���
            logDataGridView.DataSource = table;
        }

    }
}