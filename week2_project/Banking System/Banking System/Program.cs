using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem
{
    public class BankAccount
    {
        private int accountNumber;
        private string accountHolderName;
        private double balance;
        private string accountType;

        public BankAccount(int accountNumber, string accountHolderName, double balance, string accountType)
        {
            this.accountNumber = accountNumber;
            this.accountHolderName = accountHolderName;
            this.balance = balance;
            this.accountType = accountType;
        }
        public int AccountNumber
        {
            get { return accountNumber; }
        }
        public string AccountHolderName
        {
            get { return accountHolderName; }
            set { accountHolderName = value; }
        }

        public double Balance
        {
            get { return balance; }
        }

        public string AccountType
        {
            get { return accountType; }
        }

        public void Deposit(double amount)
        {
            if (amount > 0)
            {
                balance += amount;
                Console.WriteLine("Deposit successful. New balance: " + balance);
            }
            else
            {
                Console.WriteLine("Invalid deposit amount.");
            }
        }
        public bool Withdraw(double amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine("Invalid withdrawal amount.");
                return false;
            }
            else if (amount > balance)
            {
                Console.WriteLine("Insufficient funds.");
                return false;
            }
            else
            {
                balance -= amount;
                Console.WriteLine("Withdrawal successful. New balance: " + balance);
                return true;
            }
        }

        public void DisplayAccountInfo()
        {
            Console.WriteLine("Account Number: " + accountNumber);
            Console.WriteLine("Account Holder Name: " + accountHolderName);
            Console.WriteLine("Balance: " + balance);
            Console.WriteLine("Account Type: " + accountType);
        }

    }
    class Program
    {
        static BankAccount[] accounts = new BankAccount[100];
        static int accountCount = 0;
        static void Main(string[] args)
        {
            Console.WriteLine("================================");
            Console.WriteLine("Welcome to Simple Banking System");
            Console.WriteLine("================================");

            while (true)
            {
                DisplayMenu();
                int choice = GetUserChoice();

                switch (choice)
                {
                    case 1:
                        CreateAccount();
                        break;
                    case 2:
                        DepositMoney();
                        break;
                    case 3:
                        WithdrawMoney();
                        break;
                    case 4:
                        CheckBalance();
                        break;
                    case 5:
                        ViewAccountDetails();
                        break;
                    case 6:
                        ListAllAccounts();
                        break;
                    case 7:
                        Console.WriteLine("Thank you for using Simple Banking System. Goodbye!");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
                Console.Clear();
            }
        }
        public static void DisplayMenu()
        {
            Console.WriteLine("\n -- Bank Menu --");
            Console.WriteLine("1. Create Account");
            Console.WriteLine("2. Deposit Money");
            Console.WriteLine("3. Withdraw Money");
            Console.WriteLine("4. Check Balance");
            Console.WriteLine("5. View Account Details");
            Console.WriteLine("6. List All Accounts");
            Console.WriteLine("7. Exit");
            Console.WriteLine("Enter you choice (1-7): ");
        }
        public static int GetUserChoice()
        {
            string input = Console.ReadLine();
            if (int.TryParse(input, out int choice))
            {
                return choice;
            }
            else
            {
                return -1; // Invalid choice
            }

        }
        public static BankAccount FindAccount(int accountNumber)
        {
            for (int i = 0; i < accountCount; i++)
            {
                if (accounts[i].AccountNumber == accountNumber)
                {
                    return accounts[i];
                }
            }
            return null; // Account not found
        }
        public static void CreateAccount()
        {
            if (accountCount >= accounts.Length)
            {
                Console.WriteLine("Maximum account limit reached. Cannot create more accounts.");
                return;
            }
            Console.WriteLine("\n-- Create Account --");
            Console.Write("Enter account holder name:");
            string name = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Invalid name! Account creation failed.");
                return;
            }

            Console.Write("Enter account type (Savings/Checking):");
            string type = Console.ReadLine();

            Console.Write("Enter initial deposit amount:");
            string balanceInput = Console.ReadLine();

            if (double.TryParse(balanceInput, out double initialBalance) && initialBalance >= 0)
            {
                int accountNumber = 1000 + accountCount + 1;
                BankAccount newAccount = new BankAccount(accountNumber, name, initialBalance, type);

                accounts[accountCount] = newAccount;
                accountCount++;

                Console.WriteLine("Account created successfully! Your account number is: " + accountNumber);

            }
            else
            {
                Console.WriteLine("Invalid initial deposit amount! Account creation failed.");
            }
        }
        public static void DepositMoney()
        {
            Console.WriteLine("\n-- Deposit Money --");
            Console.Write("Enter account number:");
            string accountNumberInput = Console.ReadLine();
            if (int.TryParse(accountNumberInput, out int accountNumber))
            {
                BankAccount account = FindAccount(accountNumber);
                if (account != null)
                {
                    Console.Write("Enter deposit amount:");
                    string amountInput = Console.ReadLine();
                    if (double.TryParse(amountInput, out double amount) && amount > 0)
                    {
                        account.Deposit(amount);
                    }
                    else
                    {
                        Console.WriteLine("Invalid deposit amount!");
                    }
                }
                else
                {
                    Console.WriteLine("Account not found!");
                }
            }
            else
            {
                Console.WriteLine("Invalid account number!");
            }
        }
        public static void WithdrawMoney()
        {
            Console.WriteLine("\n-- Withdraw Money --");
            Console.Write("Enter account number:");
            string accountNumberInput = Console.ReadLine();
            if (int.TryParse(accountNumberInput, out int accountNumber))
            {
                BankAccount account = FindAccount(accountNumber);
                if (account != null)
                {
                    Console.Write("Enter withdrawal amount:");
                    string amountInput = Console.ReadLine();
                    if (double.TryParse(amountInput, out double amount) && amount > 0)
                    {
                        account.Withdraw(amount);
                    }
                    else
                    {
                        Console.WriteLine("Invalid withdrawal amount!");
                    }
                }
                else
                {
                    Console.WriteLine("Account not found!");
                }
            }
            else
            {
                Console.WriteLine("Invalid account number!");
            }
        }
        public static void CheckBalance()
        {
            Console.WriteLine("\n-- Check Balance --");
            Console.Write("Enter account number:");
            string accountNumberInput = Console.ReadLine();
            if (int.TryParse(accountNumberInput, out int accountNumber))
            {
                BankAccount account = FindAccount(accountNumber);
                if (account != null)
                {
                    Console.WriteLine("Current balance: " + account.Balance);
                }
                else
                {
                    Console.WriteLine("Account not found!");
                }
            }
            else
            {
                Console.WriteLine("Invalid account number!");
            }
        }
        public static void ViewAccountDetails()
        {
            Console.WriteLine("\n-- View Account Details --");
            Console.Write("Enter account number:");
            string accountNumberInput = Console.ReadLine();
            if (int.TryParse(accountNumberInput, out int accountNumber))
            {
                BankAccount account = FindAccount(accountNumber);
                if (account != null)
                {
                    account.DisplayAccountInfo();
                }
                else
                {
                    Console.WriteLine("Account not found!");
                }
            }
            else
            {
                Console.WriteLine("Invalid account number!");
            }
        }
        public static void ListAllAccounts()
        {
            Console.WriteLine("\n-- List All Accounts --");
            if (accountCount == 0)
            {
                Console.WriteLine("No accounts found!");
                return;
            }
            for (int i = 0; i < accountCount; i++)
            {
                accounts[i].DisplayAccountInfo();
                Console.WriteLine("-----------------------");
            }
        }
    }
}
