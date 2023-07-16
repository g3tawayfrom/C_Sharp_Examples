using Shops.Entities;
using Shops.Exceptions;
using Shops.Services;
using Xunit;

namespace Shops.Test;

public class ShopManagerTest
{
    private readonly ShopManager manager;

    public ShopManagerTest()
    {
        manager = new ShopManager();
    }

    [Fact]
    public void SupplyProductToShop_ReadyToSell()
    {
        Shop shop = manager.AddShop("Super Mario", "Real Life street");
        Product product = manager.AddProduct("spaghetti");

        manager.AddProduct_ToShop(shop.Id, product);
        manager.OrderProduct(shop, product.Id, 10);
        manager.ChangeProductsPrice(shop, product.Id, 84);

        Assert.Equal(manager.GetProduct(shop, product.Id).Info, product);

        Assert.True(manager.GetProduct(shop, product.Id).Price == 84);

        Assert.True(manager.GetProduct(shop, product.Id).Amount == 10);
    }

    [Fact]
    public void SetAndChangePriceForProduct()
    {
        Shop shop = manager.AddShop("Super Mario Odyssey", "Princess Peach street");
        Product product = manager.AddProduct("pasta");

        manager.AddProduct_ToShop(shop.Id, product);
        manager.ChangeProductsPrice(shop, product.Id, 84);

        Assert.NotEqual(0, manager.GetProduct(shop, product.Id).Price);

        decimal old_price = manager.GetProduct(shop, product.Id).Price;

        manager.ChangeProductsPrice(shop, product.Id, 115);

        Assert.NotEqual(old_price, manager.GetProduct(shop, product.Id).Price);
    }

    [Fact]
    public void SetIncorrectPrice_ThrowException()
    {
        Shop shop = manager.AddShop("Run out of fantasy", "White noise street");
        Product product = manager.AddProduct("paper towel");

        manager.AddProduct_ToShop(shop.Id, product);

        Assert.Throws<IncorrectFormException>(() => manager.ChangeProductsPrice(shop, product.Id, -15));
    }

    [Fact]
    public void NoSuchProductInAnyShops_ThrowException()
    {
        Shop shop1 = manager.AddShop("101 shop", "Basic street");
        Shop shop2 = manager.AddShop("102 shop", "Basic street");
        Shop shop3 = manager.AddShop("103 shop", "Basic street");

        Product product = manager.AddProduct("Something stupid");

        Assert.Throws<NoSuchProductSomewhereException>(() => manager.FindBestDeal(product.Id, 1));
    }

    [Fact]
    public void NotEnoughAmount_ThrowException()
    {
        Shop shop1 = manager.AddShop("104 shop", "Basic street");
        Shop shop2 = manager.AddShop("105 shop", "Basic street");
        Shop shop3 = manager.AddShop("10 shop", "Basic street");

        Product product = manager.AddProduct("Something stupid 2: first one was very good");

        manager.AddProduct_ToShop(shop1.Id, product);
        manager.AddProduct_ToShop(shop2.Id, product);
        manager.AddProduct_ToShop(shop3.Id, product);

        Assert.Throws<NotEnoughSomethingException>(() => manager.FindBestDeal(product.Id, 1));
    }

    [Fact]
    public void BuySomeProduct_EnoughMoneyAndEnoughAmount()
    {
        Shop shop = manager.AddShop("404 shop", "Error street");
        Buyer buyer = manager.AddBuyer("Steve Jobs");
        Product product = manager.AddProduct("Ultrakill: Devil May Quake");

        manager.AddProduct_ToShop(shop.Id, product);
        manager.ChangeProductsPrice(shop, product.Id, 319);
        manager.OrderProduct(shop, product.Id, 99);

        buyer.IncreaseBalance(1000);

        int amount = 2;

        Assert.True(buyer.Money >= amount * manager.GetProduct(shop, product.Id).Price);

        Assert.True(manager.GetProduct(shop, product.Id).Amount >= amount);

        decimal money = buyer.Money;
        int inStock = manager.GetProduct(shop, product.Id).Amount;

        manager.BuyProductDirectly(buyer, shop, product.Id, amount);

        Assert.Equal(buyer.Money, money - (amount * manager.GetProduct(shop, product.Id).Price));

        Assert.Equal(manager.GetProduct(shop, product.Id).Amount, inStock - amount);
    }
}
