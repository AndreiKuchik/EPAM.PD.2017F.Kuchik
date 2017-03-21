using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServiceLibrary.Services
{
     [Serializable]
     public class UserEventArgs: EventArgs
    {
         public UserEventArgs()
         {
             
         }

         public UserEventArgs(User _user)
         {
             user = _user;
         }
         public UserEventArgs(IEnumerable<User> users)
         {
             Users = new List<User>(users);
         }

         public List<User> Users { get; set; }
         public User user { get; set; }
    }
}
