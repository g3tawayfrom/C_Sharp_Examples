using Banks.ClientService;

namespace Banks.CNSL
{
    public class Program
    {
        public static void Main()
        {
            var centralBank = CentralBank.GetInstance();

            var clientRegistator = new ClientRegistator();
            var clientBuilder = new ClientBuilder();
            clientRegistator.Builder = clientBuilder;

            Bank bank = centralBank.RegisterNewBank(4, 20, 50, 8, 2);
            Client client = clientRegistator.BuildAdvancedClient("Georgii", "Novikov", "123456");

            while (true)
            {
                Console.WriteLine("Choose an option: " + "1 - create new account, " + "2 - transfer money, " +
                    "3 - withdraw money, " + "4 - top up account, " + "5 - exit console.");

                switch (Console.ReadLine())
                {
                    case "1":
                        Console.WriteLine("Choose type of account: " + "1 - credit account, " +
                            "2 - debit account, " + "3 - deposit account.");

                        switch (Console.ReadLine())
                        {
                            case "1":
                                bank.CreateCreditAccountForClient(client, DateTime.Now);
                                break;
                            case "2":
                                bank.CreateDebitAccountForClient(client, DateTime.Now);
                                break;
                            case "3":
                                decimal.TryParse(Console.ReadLine(), out decimal sum);
                                bank.CreateDepositAccountForClient(client, DateTime.Now, sum);
                                break;
                        }

                        Console.WriteLine("Account created");
                        break;
                    case "2":
                        Console.WriteLine("Choose outcoming account and incoming account: ");

                        int.TryParse(Console.ReadLine(), out int outcomingAcc);
                        int.TryParse(Console.ReadLine(), out int incomingAcc);

                        Console.WriteLine("Choose transfer sum: ");
                        decimal.TryParse(Console.ReadLine(), out decimal transferSum);

                        bank.AccountsList[outcomingAcc - 1].Transfer(transferSum, bank.AccountsList[incomingAcc - 1], DateTime.Now);
                        Console.WriteLine(bank.AccountsList[outcomingAcc - 1].Balance);
                        Console.WriteLine(bank.AccountsList[incomingAcc - 1].Balance);
                        Console.WriteLine("Transfer done");
                        break;
                    case "3":
                        Console.WriteLine("Choose withdraw account: ");

                        int.TryParse(Console.ReadLine(), out int withdrawAcc);

                        Console.WriteLine("Choose withdraw sum: ");
                        decimal.TryParse(Console.ReadLine(), out decimal withdrawSum);

                        bank.AccountsList[withdrawAcc - 1].Withdraw(withdrawSum, DateTime.Now);
                        Console.WriteLine(bank.AccountsList[withdrawAcc - 1].Balance);
                        Console.WriteLine("Withdraw done");
                        break;
                    case "4":
                        Console.WriteLine("Choose top up account: ");

                        int.TryParse(Console.ReadLine(), out int topuUpAcc);

                        Console.WriteLine("Choose top up sum: ");
                        decimal.TryParse(Console.ReadLine(), out decimal topUpSum);

                        bank.AccountsList[topuUpAcc - 1].TopUp(topUpSum, DateTime.Now);
                        Console.WriteLine(bank.AccountsList[topuUpAcc - 1].Balance);
                        Console.WriteLine("Top up done");
                        break;
                    case "5":
                        return;
                }
            }
        }
    }
}