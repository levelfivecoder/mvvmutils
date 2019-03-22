using System;
using System.Collections.Generic;
using System.Text;

namespace MvvmUtils.Exceptions
{
    public class BaseUrlNotInitalizedException : Exception
    {
        public int StatusCode { get; set; }

        public BaseUrlNotInitalizedException()
        {
        }

        public BaseUrlNotInitalizedException(string message)
            : base(message)
        {
        }

        public BaseUrlNotInitalizedException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
