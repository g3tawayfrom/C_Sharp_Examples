using Banks.BankAccounts;
using Banks.Exceptions;
using Banks.Transactions;

namespace Banks
{
    public class Bank
    {
        public Bank(decimal cashbackPercent, int limit, int creditLimit, decimal extraPercent, decimal commission)
        {
            Id = Guid.NewGuid();
            CashbackPercent = cashbackPercent;
            Limit = limit;
            CreditLimit = creditLimit;
            ExtraPercent = extraPercent;
            Commission = commission;
            InterestRatesList = new List<InterestRate>();
            AccountsList = new List<Account>();
            ClientsList = new List<Client>();
        }

        public Guid Id { get; }
        public decimal CashbackPercent { get; set; }
        public decimal Limit { get; set; }
        public decimal CreditLimit { get; set; }
        public decimal ExtraPercent { get; }
        public decimal Commission { get; set; }
        public List<InterestRate> InterestRatesList { get; }
        public List<Account> AccountsList { get; }
        public List<Client> ClientsList { get; }

        public void AddNewClient(Client client)
        {
            Client? client_temp = ClientsList.FirstOrDefault(c => c.Equals(client));
            if (client_temp != null)
            {
                throw new ObjectAlreadyExistsException();
            }

            ClientsList.Add(client);
        }

        public CreditAccount CreateCreditAccountForClient(Client client, DateTime creationTime)
        {
            CreditAccount creditAccount;

            int number = AccountsList.Count() + 1;

            if (client.PassportId == null)
            {
                creditAccount = new CreditAccount(client, number, creationTime, CreditLimit, Limit);
                AccountsList.Add(creditAccount);
                return creditAccount;
            }

            creditAccount = new CreditAccount(client, number, creationTime, CreditLimit);
            AccountsList.Add(creditAccount);
            return creditAccount;
        }

        public DebitAccount CreateDebitAccountForClient(Client client, DateTime creationTime)
        {
            DebitAccount debitAccount;

            int number = AccountsList.Count() + 1;

            if (client.PassportId == null)
            {
                debitAccount = new DebitAccount(client, number, creationTime, Limit);
                AccountsList.Add(debitAccount);
                return debitAccount;
            }

            debitAccount = new DebitAccount(client, number, creationTime);
            AccountsList.Add(debitAccount);
            return debitAccount;
        }

        public DepositAccount CreateDepositAccountForClient(Client client, DateTime creationTime, decimal sum)
        {
            DepositAccount depositAccount;

            int number = AccountsList.Count() + 1;

            if (client.PassportId == null)
            {
                depositAccount = new DepositAccount(client, number, creationTime, (decimal)sum, creationTime.AddDays(1), Limit);
                AccountsList.Add(depositAccount);
                return depositAccount;
            }

            depositAccount = new DepositAccount(client, number, creationTime, (decimal)sum, creationTime.AddDays(1));
            AccountsList.Add(depositAccount);
            return depositAccount;
        }

        public void NotifySubsribers(string message)
        {
            var subscribedClients = ClientsList.Where(c => c.SubscriptionsList.Contains(Id)).ToList();

            foreach (Client subscribedClient in subscribedClients)
            {
                subscribedClient.NotificationsList.Add(new Notification(message));
            }
        }

        public void ChangeCashbackPersent(decimal cashbackPercent)
        {
            CashbackPercent = cashbackPercent;
            NotifySubsribers("Cashback percent was changed");
        }

        public void ChangeLimit(decimal limit)
        {
            Limit = limit;
            NotifySubsribers("Limit was changed");
        }

        public void ChangeCreditLimit(decimal creditLimit)
        {
            CreditLimit = creditLimit;
            NotifySubsribers("Credit limit was changed");
        }

        public void ChangeCommission(decimal commission)
        {
            Commission = commission;
            NotifySubsribers("Commission was changed");
        }

        public void AddNewInterestRate(InterestRate interestRate)
        {
            InterestRatesList.Add(interestRate);
            NotifySubsribers("We got new interest rate");
        }

        public void ChargeInterestOrCommissionPerPeriod(DateTime currentDate)
        {
            foreach (Account account in AccountsList)
            {
                GetInterestOrCommissionPerPeriod(account, currentDate);
                account.ChargeTemp(currentDate);
                account.LastInteractionTime = currentDate;
                account.TempMoney = 0;
            }
        }

        public void GetInterestOrCommissionPerPeriod(Account account, DateTime currentDate)
        {
            decimal percent;

            if (account.AccountType == AccountType.Deposit)
            {
                if (account.TransactionsList.Count() >= 1)
                    percent = GetDepositPercent(account.TransactionsList[0].PreBalance);
                else
                    percent = GetDepositPercent(account.Balance);
            }
            else
            {
                percent = CashbackPercent;
            }

            for (DateTime date = account.LastInteractionTime.Date; date <= currentDate.Date; date = date.AddDays(1))
            {
                switch (account.AccountType)
                {
                    case AccountType.Credit:
                        var transactionsList = account.TransactionsList.Where(t => t.TransactionTime.Date <= date).ToList();
                        if (transactionsList == null)
                            continue;

                        foreach (Transaction transaction in transactionsList)
                        {
                            decimal balance = transaction.PostBalance;

                            if (transaction.PostBalance < 0)
                            {
                                account.TempMoney += Commission;
                            }
                        }

                        break;
                    case AccountType.Debit:
                    case AccountType.Deposit:
                        account.TempMoney += account.Balance * (percent / 36500);
                        break;
                }
            }
        }

        private decimal GetDepositPercent(decimal sum)
        {
            foreach (InterestRate interestRate in InterestRatesList)
            {
                if (sum < interestRate.UpperLimit)
                    return interestRate.Percent;
            }

            return ExtraPercent;
        }
    }
}
