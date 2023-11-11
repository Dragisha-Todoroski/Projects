using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoGameLibraryApp.Exceptions.CustomExceptions
{
    public class DuplicateVideoGameTitleException : ArgumentException
    {
        public DuplicateVideoGameTitleException() : base() { }
        public DuplicateVideoGameTitleException(string? message) : base(message) { }
        public DuplicateVideoGameTitleException(string? message, Exception? innerException) : base(message, innerException) { }
    }
}
