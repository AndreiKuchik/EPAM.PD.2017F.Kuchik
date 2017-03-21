using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServiceLibrary.Exceptions
{
    [Serializable]
    public class UserInvalidException : Exception
    {
        public UserInvalidException ()
        {
        }

        public UserInvalidException (string message)
            : base(message)
        {
        }

        public UserInvalidException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
