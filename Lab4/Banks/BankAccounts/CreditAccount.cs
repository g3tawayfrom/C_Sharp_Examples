using Banks.Exceptions;
using Banks.Transactions;

namespace Banks.BankAccounts
{
    public class CreditAccount : Account
    {
        public CreditAccount(Client owner, int id, DateTime creationTime, decimal creditLimit, decimal? limit = null)
            : base(owner, id, creationTime, limit)
        {
            AccountType = AccountType.Credit;
            CreditLimit = creditLimit;
        }

        public decimal CreditLimit { get; }

        public override Transaction TopUp(decimal sum, DateTime time)
        {
            if ((Limit != null && sum > Limit) || sum <= 0)
            {
                throw new WrongSumException();
            }

            var transaction = new TopUpTransaction(this, sum, time);
            transaction.Complete();

            TransactionsList.Add(transaction);
            return transaction;
        }

        public override Transaction Withdraw(decimal sum, DateTime time)
        {
            if (Limit != null && sum > Limit)
            {
                throw new WrongSumException();
            }

            if (Math.Abs(Balance - sum) > CreditLimit)
            {
                throw new WrongSumException();
            }

            var transaction = new WithdrawTransaction(this, sum, time);
            transaction.Complete();

            TransactionsList.Add(transaction);
            return transaction;
        }

        public override Transaction Transfer(decimal sum, Account transferPoint, DateTime time)
        {
            if (Limit != null && sum > Limit)
            {
                throw new WrongSumException();
            }

            if ((transferPoint.Limit != null && sum > transferPoint.Limit) || sum <= 0)
            {
                throw new WrongSumException();
            }

            if (Math.Abs(Balance - sum) > CreditLimit)
            {
                throw new WrongSumException();
            }

            var transaction = new TransferTransaction(this, sum, time, transferPoint);
            transaction.Complete();

            TransactionsList.Add(transaction);
            return transaction;
        }

        public override Transaction ChargeTemp(DateTime time)
        {
            var transaction = new CommissionTransaction(this, TempMoney, time);
            transaction.Complete();

            TransactionsList.Add(transaction);
            return transaction;
        }
    }
}
