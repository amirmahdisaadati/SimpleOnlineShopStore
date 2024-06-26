using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShopStore.Domain.DomainModel.Exception;

namespace OnlineShopStore.Domain.DomainModel.Models.Order.Exceptions
{
    public class EmptyProductOrderException:System.Exception,IDomainException
    {
        public EmptyProductOrderException(string message="برای ثبت سفارش وجود یک محصول الزامی است") :base(message)       {
            
        }
    }
}
