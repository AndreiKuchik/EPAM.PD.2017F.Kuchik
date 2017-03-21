using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServiceLibrary.CreateId
{
    public abstract class CreateId
    {
        public int CurrentId { get; set; }
        public abstract int GenerateId();
    }
}
