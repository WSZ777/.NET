namespace _1a
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int number;
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox2.Text = "";
            if (int.TryParse(textBox1.Text,out int value)&&value>0)
            { 
                number = Convert.ToInt32(textBox1.Text);
            }
            else
            {
                number=0;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            PrintPrimeFactors(number);
            // 判断一个数是否为素数
            static bool IsPrime(int number)
            {
                if (number < 2)
                    return false;

                for (int i = 2; i <= Math.Sqrt(number); i++)
                {
                    if (number % i == 0)
                        return false;
                }

                return true;
            }
            // 输出一个数的所有素数因子
            void PrintPrimeFactors(int number)
            {
                for (int i = 2; i <= number; i++)
                {
                    if (number % i == 0 && IsPrime(i))
                    {
                        textBox2.Text += i.ToString()+",";

                        while (number % i == 0)
                        {
                            number /= i;
                        }
                    }
                }
            }
        }
    }
}