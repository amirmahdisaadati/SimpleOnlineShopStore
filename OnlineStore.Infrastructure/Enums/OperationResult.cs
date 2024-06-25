using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Infrastructure.Enums
{
    public enum OperationResult : byte
    {
        NotFound = 0,
        Succeeded = 1,
        Failed = 2,
        NotValid = 3,
    }
}
