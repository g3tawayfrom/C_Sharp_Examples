using Shops.Exceptions;

namespace Shops.Entities
{
    public class Container
    {
        public Container(Product info)
        {
            Info = info;
            Price = 0;
            Amount = 0;
        }

        public Product Info { get; }

        public decimal Price { get; private set; }

        public int Amount { get; set; }

        public void SetPrice(decimal price)
        {
            if (price <= 0)
            {
                var exception = IncorrectFormException.Price();
                throw exception;
            }

            Price = price;
        }
    }
}
