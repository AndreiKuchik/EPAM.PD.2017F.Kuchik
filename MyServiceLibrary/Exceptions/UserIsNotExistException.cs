using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServiceLibrary.Exceptions
{
    [Serializable]
    public class UserIsNotExistException : Exception
    {
        public UserIsNotExistException()
        {
        }

        public UserIsNotExistException(string message)
            : base(message)
        {
        }

        public UserIsNotExistException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
