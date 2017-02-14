using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServiceLibrary.CreateId
{
    public class CreateFirstMethodId:ICreateId
    {

        /// <summary>
        /// Generate Id second of method
        /// </summary>
        /// <returns></returns>
        public int GenerateId()
        {
            int newId;
            if (ListUser.Instance.GetUsers().Count == 0)
            {
                return 1;
            }
            newId = ListUser.Instance.GetUsers().Last().Id;
            newId ++;
            return newId;
        }
    }
}
