using OnlineShopStore.Domain.DomainModel.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopStore.Domain.DomainModel.Models.User.Exeptions
{
    public class EmptyUserNameException : System.Exception, IDomainException
    {
        public EmptyUserNameException(string message = "نام کاربر نمیتواند خالی باشد")
        {

        }

    }
}
