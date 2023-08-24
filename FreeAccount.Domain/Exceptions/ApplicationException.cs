using System;

namespace FreeAccount.Domain.Exceptions
{
    public abstract class ApplicationException : Exception
    {
        protected ApplicationException(string title, string message) : base(message) => Title = title;

        public string Title { get; }
    }
}
