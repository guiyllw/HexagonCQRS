using System;

namespace Domain.Order.Exceptions
{
    public class AdvanceOrderException : Exception
    {
        public AdvanceOrderException(string message) : base(message) { }
    }
}
