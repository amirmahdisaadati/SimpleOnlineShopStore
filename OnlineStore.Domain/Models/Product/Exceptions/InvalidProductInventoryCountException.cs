using OnlineStore.Domain.DomainModel.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain.DomainModel.Models.Product.Exceptions
{
    public class InvalidProductInventoryCountException:System.Exception, IDomainException
    {
        public InvalidProductInventoryCountException(string message="موجودی محصول نمیتواند کمتر از صفر باشد")
        {
            
        }
    }
}
