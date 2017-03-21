using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServiceLibrary.CreateId
{
    public class CreateSecondMethodId:CreateId
    {

        public new int CurrentId { get; set; }

        public CreateSecondMethodId():this(0)
        {
            
        }

        public CreateSecondMethodId(int currentId)
        {
            CurrentId = currentId;
        }

        /// <summary>
        /// Generate Id second of method
        /// </summary>
        /// <returns></returns>
        public override int GenerateId()
        {
           
            return CurrentId=+2;
        }
    }
}
