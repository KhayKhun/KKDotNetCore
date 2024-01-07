using System;
using System.Net.Sockets;

public class cardHolder
{
    String cardNum;
    String pin;
    String firstName;
    String lastName;
    double balance;

    public cardHolder(String cardNum, String pin, String firstName, String lastName, double balance)
    {
        this.cardNum = cardNum;
        this.pin = pin;
        this.firstName = firstName;
        this.lastName = lastName;
        this.balance = balance;
    }

    public String getCardNum()
    {
        return cardNum;
    }
    public String getFirstName()
    {
        return firstName;
    }
    public String getLastName()
    {
        return lastName;
    }
    public double getBalance()
    {
        return balance;
    }
    public String getPin()
    {
        return pin;
    }


    public void setCardNum(String cn)
    {
        cardNum = cn;        
    }
    public void setFirstName(String fn)
    {
        firstName = fn;
    }
    public void setLastName(String ln)
    {
        lastName = ln;
    }
    public void setBalance(double b)
    {
        balance = b;
    }
    public void setPin(String p)
    {
        pin = p;
    }

    public static void Main(String[] args)
    {
        void printOptions()
        {
            Console.WriteLine("Please choose one of the following options...");
            Console.WriteLine("1. Deposit");
            Console.WriteLine("2. Withdraw");
            Console.WriteLine("3. Show Balance");
            Console.WriteLine("4. Exit");
        }

        void deposit(cardHolder currentUser)
        {
            Console.WriteLine("How much && would you like to deposit? ");
            double deposit = Double.Parse(Console.ReadLine());
            currentUser.setBalance(currentUser.getBalance() + deposit);
            Console.WriteLine($"Thank you for your $$. Your new balance is {currentUser.getBalance()}");
        }

        void withdraw(cardHolder currentUser)
        {
            Console.WriteLine("How much && would you like to withdraw: ");
            double withdraw = Double.Parse(Console.ReadLine());

            if(currentUser.getBalance() < withdraw)
            {
                Console.WriteLine("Not enough money!");
            }
            else
            {
                currentUser.setBalance(currentUser.getBalance() - withdraw);
                Console.WriteLine("Withdrawing success");
                Console.WriteLine($"Your new balance is {currentUser.getBalance()}");

            }
        }

        void balance(cardHolder currentUser)
        {
            Console.WriteLine($"Current balance: {currentUser.getBalance()}");
        }

        List<cardHolder> list = new List<cardHolder>();
        list.Add(new cardHolder("1212121212121212", "1234", "Harley","Khun", 2001.50));
        list.Add(new cardHolder("1212121212121213", "1234", "Poppy","Yung", 989.00));
        list.Add(new cardHolder("1212121212121214", "1234", "Nicky","Cole", 40010.50));
        list.Add(new cardHolder("1212121212121215", "1234", "John","Wink", 100.99));

        Console.WriteLine("Welcome to simple ATM");
        Console.WriteLine("Please insert your debit card");
        String debitCardNum = "";
        cardHolder currentUser;

        while(true)
        {
            try
            {
                debitCardNum = Console.ReadLine();

                currentUser = list.FirstOrDefault(x => x.cardNum == debitCardNum);
                if(currentUser != null)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Card not reconized. Please try again");
                }
            }
            catch
            {
                Console.WriteLine("Card not reconized. Please try again");
            }
        }
        Console.WriteLine("Please enter your pin");

        String pin;
        while (true)
        {
            try
            {
                pin = Console.ReadLine();

                if (currentUser.getPin() == pin)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Incorrect pin. Please try again.");
                }
            }
            catch
            {
                Console.WriteLine("Incorrect pin. Please try again.");
            }
        }

        Console.WriteLine($"Welcome {currentUser.getFirstName()}");

        String option = "0";

        do
        {
            printOptions();
            option = Console.ReadLine();
            if(option == "1") { deposit(currentUser); }
            else if(option == "2") { withdraw(currentUser); }
            else if(option == "3") { balance(currentUser); }
            else if(option == "4") { break; }
            else { option = "0"; }
            
        }
        while (option != "4");
        Console.WriteLine("Thank you byeeeee");
    }
}