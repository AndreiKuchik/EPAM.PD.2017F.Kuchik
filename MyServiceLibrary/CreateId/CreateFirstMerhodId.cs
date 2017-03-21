using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServiceLibrary.CreateId
{
    public class CreateFirstMethodId:CreateId
    {
        public new int CurrentId { get; set; }

        public CreateFirstMethodId():this(0)
        {
            
        }

        public CreateFirstMethodId(int currentId)
        {
            CurrentId = currentId;
        }

        /// <summary>
        /// Generate Id second of method
        /// </summary>
        /// <returns></returns>
        public override int GenerateId()
        {
           
            return ++CurrentId;
        }
    }
}
