using Shops.Entities;
using Shops.Exceptions;

namespace Shops.Services
{
    public class ShopManager
    {
        private List<Shop> shopList = new List<Shop>();
        private List<Product> productList = new List<Product>();

        public Shop AddShop(string name, string address)
        {
            var shop = new Shop(name, address);
            shopList.Add(shop);

            return shop;
        }

        public Shop FindShop(Guid id)
        {
            Shop? shop = shopList.Where(s => s.Id.Equals(id)).SingleOrDefault();

            if (shop == null)
            {
                var exception = SomethingNotFoundException.Shop();
                throw exception;
            }
            else
            {
                return shop;
            }
        }

        public Product AddProduct(string name)
        {
            var product = new Product(name);
            productList.Add(product);

            return product;
        }

        public Product FindProduct_InGeneral(Guid id)
        {
            Product? product = productList.Where(s => s.Id.Equals(id)).SingleOrDefault();

            if (product == null)
            {
                var exception = SomethingNotFoundException.Product();
                throw exception;
            }
            else
            {
                return product;
            }
        }

        public void AddProduct_ToShop(Guid id, Product product)
        {
            Shop shop = FindShop(id);
            shop.GetProductList().Add(new Container(product));
        }

        public bool ContainsProduct(Shop shop, Guid id)
        {
            Container? product = shop.GetProductList().Where(s => s.Info.Id.Equals(id)).SingleOrDefault();

            if (product == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public Container GetProduct(Shop shop, Guid id)
        {
            Container? product = shop.GetProductList().Where(s => s.Info.Id.Equals(id)).SingleOrDefault();

            if (product == null)
            {
                var exception = NoSuchProductSomewhereException.InDirectShop();
                throw exception;
            }
            else
            {
                return product;
            }
        }

        public void OrderProduct(Shop shop, Guid id, int amount)
        {
            if (amount <= 0)
            {
                var exception = IncorrectFormException.Amount();
                throw exception;
            }

            if (!ContainsProduct(shop, id))
            {
                Product product_temp = FindProduct_InGeneral(id);
                AddProduct_ToShop(shop.Id, product_temp);
                Container product = GetProduct(shop, id);
                product.Amount = +amount;
            }
            else
            {
                Container product = GetProduct(shop, id);
                product.Amount = +amount;
            }
        }

        public void ChangeProductsPrice(Shop shop, Guid id, decimal price)
        {
            Container product = GetProduct(shop, id);
            product.SetPrice(price);
        }

        public List<Shop> FindBestDeal(Guid id, int amount)
        {
            if (amount <= 0)
            {
                var exception = IncorrectFormException.Amount();
                throw exception;
            }

            var shops_temp = shopList.Where(s => ContainsProduct(s, id) == true).OrderBy(s => GetProduct(s, id).Price).ToList();
            if (!shops_temp.Any())
            {
                var exception = NoSuchProductSomewhereException.InGeneral();
                throw exception;
            }

            var shops = new List<Shop>();
            int amount_temp = amount;

            foreach (Shop shop in shops_temp)
            {
                int stock = GetProduct(shop, id).Amount;

                if (stock < amount_temp)
                {
                    amount_temp -= stock;
                    shops.Add(shop);
                }
                else
                {
                    shops.Add(shop);
                    break;
                }

                if (shops.Last() == shop && amount_temp > 0)
                {
                    var exception = NotEnoughSomethingException.Amount();
                    throw exception;
                }
            }

            return shops;
        }

        public Buyer AddBuyer(string name)
        {
            var buyer = new Buyer(name);

            return buyer;
        }

        public void BuyProductDirectly(Buyer buyer, Shop shop, Guid id, int amount)
        {
            if (amount <= 0)
            {
                var exception = IncorrectFormException.Amount();
                throw exception;
            }

            if (!ContainsProduct(shop, id))
            {
                var exception = NoSuchProductSomewhereException.InGeneral();
                throw exception;
            }

            Container container = GetProduct(shop, id);

            if (container.Amount < amount)
            {
                var exception = NotEnoughSomethingException.Amount();
                throw exception;
            }

            decimal price = container.Price;

            buyer.DecreaseBalance(amount * price);
            container.Amount -= amount;
        }

        public void BuyProductOptimaly(Buyer buyer, Guid id, int amount)
        {
            if (amount <= 0)
            {
                var exception = IncorrectFormException.Amount();
                throw exception;
            }

            List<Shop> shops = FindBestDeal(id, amount);

            int amount_temp = amount;

            foreach (Shop shop in shops)
            {
                Container container = GetProduct(shop, id);

                int stock = container.Amount;
                decimal price = container.Price;

                if (stock < amount_temp)
                {
                    buyer.DecreaseBalance(stock * price);
                    container.Amount = 0;
                    amount_temp -= stock;
                }
                else
                {
                    buyer.DecreaseBalance(amount_temp * price);
                    container.Amount -= amount_temp;
                }
            }
        }
    }
}
