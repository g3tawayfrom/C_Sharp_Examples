using Shops.Exceptions;

namespace Shops.Entities
{
    public class Shop : EntityBase
    {
        private readonly List<Container> products;

        public Shop(string name, string address)
            : base()
        {
            if (!NameValidation(name))
            {
                var exception = IncorrectFormException.Name();
                throw exception;
            }
            else
            {
                Name = name;
            }

            if (!AddressValidation(address))
            {
                var exception = IncorrectFormException.Address();
                throw exception;
            }
            else
            {
                Address = address;
            }

            products = new List<Container>();
        }

        public string Name { get; }

        public string Address { get; }

        public void AddProduct(Container product)
        {
            if (products.Contains(product))
            {
                throw new ProductAlreadyExistsException();
            }

            products.Add(product);
        }

        public List<Container> GetProductList()
        {
            return products;
        }

        public bool NameValidation(string name)
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

        public bool AddressValidation(string address)
        {
            if (string.IsNullOrWhiteSpace(address))
            {
                return false;
            }
            else
            {
                if (address.Length < 7)
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
