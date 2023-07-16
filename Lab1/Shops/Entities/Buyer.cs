using Shops.Exceptions;

namespace Shops.Entities
{
    public class Buyer : EntityBase
    {
        public Buyer(string name)
            : base()
        {
            if (!Validation(name))
            {
                var exception = IncorrectFormException.Name();
                throw exception;
            }
            else
            {
                Name = name;
            }

            Money = 0;
        }

        public string Name { get; }

        public decimal Money { get; private set; }

        public void IncreaseBalance(decimal money)
        {
            if (money <= 0)
            {
                var exception = IncorrectFormException.Sum();
                throw exception;
            }

            Money += money;
        }

        public void DecreaseBalance(decimal money)
        {
            if (Money - money < 0)
            {
                var exception = NotEnoughSomethingException.Money();
                throw exception;
            }

            Money -= money;
        }

        public bool Validation(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return false;
            }
            else
            {
                if (name.Length < 3)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
    }
}
