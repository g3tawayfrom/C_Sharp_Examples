using Banks.BankAccounts;

namespace Banks.Transactions
{
    public class CommissionTransaction : WithdrawTransaction
    {
        public CommissionTransaction(Account account, decimal commission, DateTime transactionTime)
            : base(account, commission, transactionTime) { }
    }
}
