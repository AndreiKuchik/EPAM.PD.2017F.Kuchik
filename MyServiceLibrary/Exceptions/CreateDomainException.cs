using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServiceLibrary.Exceptions
{
     [Serializable]
   public class CreateDomainException : Exception
    {
        public CreateDomainException()
        {
        }

        public CreateDomainException(string message)
            : base(message)
        {
        }

        public CreateDomainException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
