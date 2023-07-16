namespace Banks
{
    public class InterestRate
    {
        public InterestRate(int upperLimit, decimal percent)
        {
            UpperLimit = upperLimit;
            Percent = percent;
        }

        public int UpperLimit { get; }
        public decimal Percent { get; }
    }
}
