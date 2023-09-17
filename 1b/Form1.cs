using System.Timers;
namespace _1b
{
    public partial class Form1 : Form
    {
        System.Timers.Timer timer;
        int totalQuestions = 5; // 题目数量
        int questionCount = 0; // 当前题目计数
        int correctCount = 0; // 正确答案计数
        Random random = new Random();
        DateTime startTime;
        private bool answeringQuestion;
        private int result;
        public Form1()
        {
            InitializeComponent();
            label1.Text = "欢迎参加随机加减法测试！";
            label2.Text = "按任意键开始...";
            label3.Text = ""; // 清空文本
            label4.Text = ""; // 清空文本
            label5.Text = ""; // 清空文本
            label6.Text = ""; // 清空文本
            this.KeyPreview = true; // 允许窗体接收按键事件
            this.KeyDown += Form1_KeyDown; // 注册按键事件处理程序

            timer = new System.Timers.Timer(10000); // 限定时间为10秒
            timer.Elapsed += TimerElapsed;

        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            StartTest();
        }
        private void StartTest()
        {
            label2.Text = ""; // 清空文本
            startTime = DateTime.Now; // 记录开始时间
            questionCount = 0; // 重置题目计数
            correctCount = 0; // 重置正确答案数量
            GenerateQuestion(); // 生成第一道题目
            this.KeyDown -= Form1_KeyDown;        
            timer.Start();        
        }
        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            MessageBox.Show("时间到，测试结束！");

            this.Invoke(new Action(() =>
            {
                timer.Stop(); // 停止计时器
                TimeSpan duration = DateTime.Now - startTime; // 计算耗时

                label2.Text = "恭喜您完成测试！";
                label3.Text = "正确答案数量: " + correctCount;
                label4.Text = "总题目数量: " + totalQuestions;
                label5.Text = "耗时: " + duration.ToString(@"mm\:ss");
                label6.Text = "得分: " + (correctCount / (float)totalQuestions * 100);

                MessageBox.Show("按任意键退出...");
                Application.Exit();
            }));
        }
        private void GenerateQuestion()
        {           
            timer.Stop(); // 暂停计时器

            int num1 = random.Next(1, 101); // 随机生成1到100的整数
            int num2 = random.Next(1, 101);
            int operation = random.Next(1, 3);

            result = operation == 1 ? num1 + num2 : num1 - num2; // 计算正确答案

            label1.Text = num1 + (operation == 1 ? " + " : " - ") + num2 + " = "; // 显示题目

            questionCount++;
            
            textBox1.Focus(); // 将焦点设置到文本框，等待用户输入

            answeringQuestion = true;
            if (questionCount >= totalQuestions)
            {
                timer.Stop(); // 停止计时器
            }
            else
            {
                timer.Start(); // 继续计时
            }

        }
        private void textBox1_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (answeringQuestion)
                {
                    int userAnswer;
                    if (int.TryParse(textBox1.Text.Trim(), out userAnswer))
                    {
                        if (userAnswer == result)
                        {
                            label2.ForeColor = Color.Green;
                            label2.Text = "回答正确！";
                            correctCount++;
                        }
                        else
                        {
                            label2.ForeColor = Color.Red;
                            label2.Text = "回答错误！ 正确答案是：" + result;
                        }
                        if (questionCount < totalQuestions)
                        {
                            textBox1.Clear(); // 清空文本框
                            answeringQuestion = false;
                            GenerateQuestion(); // 继续下一道题目   
                        }
                        else
                        {
                            timer.Stop(); // 停止计时器
                        }
                    }
                    else
                    {
                        MessageBox.Show("请输入一个有效的整数！");
                        textBox1.Clear();
                    }
                }
            }
        }
    }
}