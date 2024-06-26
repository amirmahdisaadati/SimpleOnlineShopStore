using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShopStore.Domain.DomainModel.Models.Product;

namespace OnlineShopStore.Test.Utils.Utils
{
    public class ProductBuilder
    {
        private string _title = "Test Product";
        private decimal _price = 100;
        private decimal _discount = 20;

        private int _inventoryCount = 500;



        public ProductBuilder WithTitle(string title)
        {
            _title = title;
            return this;
        }

        public ProductBuilder WithPrice(decimal price) { _price = price; return this; }
        public ProductBuilder WithDiscount(decimal discount) { _discount = discount; return this; }

        public ProductBuilder WithInventoryCount(int inventoryCount)
        {
            _inventoryCount = inventoryCount;
            return this;
        }
        public Product Build()
        {
            return new Product(_title, _price, _discount, _inventoryCount);
        }
    }
}
