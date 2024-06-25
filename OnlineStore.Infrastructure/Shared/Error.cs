﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopStore.Infrastructure.Shared
{
    public class Error
    {
        public Error(string? message)
        {
            Message = message;
        }

        public string? Message { get; set; }
    }
}
