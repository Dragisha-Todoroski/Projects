using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoGameLibraryApp.Exceptions.CustomExceptions
{
    public class VideoGameNotFoundException : ArgumentException
    {
        public VideoGameNotFoundException() : base() { }
        public VideoGameNotFoundException(string? message) : base(message) { }
        public VideoGameNotFoundException(string? message, Exception? innerException) : base(message, innerException) { }
    }
}
