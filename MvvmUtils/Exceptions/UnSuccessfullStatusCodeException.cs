using System;
using System.Collections.Generic;
using System.Text;

namespace MvvmUtils.Exceptions
{
    public class UnSuccessfullStatusCodeException : Exception
    {
        public int StatusCode { get; set; }

        public UnSuccessfullStatusCodeException()
        {
        }

        public UnSuccessfullStatusCodeException(string message)
            : base(message)
        {
        }

        public UnSuccessfullStatusCodeException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
