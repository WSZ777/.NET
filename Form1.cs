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
            // 连接到SQLite数据库
            connection = new SQLiteConnection("Data Source=student.db;Version=3;");
            connection.Open();

            // 创建School表
            string createSchoolTableQuery = "CREATE TABLE IF NOT EXISTS School (Id INTEGER PRIMARY KEY AUTOINCREMENT, Name TEXT, Address TEXT)";
            SQLiteCommand createSchoolTableCommand = new SQLiteCommand(createSchoolTableQuery, connection);
            createSchoolTableCommand.ExecuteNonQuery();

            // 创建Class表
            string createClassTableQuery = "CREATE TABLE IF NOT EXISTS Class (Id INTEGER PRIMARY KEY AUTOINCREMENT, Name TEXT, Grade TEXT, SchoolId INTEGER)";
            SQLiteCommand createClassTableCommand = new SQLiteCommand(createClassTableQuery, connection);
            createClassTableCommand.ExecuteNonQuery();

            // 创建Student表
            string createStudentTableQuery = "CREATE TABLE IF NOT EXISTS Student (Id INTEGER PRIMARY KEY AUTOINCREMENT, Name TEXT, Age INTEGER, Gender TEXT, ClassId INTEGER)";
            SQLiteCommand createStudentTableCommand = new SQLiteCommand(createStudentTableQuery, connection);
            createStudentTableCommand.ExecuteNonQuery();

            // 创建Log表
            string createLogTableQuery = "CREATE TABLE IF NOT EXISTS Log (Id INTEGER PRIMARY KEY AUTOINCREMENT, Timestamp DATETIME DEFAULT CURRENT_TIMESTAMP, Action TEXT)";
            SQLiteCommand createLogTableCommand = new SQLiteCommand(createLogTableQuery, connection);
            createLogTableCommand.ExecuteNonQuery();
        }

        private void LoadSchools()
        {
            // 查询所有学校
            string query = "SELECT * FROM School";
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, connection);
            DataTable table = new DataTable();
            adapter.Fill(table);

            // 绑定学校列表到TreeView控件上
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

            // 查询指定学校的班级
            string query = "SELECT * FROM Class WHERE SchoolId = @SchoolId";
            SQLiteCommand command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@SchoolId", schoolId);
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);

            // 绑定班级列表到学校节点下的子节点上
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

            // 查询指定班级的学生
            string query = "SELECT * FROM Student WHERE ClassId = @ClassId";
            SQLiteCommand command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@ClassId", classId);
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);

            // 绑定学生列表到班级节点下的子节点上
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

            // 插入学校信息到数据库
            string insertQuery = "INSERT INTO School (Name, Address) VALUES (@Name, @Address)";
            SQLiteCommand command = new SQLiteCommand(insertQuery, connection);
            command.Parameters.AddWithValue("@Name", schoolName);
            command.Parameters.AddWithValue("@Address", schoolAddress);
            command.ExecuteNonQuery();

            // 添加操作记录到Log表
            string logMessage = "添加学校：" + schoolName;
            LogAction(logMessage);

            LoadSchools();
        }

        private void addClassButton_Click(object sender, EventArgs e)
        {
            string className = classNameTextBox.Text;
            string classGrade = classGradeTextBox.Text;
            int schoolId = Convert.ToInt32(schoolTreeView.SelectedNode.Tag);

            // 插入班级信息到数据库
            string insertQuery = "INSERT INTO Class (Name, Grade, SchoolId) VALUES (@Name, @Grade, @SchoolId)";
            SQLiteCommand command = new SQLiteCommand(insertQuery, connection);
            command.Parameters.AddWithValue("@Name", className);
            command.Parameters.AddWithValue("@Grade", classGrade);
            command.Parameters.AddWithValue("@SchoolId", schoolId);
            command.ExecuteNonQuery();

            // 添加操作记录到Log表
            string logMessage = "添加班级：" + className;
            LogAction(logMessage);

            LoadClasses(schoolTreeView.SelectedNode);
        }

        private void addStudentButton_Click(object sender, EventArgs e)
        {
            string studentName = studentNameTextBox.Text;
            int studentAge = Convert.ToInt32(studentAgeTextBox.Text);
            string studentGender = studentGenderTextBox.Text;
            int classId = Convert.ToInt32(schoolTreeView.SelectedNode.Tag);

            // 插入学生信息到数据库
            string insertQuery = "INSERT INTO Student (Name, Age, Gender, ClassId) VALUES (@Name, @Age, @Gender, @ClassId)";
            SQLiteCommand command = new SQLiteCommand(insertQuery, connection);
            command.Parameters.AddWithValue("@Name", studentName);
            command.Parameters.AddWithValue("@Age", studentAge);
            command.Parameters.AddWithValue("@Gender", studentGender);
            command.Parameters.AddWithValue("@ClassId", classId);
            command.ExecuteNonQuery();

            // 添加操作记录到Log表
            string logMessage = "添加学生：" + studentName;
            LogAction(logMessage);

            LoadStudents(schoolTreeView.SelectedNode);
        }

        private void deleteSchoolButton_Click(object sender, EventArgs e)
        {
            int schoolId = Convert.ToInt32(schoolTreeView.SelectedNode.Tag);

            // 删除学校信息及其关联的班级和学生信息
            string deleteQuery = "DELETE FROM School WHERE Id = @SchoolId";
            SQLiteCommand command = new SQLiteCommand(deleteQuery, connection);
            command.Parameters.AddWithValue("@SchoolId", schoolId);
            command.ExecuteNonQuery();

            // 添加操作记录到Log表
            string logMessage = "删除学校：" + schoolTreeView.SelectedNode.Text;
            LogAction(logMessage);

            LoadSchools();
        }

        private void deleteClassButton_Click(object sender, EventArgs e)
        {
            int classId = Convert.ToInt32(schoolTreeView.SelectedNode.Tag);

            // 删除班级信息及其关联的学生信息
            string deleteQuery = "DELETE FROM Class WHERE Id = @ClassId";
            SQLiteCommand command = new SQLiteCommand(deleteQuery, connection);
            command.Parameters.AddWithValue("@ClassId", classId);
            command.ExecuteNonQuery();

            // 添加操作记录到Log表
            string logMessage = "删除班级：" + schoolTreeView.SelectedNode.Text;
            LogAction(logMessage);
            LoadSchools();
        }

        private void deleteStudentButton_Click(object sender, EventArgs e)
        {
            int studentId = Convert.ToInt32(schoolTreeView.SelectedNode.Tag);

            // 删除学生信息
            string deleteQuery = "DELETE FROM Student WHERE Id = @StudentId";
            SQLiteCommand command = new SQLiteCommand(deleteQuery, connection);
            command.Parameters.AddWithValue("@StudentId", studentId);
            command.ExecuteNonQuery();

            // 添加操作记录到Log表
            string logMessage = "删除学生：" + schoolTreeView.SelectedNode.Text;
            LogAction(logMessage);

            LoadStudents(schoolTreeView.SelectedNode.Parent);
        }

        private void LogAction(string message)
        {
            // 插入操作记录到Log表
            string insertQuery = "INSERT INTO Log (Action) VALUES (@Action)";
            SQLiteCommand command = new SQLiteCommand(insertQuery, connection);
            command.Parameters.AddWithValue("@Action", message);
            command.ExecuteNonQuery();
        }

        private void searchLogButton_Click(object sender, EventArgs e)
        {
            string keyword = searchLogTextBox.Text;

            // 查询Log表中包含关键字的操作记录
            string query = "SELECT * FROM Log WHERE Action LIKE @Keyword";
            SQLiteCommand command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@Keyword", "%" + keyword + "%");
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);

            // 在DataGridView控件中显示查询结果
            logDataGridView.DataSource = table;
        }

    }
}