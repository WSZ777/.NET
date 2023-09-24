using System;

namespace BankSimulation
{
    // 银行类
    public class Bank
    {
        public void OpenAccount(Account account)
        {
            // 打开账户逻辑
        }
        public Account GetAccount(string accountNumber)
        {
            // 在银行类中实现获取账户的逻辑
            // 可以基于账户号码从存储中查找对应的账户并返回
            // 如果找不到对应账户，可以返回 null 或抛出异常

            // 示例逻辑：
            if (accountNumber == "1234567890")
            {
                return new Account()
                {
                    AccountNumber = "1234567890",
                    Balance = 10000m
                };
            }
            else if (accountNumber == "0987654321")
            {
                return new CreditAccount()
                {
                    AccountNumber = "0987654321",
                    Balance = 5000m,
                    CreditLimit = 2000m
                };
            }

            return null; // 或者抛出账户不存在的异常
        }
    }

    // 账号类
    public class Account
    {
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
        

        public virtual void Withdraw(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException("amount", "取款金额必须大于零");
            }

            if (amount > Balance)
            {
                throw new InvalidOperationException("账户余额不足");
            }

            Console.WriteLine($"从账户 {AccountNumber} 取款 {amount:C}");
            Balance -= amount;
        }
    }

    // 信用账号类
    public class CreditAccount : Account
    {
        public decimal CreditLimit { get; set; }

        public override void Withdraw(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException("amount", "取款金额必须大于零");
            }

            if (amount > Balance + CreditLimit)
            {
                throw new InvalidOperationException("信用额度不足");
            }

            Console.WriteLine($"从信用账号 {AccountNumber} 取款 {amount:C}（包括信用额度）");
            if (amount > Balance)
            {
                decimal creditUsed = amount - Balance;
                Balance = 0;
                CreditLimit -= creditUsed;
            }
            else
            {
                Balance -= amount;
            }
        }
    }

    // ATM类
    public class ATM
    {
        public event EventHandler<BigMoneyArgs> BigMoneyFetched;

        public void WithdrawMoney(Account account, decimal amount)
        {
            if (amount > 10000m)
            {
                OnBigMoneyFetched(account, amount);
            }
            else
            {
                try
                {
                    account.Withdraw(amount);
                    Console.WriteLine($"从账户 {account.AccountNumber} 取款完成");
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Console.WriteLine("错误：" + ex.Message);
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine("错误：" + ex.Message);
                }
            }
        }

        protected virtual void OnBigMoneyFetched(Account account, decimal amount)
        {
            BigMoneyFetched?.Invoke(this, new BigMoneyArgs(account, amount));
        }
    }

    // 自定义事件参数类
    public class BigMoneyArgs : EventArgs
    {
        public Account Account { get; }
        public decimal Amount { get; }

        public BigMoneyArgs(Account account, decimal amount)
        {
            Account = account;
            Amount = amount;
        }
    }

    // 自定义异常类
    public class BadCashException : Exception
    {
        public BadCashException(string message) : base(message)
        {
        }
    }

    public class Program
    {
        private static Bank bank;
        private static ATM atm;
        private static Random random;

        public static void Main(string[] args)
        {
            bank = new Bank();
            atm = new ATM();
            random = new Random();

            // 运行示例代码
            OpenAccount();
            WithdrawMoney();

            Console.ReadLine();
        }

        private static void OpenAccount()
        {
            Account account = new Account();
            account.AccountNumber = "1234567890";
            account.Balance = 10000m;
            bank.OpenAccount(account);

            CreditAccount creditAccount = new CreditAccount();
            creditAccount.AccountNumber = "0987654321";
            creditAccount.Balance = 5000m;
            creditAccount.CreditLimit = 2000m;
            bank.OpenAccount(creditAccount);
        }

        private static void WithdrawMoney()
        {
            Account account = bank.GetAccount("1234567890");
            decimal amount = 6000m; // 取款金额
            if (amount > account.Balance)
            {
                Console.WriteLine("错误：信用额度不足");
            }
            else
            {
                try
                {
                    atm.WithdrawMoney(account, amount);
                }
                catch (BadCashException ex)
                {
                    Console.WriteLine("错误：" + ex.Message);
                }
            }

            Account creditAccount = bank.GetAccount("0987654321");
            decimal creditAmount = 7000m; // 取款金额
            if (creditAmount > creditAccount.Balance + ((CreditAccount)creditAccount).CreditLimit)
            {
                Console.WriteLine("错误：信用额度不足");
            }
            else
            {
                try
                {
                    atm.WithdrawMoney(creditAccount, creditAmount);
                }
                catch (BadCashException ex)
                {
                    Console.WriteLine("错误：" + ex.Message);
                }
            }
        }

        private static bool IsBadCash()
        {
            // 模拟30%的坏钞率
            return random.Next(1, 101) <= 30;
        }
    }
}

