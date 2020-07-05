using System;

namespace Domain.Exceptions.Order
{
    public class StatusNotAllowedException : Exception
    {
        public StatusNotAllowedException(string message) : base(message) { }
    }
}
