﻿using OnlineShopStore.Domain.DomainModel.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopStore.Domain.DomainModel.Models.Product.Exceptions
{
    public class InvalidProductNameException : System.Exception, IDomainException
    {
        public InvalidProductNameException(string message = "نام محصول بیشتر از 40 کاراکتر است"): base(message)
        {

        }
    }
}
