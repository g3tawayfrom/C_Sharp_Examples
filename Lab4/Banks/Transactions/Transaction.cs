using Banks.BankAccounts;

namespace Banks.Transactions
{
    public abstract class Transaction
    {
        public Transaction(Account account, decimal transactionSum, DateTime transactionTime, Account? transactionDestination = null)
        {
            Id = Guid.NewGuid();
            Account = account;
            TransactionSum = transactionSum;
            PreBalance = Account.Balance;
            PostBalance = Account.Balance;
            TransactionTime = transactionTime;
            TransactionDestination = transactionDestination;
        }

        public Guid Id { get; }
        public Account Account { get; }
        public decimal TransactionSum { get; set; }
        public decimal PreBalance { get; }
        public decimal PostBalance { get; set; }
        public decimal TempSum { get; set; }
        public bool IsCanceled { get; set; } = false;
        public DateTime TransactionTime { get; set; }
        public Account? TransactionDestination { get; }

        public abstract void Complete();

        public abstract void Cancel();
    }
}
