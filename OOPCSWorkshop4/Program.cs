using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPCSWorkshop4
{
    class Program
    {
        static void Main(string[] args)
        {
            Customer cus1 = new Customer("Tan Ah Kow", "2 Rich Street",
                          "P123123", 20);
            Customer cus2 = new Customer("Kim May Mee", "89 Gold Road",
                                      "P334412", 60);

            BankAccount a1 = new BankAccount("S0000223", cus1, 2000);
            Console.WriteLine(a1.CalculateInterest());
            OverdraftAccount a2 = new OverdraftAccount("O1230124", cus1, 2000);
            Console.WriteLine(a2.CalculateInterest());
            CurrentAccount a3 = new CurrentAccount("C1230125", cus2, 2000);
            Console.WriteLine(a3.CalculateInterest());
        }
    }

    public class BankAccount
    {
        //
        //attributes
        //

        private string acctNum;
        private Customer acctName;
        protected double balance;

        //
        //Constructor
        //

        public BankAccount(string num, Customer name, double balance)
        {
            acctNum = num;
            acctName = name;
            this.balance = balance;
        }

        public BankAccount() : this("000-000-000", new Customer(), 0)
        {
        }

        //
        //Properties
        //

        public string AccountNumber
        {
            get { return AccountNumber; }
        }

        public Customer AccountName
        {
            get { return acctName; }
            set { acctName = value; }
        }

        public double Balance
        {
            get { return balance; }
        }

        //
        // Methods
        //

        public bool Withdraw(double amount)
        {
            if (balance < amount)
            {
                Console.Error.WriteLine("Withdraw for {0} is unsuccessful", AccountName);
                return false;
            }

            else
            {
                balance -= amount;
                return true;
            }
        }

        public void Deposit(double amount)
        {
            balance += amount;
        }

        public bool TransferTo(double amount, BankAccount another)
        {
            if (Withdraw(amount))
            {
                another.Deposit(amount);
                return true;
            }
            else
            {
                Console.Error.WriteLine("TransferTo for {0} is unsuccessful", AccountName);
                return false;
            }
        }

        public double CalculateInterest()
        {
            return Balance * 0.01;
        }

        public void CreditInterest()
        {
            Deposit(CalculateInterest());
        }

        public string Show()
        {
            string m = String.Format("[Account number = {0}, account holder = {1}, account's balance = {2}]", acctNum, acctName.Show(), balance);
            return m;
        }
    }

    public class CurrentAccount : BankAccount
    {
        public CurrentAccount(string number, Customer holder, double bal) : base(number, holder, bal)
        {
        }

        public new double CalculateInterest()
        {
            return Balance * 0.0025;
        }

        public new string Show()
        {
            string m = String.Format("[Current account number = {0}, account holder = {1}, account's balance = {2}]", AccountNumber, AccountName.Show(), Balance);
            return m;
        }

    }

    public class OverdraftAccount : BankAccount
    {
        private static double interestRate = 0.25;
        private static double overdraftInterest = 6;

        public OverdraftAccount(string num, Customer holder, double bal) : base(num, holder, bal)
        {
        }

        public new bool Withdraw(double amount)
        {
            balance -= amount;
            return true;
        }

        public new double CalculateInterest()
        {
            return (balance > 0) ? (balance * interestRate / 100) : (balance * overdraftInterest / 100);
        }

        public new string Show()
        {
            string m = String.Format("[Overdraft account number = {0}," +
                " account holder = {1}, account's balance = {2}]", AccountNumber, AccountName.Show(), Balance);
            return m;
        }
    }

    public class Customer
    {
        //
        // Attributes
        //
        private string name;
        private string address;
        private string passport;
        private DateTime dateOfBirth;

        //
        // Constructor
        //

        public Customer(string name, string address, string passport, DateTime dob) : this(name, address, passport)
        {
            this.dateOfBirth = dob;
        }

        public Customer(string name, string address, string passport, int age) : this(name, address, passport)
        {
            this.dateOfBirth = new DateTime(DateTime.Now.Year - age, 1, 1);
        }

        public Customer(string name, string address, string passport)
        {
            this.name = name;
            this.address = address;
            this.passport = passport;

        }

        public Customer() : this("NoName", "NoAddress", "NoPassport", new DateTime(1980, 1, 1))
        {
        }

        //
        // Properties
        //

        public string Name
        {
            get { return name; }
        }

        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        public string Passport
        {
            get { return passport; }
            set { passport = value; }
        }

        public int Age
        {
            get
            {
                return DateTime.Now.Year - dateOfBirth.Year;
            }
        }

        //
        // Methods
        //
        public string Show()
        {
            string m = String.Format
                ("[Customer:name = {0}, address = {1}, passport = {2} and age = {3}]", Name, Address, Passport, Age);
            return m;
        }
    }



}
