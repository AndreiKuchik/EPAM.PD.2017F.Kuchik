using System;
using System.Collections.Generic;
using MyServiceLibrary.Exceptions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using MyServiceLibrary.Connection;

namespace MyServiceLibrary.Services
{
     [Serializable]
    public class SlaveUserService :MarshalByRefObject, ISlaveService
    {
        #region Private Filds
        private List<User> users;
        private readonly Logger logger = LogManager.GetCurrentClassLogger();
#endregion

        #region ctor
        public SlaveUserService()
        {
            users = new List<User>();
            logger.Trace("Slave was created");
        }
#endregion

        #region  Public Methods
        public void Add(User user)
        {
            throw new AccessErorException();
        }

        public void Remove(User user)
        {
            throw new AccessErorException();
        }

        public IEnumerable<User> Search(Predicate<User> searchUser)
        {
             if (searchUser == null)
                throw new ArgumentNullException();
            return users.Where(user => searchUser(user)).ToList();
        }
        public void NotificationOnClient(ClientCommunication client)
        {
            client.AddNewUser += GetNotificationOfAdding;
            client.RemoveUser += GetNotificationOfRemoving;
        }
        public void GetNotificationOfAdding(object sender, UserEventArgs args)
        {
            if (args.Users != null)
            {
                users = args.Users;
            }
            else
            {
                 var user = args.user;
            if (user == null)
                throw new ArgumentNullException();
            users.Add(user);
            }
           
            logger.Trace("Notification of the user was received");
        }
        public void GetNotificationOfRemoving(object sender, UserEventArgs args)
        {
            var user = args.user;
            if (user == null)
                throw new ArgumentNullException();
            users.Remove(user);
            logger.Trace("Notification of the user {0} was received", user);
        }

#endregion
    }
}
