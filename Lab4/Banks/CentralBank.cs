namespace Banks
{
    public class CentralBank
    {
        private static CentralBank? instance;

        private List<Bank> banksList = new List<Bank>();
        private CentralBank() { }

        public static CentralBank GetInstance()
        {
            if (instance == null)
            {
                instance = new CentralBank();
            }

            return instance;
        }

        public Bank RegisterNewBank(decimal cashbackPercent, int limit, int creditLimit, decimal extraPercent, decimal commission)
        {
            var bank = new Bank(cashbackPercent, limit, creditLimit, extraPercent, commission);
            banksList.Add(bank);

            return bank;
        }

        public void MakeEmPay(DateTime dateTime)
        {
            foreach (Bank bank in banksList)
            {
                bank.ChargeInterestOrCommissionPerPeriod(dateTime);
            }
        }
    }
}
