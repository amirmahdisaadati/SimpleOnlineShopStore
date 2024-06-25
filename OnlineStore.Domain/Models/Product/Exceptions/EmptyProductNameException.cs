using OnlineShopStore.Domain.DomainModel.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopStore.Domain.DomainModel.Models.Product.Exceptions
{
    public class EmptyProductNameException : System.Exception, IDomainException
    {
        public EmptyProductNameException(string message = "نام محصول نمیتواند خالی باشد"):base(message)
        {

        }
    }
}
