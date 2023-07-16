using Banks.BankAccounts;
using Banks.ClientService;
using Banks.Exceptions;
using Banks.Transactions;
using Xunit;

namespace Banks.Tests
{
    public class BankTest
    {
        private readonly CentralBank bankService;
        private readonly ClientRegistator clientRegistator;
        private readonly ClientBuilder clientBuilder;

        public BankTest()
        {
            bankService = CentralBank.GetInstance();
            clientRegistator = new ClientRegistator();
            clientBuilder = new ClientBuilder();
            clientRegistator.Builder = clientBuilder;
        }

        [Fact]
        public void TopUpTransaction_BalanceChangedCorrenctly()
        {
            Bank bank = bankService.RegisterNewBank(2, 20, 50, 8, 1);
            Client client = clientRegistator.BuildAdvancedClient("name", "surname", "123456");
            bank.AddNewClient(client);
            Account account = bank.CreateDebitAccountForClient(client, DateTime.Now);

            decimal preBalance = account.Balance;

            decimal sum = 10;
            account.TopUp(sum, DateTime.Now.AddDays(1));

            Assert.Equal(preBalance + sum, account.Balance);
        }

        [Fact]
        public void CancelTopUpTransaction_NoTransactionsAndBalanceReturned()
        {
            Bank bank = bankService.RegisterNewBank(2, 20, 50, 8, 1);
            Client client = clientRegistator.BuildAdvancedClient("name", "surname", "123456");
            bank.AddNewClient(client);
            Account account = bank.CreateDebitAccountForClient(client, DateTime.Now);

            decimal sum = 10;

            Transaction transaction = account.TopUp(sum, DateTime.Now.AddDays(1));

            Assert.Contains(transaction, account.TransactionsList);

            account.CancelTransaction(transaction.Id);

            Assert.True(account.TransactionsList.Count == 0);
        }

        [Fact]
        public void ChangeDepositPercent_BalanceChangedCorrectly()
        {
            Bank bank = bankService.RegisterNewBank(2, 20, 50, 8, 1);

            var interestRate = new InterestRate(20, 5);
            bank.AddNewInterestRate(interestRate);

            Client client = clientRegistator.BuildAdvancedClient("name", "surname", "123456");
            bank.AddNewClient(client);

            decimal preBalance = 15;
            decimal postBalance = preBalance + (2 * (preBalance * 5 / 36500));

            Account account = bank.CreateDepositAccountForClient(client, DateTime.Now, preBalance);

            bankService.MakeEmPay(DateTime.Now.AddDays(1));

            Assert.Equal(Math.Round(account.Balance, 5), Math.Round(postBalance, 5));
        }

        [Fact]
        public void WithdrawTransactionAndCreditCommission_BalanceChangedCorrectly()
        {
            Bank bank = bankService.RegisterNewBank(2, 20, 50, 8, 1);
            Client client = clientRegistator.BuildAdvancedClient("name", "surname", "123456");
            bank.AddNewClient(client);
            Account account = bank.CreateCreditAccountForClient(client, DateTime.Now);

            decimal preBalance = account.Balance;

            decimal sum = 10;
            account.Withdraw(sum, DateTime.Now.AddDays(1));

            Assert.Equal(preBalance - sum, account.Balance);

            decimal postBalance = account.Balance - bank.Commission;

            bankService.MakeEmPay(DateTime.Now.AddDays(1));

            Assert.Equal(Math.Round(account.Balance, 5), Math.Round(postBalance, 5));
        }

        [Fact]
        public void TransferTransaction_MoneyTransfered()
        {
            Bank bank = bankService.RegisterNewBank(2, 20, 50, 8, 1);
            Client client = clientRegistator.BuildAdvancedClient("name", "surname", "123456");
            Client client1 = clientRegistator.BuildAdvancedClient("name1", "surname1", "234567");
            bank.AddNewClient(client);
            bank.AddNewClient(client1);
            Account account = bank.CreateCreditAccountForClient(client, DateTime.Now);
            Account account1 = bank.CreateCreditAccountForClient(client1, DateTime.Now);

            account.TopUp(10, DateTime.Now.AddDays(1));

            decimal preBalance = account.Balance;
            decimal preBalance1 = account1.Balance;

            account.Transfer(2, account1, DateTime.Now.AddDays(2));

            Assert.Equal(account.Balance, preBalance - 2);

            Assert.Equal(account1.Balance, preBalance1 + 2);
        }

        [Fact]
        public void CheckUnverifiedClient_LimitsAreWorking()
        {
            Bank bank = bankService.RegisterNewBank(2, 20, 50, 8, 1);
            Client client = clientRegistator.BuildBasicClient("name", "surname");
            bank.AddNewClient(client);
            Account account = bank.CreateDebitAccountForClient(client, DateTime.Now);

            Assert.Throws<WrongSumException>(() => account.TopUp(21, DateTime.Now));
        }

        [Fact]
        public void CheckSubscriptionAndNotification_GotNotified()
        {
            Bank bank = bankService.RegisterNewBank(2, 20, 50, 8, 1);
            Client client = clientRegistator.BuildAdvancedClient("name", "surname", "123456");
            bank.AddNewClient(client);
            bank.CreateDebitAccountForClient(client, DateTime.Now);

            client.SubcribeToBanksNotification(bank.Id);

            Assert.Contains(bank.Id, client.SubscriptionsList);

            Assert.True(client.NotificationsList.Count == 0);

            bank.ChangeLimit(22);

            Assert.True(client.NotificationsList.Count == 1);
        }

        [Fact]
        public void CheckCreditLimit_CantGoDownIt()
        {
            Bank bank = bankService.RegisterNewBank(2, 20, 50, 8, 1);
            Client client = clientRegistator.BuildAdvancedClient("name", "surname", "123456");
            bank.AddNewClient(client);
            Account account = bank.CreateCreditAccountForClient(client, DateTime.Now);

            Assert.Throws<WrongSumException>(() => account.Withdraw(bank.CreditLimit + 1, DateTime.Now));
        }
    }
}