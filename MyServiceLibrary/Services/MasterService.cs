using System;
using NLog;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using  MyServiceLibrary.CreateId;
using MyServiceLibrary.Services;
using MyServiceLibrary.Exceptions;

namespace MyServiceLibrary
{
     [Serializable]
    public class MasterService : MarshalByRefObject, IMasterServce
    {
        #region Private Filds

        private List<User> users; 
        private CreateId.CreateId _create;
        private readonly Logger logger = LogManager.GetCurrentClassLogger();
        private string pathXMLStore;
        private XmlSerializer formatter = new XmlSerializer(typeof(List<User>));
  

#endregion

        #region Public Events
        public event EventHandler<UserEventArgs> AddNewUser;
        public event EventHandler<UserEventArgs> RemoveUser;
#endregion

        #region ctors 
        public MasterService():this (new CreateFirstMethodId())
        {
            
        }

        public MasterService(CreateId.CreateId create)
            : this(create, (ConfigurationManager.AppSettings["XMLFilePath"]))
        {
          
            
        }
         public MasterService(CreateId.CreateId create, string pathXMLFile)
        {
             users = new List<User>();
             pathXMLStore = pathXMLFile;
             GetStateStorage();
             logger.Trace("Master created");


        }
       
      
#endregion

        #region Public methods 

        /// <summary>
        /// Add user in ListUser
        /// </summary>
        /// <param name="user"></param>
        /// <returns>result of actions</returns>
        public void Add(User user)
        {
            OnNewUser(new UserEventArgs(users));
            if (user == null)
            {
                throw new ArgumentNullException(); 
            }
            if (user.FirstName == null || user.LastName == null)
            {
                throw new UserInvalidException();
            }
            foreach (var us in users)
            {
                if (us.Equals(user))
                {
                    throw new UserIsExistException();
                }
            }
            user.Id = _create.GenerateId();
            users.Add(user);
            OnNewUser(new UserEventArgs(user));
            logger.Trace("User - {0} added", user);
               
        }
   
        /// <summary>
        ///remove user from listuser 
        /// </summary>
        /// <param name="user"></param>
        /// <returns>result of actions</returns>
        public void Remove(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException();
            }
            if (!users.Remove(user))
            {
                throw new UserIsNotExistException();
            }
            logger.Trace("User - {0} removed", user);
            OnRemoveUser(new UserEventArgs(user));
        }
        
        /// <summary>
        /// finds  users 
        /// </summary>
        /// <param name="surname"></param>
        /// <returns> UserList</returns>
        public IEnumerable<User> Search(Predicate<User> searchUser)
        {
            if (searchUser == null)
                throw new ArgumentNullException();
            return users.Where(user => searchUser(user)).ToList();
           
        }

        public void GetStateStorage()
        {
            using (FileStream fs = new FileStream(pathXMLStore, FileMode.OpenOrCreate))
            {
                users = (List<User>)formatter.Deserialize(fs);
            }
            _create = new CreateFirstMethodId(users.Last().Id);
           
            logger.Trace("Master watched state of storage");
        }
        public void SetStateStorage()
        {
            using (FileStream fs = new FileStream(pathXMLStore, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, users);
            }
            logger.Trace("Master update state of storage");
        }
#endregion

        #region Protected methods

        protected virtual void OnNewUser(UserEventArgs e)
        {
            AddNewUser(this, e);
        }
        protected virtual void OnRemoveUser(UserEventArgs e)
        {
            RemoveUser(this, e);
        }
#endregion
        
       
    }
}
