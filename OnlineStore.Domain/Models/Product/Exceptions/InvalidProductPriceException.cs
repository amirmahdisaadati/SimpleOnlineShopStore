using OnlineShopStore.Domain.DomainModel.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopStore.Domain.DomainModel.Models.Product.Exceptions
{
    public class InvalidProductPriceException : System.Exception, IDomainException
    {
        public InvalidProductPriceException(string message = "قیمت محصول نامعتبر است"): base(message)
        {

        }
    }
}
