using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServiceLibrary.Exceptions
{
     [Serializable]
    public class AccessErorException:Exception
    {
         public AccessErorException()
        {
        }

        public AccessErorException(string message)
            : base(message)
        {
        }

        public AccessErorException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
