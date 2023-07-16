using Banks.Exceptions;
using Banks.Transactions;

namespace Banks.BankAccounts
{
    public abstract class Account
    {
        public Account(Client owner, int id, DateTime creationTime, decimal? limit = null)
        {
            Id = id;
            CreationTime = creationTime;
            LastInteractionTime = creationTime;
            Balance = 0;
            TempMoney = 0;
            Owner = owner;
            Limit = limit;
            TransactionsList = new List<Transaction>();
        }

        public int Id { get; }
        public AccountType AccountType { get; set; }
        public DateTime CreationTime { get; }
        public DateTime LastInteractionTime { get; set; }
        public decimal Balance { get; set; }
        public decimal TempMoney { get; set; }
        public Client Owner { get; }
        public decimal? Limit { get; }
        public List<Transaction> TransactionsList { get; }

        public abstract Transaction TopUp(decimal sum, DateTime time);

        public abstract Transaction Withdraw(decimal sum, DateTime time);

        public abstract Transaction Transfer(decimal sum, Account transferPoint, DateTime time);

        public abstract Transaction ChargeTemp(DateTime time);

        public void CancelTransaction(Guid transactionId)
        {
            Transaction? transaction = TransactionsList.FirstOrDefault(t => t.Id.Equals(transactionId));

            if (transaction == null)
            {
                throw new ObjectNotFoundException();
            }

            transaction.Cancel();
            TransactionsList.Remove(transaction);
        }
    }
}