using Shops.Exceptions;

namespace Shops.Entities
{
    public class Product : EntityBase
    {
        public Product(string name)
            : base()
        {
            if (!Validation(name))
            {
                var exception = IncorrectFormException.Name();
                throw exception;
            }

            Name = name;
        }

        public string Name { get; }

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
