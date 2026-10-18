using System.Diagnostics.Metrics;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;

namespace Bank_Account
{
    class BankAccount
    {
        public string ownerName { get; set; }
        private int _password;
        public int password { 

            get=> _password; 
            private set=> _password = value;
        }
        private decimal _balance;
        public decimal balance { get => _balance; 
            private set
            {
                if(value >= 0)
                {
                    _balance = value;
                }
            }
        }
        private int Counter = 0 ;
        public bool IsLock { get; private set; } = false;  
        public BankAccount(string ownerName, int password, decimal balance)
        {
            this.ownerName = ownerName;
            this.password = password;
            this.balance = balance;
        }
        public decimal Deposite(decimal amount , int password)
        {
            if (IsLock)
            {
                Console.WriteLine("locked account");
                return balance;
            }
            if (password == this.password && amount >= 0 )
            {
                return balance += amount;
                
            }else
            {
                Console.WriteLine("unsucceful operation ");
                return _balance;
            }
         
        }
        public decimal WithDraw(decimal amount , int password)
        {
            if (IsLock)
            {
                Console.WriteLine("locked account");
                return balance;
            }
            if (amount >= 0 && amount <=  balance && password == this.password)
            {
                return balance -= amount;
            }
            else
            {
                Console.WriteLine("Unsuccessful operation");
                    return balance;
            }
        }
        public decimal Transfer(BankAccount acc , int password , decimal amount)
        {
            if (IsLock)
            {
                Console.WriteLine("locked account");
                return balance;
            }else if (acc.IsLock)
            {
                Console.WriteLine($"account of {acc.ownerName} is locked");
                return balance;
            }

            if (password == this.password && amount <= balance && amount >= 0 )
            {
                Console.WriteLine("succeful operation");
                balance -= amount;
                acc.balance += amount;
                return balance;
            }
            else
            {
                Console.WriteLine("unsucceful operation");
                return balance;
            }
        }
        public void DisplayBalance(int password)
        {
            if (IsLock)
            {
                Console.WriteLine("locked account");
                return;
            }
            if (password == this.password && !IsLock)
            {

                Console.WriteLine($"your balance Mr {ownerName} : {balance:C0}");
            }else
            {

                Console.WriteLine("wrong password");
            }
        }
        public bool changePassword(int oldPass , int newPass)
        {
            if (oldPass == this._password && !IsLock)
            {
                if (oldPass == newPass)
                {
                    Console.WriteLine("please enter another password");
                    return false;
                }else
                {
                    password = newPass;
                    Console.WriteLine("password changed");
                    return true;

                }
            }
            else
            {

                Console.WriteLine("incorrect password");
                Counter++;
                if (Counter >= 3)
                {
                    IsLock = true;
                    Console.WriteLine("account locked");
                }
                return false;
            }
        }
        public void accountState()
        {
            if(!IsLock)
                Console.WriteLine("available account ");
            else
                Console.WriteLine("locked acount");
        }


    }
    internal class Program
    {
        static void Main(string[] args)
        {
            BankAccount acc1 = new BankAccount("mohamed", 123 , 2000);
            acc1.Deposite(1000 , 123); //3000
            acc1.DisplayBalance(143);  //wrong password
            acc1.Deposite(500,123);   // 3500
            acc1.WithDraw(1000,423);  // unsucceful operation
            acc1.DisplayBalance(123);  //3500
            Console.WriteLine("------------------------------");
            BankAccount acc2 = new BankAccount("ahmed" ,456, 1000);
            acc2.Deposite(2000,456);
            acc2.WithDraw(4000,456);  //unsuccefuloperation
            acc2.DisplayBalance(456); // 3000

            Console.WriteLine("-------------------------");
            acc2.Transfer(acc1, 456, 1000);      
            acc2.DisplayBalance(456);  // ahmed 2000 
            acc1.DisplayBalance(123);  // mohamed 4500
            Console.WriteLine("-------------------------------");
            acc1.changePassword(123, 789); //password changed 
            acc1.Deposite(1000, 123);
            acc1.DisplayBalance(789);
            Console.WriteLine("-------------------------------");
            acc1.changePassword(567, 123);
            acc1.changePassword(567, 123);
            acc1.changePassword(567, 123);
            acc1.accountState();
            acc1.Deposite(200, 789);
            acc2.Transfer(acc1, 456, 100); 


        }
    }
}
