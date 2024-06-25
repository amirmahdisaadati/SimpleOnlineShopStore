using OnlineStore.Domain.DomainModel.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain.DomainModel.Models.Product.Exceptions
{
    public  class EmptyProductNameException:System.Exception, IDomainException
    {
        public EmptyProductNameException(string message="نام محصول نمیتواند خالی باشد")
        {
                
        }
    }
}
