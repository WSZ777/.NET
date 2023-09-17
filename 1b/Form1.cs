using System.Timers;
namespace _1b
{
    public partial class Form1 : Form
    {
        System.Timers.Timer timer;
        int totalQuestions = 5; // ��Ŀ����
        int questionCount = 0; // ��ǰ��Ŀ����
        int correctCount = 0; // ��ȷ�𰸼���
        Random random = new Random();
        DateTime startTime;
        private bool answeringQuestion;
        private int result;
        public Form1()
        {
            InitializeComponent();
            label1.Text = "��ӭ�μ�����Ӽ������ԣ�";
            label2.Text = "���������ʼ...";
            label3.Text = ""; // ����ı�
            label4.Text = ""; // ����ı�
            label5.Text = ""; // ����ı�
            label6.Text = ""; // ����ı�
            this.KeyPreview = true; // ��������հ����¼�
            this.KeyDown += Form1_KeyDown; // ע�ᰴ���¼��������

            timer = new System.Timers.Timer(10000); // �޶�ʱ��Ϊ10��
            timer.Elapsed += TimerElapsed;

        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            StartTest();
        }
        private void StartTest()
        {
            label2.Text = ""; // ����ı�
            startTime = DateTime.Now; // ��¼��ʼʱ��
            questionCount = 0; // ������Ŀ����
            correctCount = 0; // ������ȷ������
            GenerateQuestion(); // ���ɵ�һ����Ŀ
            this.KeyDown -= Form1_KeyDown;        
            timer.Start();        
        }
        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            MessageBox.Show("ʱ�䵽�����Խ�����");

            this.Invoke(new Action(() =>
            {
                timer.Stop(); // ֹͣ��ʱ��
                TimeSpan duration = DateTime.Now - startTime; // �����ʱ

                label2.Text = "��ϲ����ɲ��ԣ�";
                label3.Text = "��ȷ������: " + correctCount;
                label4.Text = "����Ŀ����: " + totalQuestions;
                label5.Text = "��ʱ: " + duration.ToString(@"mm\:ss");
                label6.Text = "�÷�: " + (correctCount / (float)totalQuestions * 100);

                MessageBox.Show("��������˳�...");
                Application.Exit();
            }));
        }
        private void GenerateQuestion()
        {           
            timer.Stop(); // ��ͣ��ʱ��

            int num1 = random.Next(1, 101); // �������1��100������
            int num2 = random.Next(1, 101);
            int operation = random.Next(1, 3);

            result = operation == 1 ? num1 + num2 : num1 - num2; // ������ȷ��

            label1.Text = num1 + (operation == 1 ? " + " : " - ") + num2 + " = "; // ��ʾ��Ŀ

            questionCount++;
            
            textBox1.Focus(); // ���������õ��ı��򣬵ȴ��û�����

            answeringQuestion = true;
            if (questionCount >= totalQuestions)
            {
                timer.Stop(); // ֹͣ��ʱ��
            }
            else
            {
                timer.Start(); // ������ʱ
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
                            label2.Text = "�ش���ȷ��";
                            correctCount++;
                        }
                        else
                        {
                            label2.ForeColor = Color.Red;
                            label2.Text = "�ش���� ��ȷ���ǣ�" + result;
                        }
                        if (questionCount < totalQuestions)
                        {
                            textBox1.Clear(); // ����ı���
                            answeringQuestion = false;
                            GenerateQuestion(); // ������һ����Ŀ   
                        }
                        else
                        {
                            timer.Stop(); // ֹͣ��ʱ��
                        }
                    }
                    else
                    {
                        MessageBox.Show("������һ����Ч��������");
                        textBox1.Clear();
                    }
                }
            }
        }
    }
}