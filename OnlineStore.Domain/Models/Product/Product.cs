using OnlineShopStore.Domain.DomainModel.Models.Product.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopStore.Domain.DomainModel.Models.Product
{
    public class Product
    {
        public Product(string title, decimal price, decimal discount, int inventoryCount)
        {
            ValidateProductTitle(title);
            ValidateProductPrice(price);

            Title = title;
            Price = price;
            Discount = discount;
            InventoryCount = inventoryCount;
        }
        public long Id { get; private set; }
        public string Title { get; private set; }
        public decimal Price { get; private set; }
        public decimal Discount { get; private set; }

        public int InventoryCount { get; private set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
        #region Methods

        public void IncreaseInventoryCount()
        {
            InventoryCount += 1;
        }

        public void DecreaseInventoryCount()
        {
            InventoryCount -= 1;
        }

        public void UpdateInventoryCount(int inventoryCount)
        {
            if (inventoryCount < 0)
                throw new InvalidProductInventoryCountException();
            InventoryCount = inventoryCount;
        }

        public decimal GetFinalPriceBasedOnDiscount()
        {
            return Price - Price * Discount / 100;
        }



        #endregion
        #region Validations

        private void ValidateProductTitle(string title)
        {
            if (string.IsNullOrEmpty(title))
                throw new EmptyProductNameException();
            if (title.Length >= 40)
                throw new InvalidProductNameException();
        }

        private void ValidateProductPrice(decimal price)
        {
            if (price == decimal.Zero)
                throw new InvalidProductPriceException();
        }

        #endregion
    }
}
