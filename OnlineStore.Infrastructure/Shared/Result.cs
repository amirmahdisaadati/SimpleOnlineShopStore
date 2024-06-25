using OnlineStore.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Infrastructure.Shared
{
    public class Result
    {
        public Result(OperationResult operationResult)
        {
            OperationResult = operationResult;
        }

        public OperationResult OperationResult { get; set; }

        public string? Error { get; set; }
    }

    public class Result<T> : Result
    {
        public Result(OperationResult operationResult) : base(operationResult)
        {
        }

        public T? Data { get; set; }
    }
}
