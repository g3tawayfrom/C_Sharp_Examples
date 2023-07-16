using Banks.BankAccounts;
using Banks.Exceptions;

namespace Banks.Transactions
{
    public class TopUpTransaction : Transaction
    {
        public TopUpTransaction(Account account, decimal transactionSum, DateTime transactionTime)
            : base(account, transactionSum, transactionTime, null) { }

        public override void Complete()
        {
            Account.Balance += TransactionSum;
            PostBalance += TransactionSum;
        }

        public override void Cancel()
        {
            if (IsCanceled)
            {
                throw new DoubleCancelationException();
            }

            Account.Balance -= TransactionSum;
            PostBalance -= TransactionSum;
            IsCanceled = true;
        }
    }
}
