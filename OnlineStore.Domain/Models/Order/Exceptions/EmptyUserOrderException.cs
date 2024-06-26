using OnlineShopStore.Domain.DomainModel.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopStore.Domain.DomainModel.Models.Order.Exceptions
{
    public  class EmptyUserOrderException:System.Exception, IDomainException
    {
        public EmptyUserOrderException(string message="وجود کاربر برای ثبت سفارش الزامی است"):base(message)
        {
            
        }
    }
}
