﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopStore.Infrastructure.Persistence.Exceptions
{
    public  class EFConcurrencyException:Exception
    {
        public EFConcurrencyException(string message=""):base(message)
        {
            
        }
    }
}
