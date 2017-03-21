using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServiceLibrary.Connection
{
     [Serializable]
    public class Message
    {
        public User user { get; set; }
        public string TypeOfMessage { get; set; }
        public Message()
        {
         
        }
        public Message(User _user, string _type)
        {
            user = _user;
            TypeOfMessage = _type;
        }
          public List<User> ListUsers { get; set; }
     
        public Message(List<User> listUsers, string mess)
        {
            ListUsers = listUsers;
            TypeOfMessage = mess;
        }
    }
}
