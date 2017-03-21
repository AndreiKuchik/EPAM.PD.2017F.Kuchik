using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServiceLibrary.Exceptions
{
    [Serializable]
    public class UserIsExistException : Exception
    {
        public UserIsExistException()
        {
        }

        public UserIsExistException(string message)
            : base(message)
        {
        }

        public UserIsExistException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
