using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServiceLibrary.Services
{
    public interface IMasterServce:IService,IStateOfStorag
    {
        event EventHandler<UserEventArgs> AddNewUser;
        event EventHandler<UserEventArgs> RemoveUser;
     
        
    }
}
