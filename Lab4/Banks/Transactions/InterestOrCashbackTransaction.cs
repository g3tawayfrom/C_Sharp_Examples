using Banks.BankAccounts;

namespace Banks.Transactions
{
    public class InterestOrCashbackTransaction : TopUpTransaction
    {
        public InterestOrCashbackTransaction(Account account, decimal profit, DateTime transactionTime)
            : base(account, profit, transactionTime) { }
    }
}
