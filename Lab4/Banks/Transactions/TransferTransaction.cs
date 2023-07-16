using Banks.BankAccounts;
using Banks.Exceptions;

namespace Banks.Transactions
{
    public class TransferTransaction : Transaction
    {
        public TransferTransaction(Account account, decimal transactionSum, DateTime transactionTime, Account transactionDestination)
            : base(account, transactionSum, transactionTime, transactionDestination) { }

        public override void Complete()
        {
            if (TransactionDestination == null)
            {
                throw new ObjectNotFoundException();
            }

            Account.Balance -= TransactionSum;
            PostBalance -= TransactionSum;
            TransactionDestination.Balance += TransactionSum;
        }

        public override void Cancel()
        {
            if (IsCanceled)
            {
                throw new DoubleCancelationException();
            }

            if (TransactionDestination == null)
            {
                throw new ObjectNotFoundException();
            }

            Account.Balance += TransactionSum;
            PostBalance += TransactionSum;
            TransactionDestination.Balance -= TransactionSum;
            IsCanceled = true;
        }
    }
}
