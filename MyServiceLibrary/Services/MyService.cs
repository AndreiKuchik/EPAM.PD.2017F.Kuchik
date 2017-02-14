using System;
using System.Collections.Generic;
using System.Linq;

namespace MyServiceLibrary
{  
    public delegate IEnumerable<User> GetSearch(string nameOrSurname);
    // Rename this class. Give the class an appropriate name that will allow all other developers understand it's purpose.
    public class MyService:IService
    {
        #region Private Filds
        private ICreateId _create;
#endregion

        #region ctors 
        public MyService()
        {
            
        }

        public MyService(ICreateId create)
        {
            _create = create;
        }
#endregion

        #region Public methods 

        /// <summary>
        /// Add user in ListUser
        /// </summary>
        /// <param name="user"></param>
        /// <returns>result of actions</returns>
        public bool Add(User user)
        {
            if (user == null)
            {
                return false;
            }
            else
            {
                 user.Id = _create.GenerateId();
                 return ListUser.Instance.Add(user);
            }
           
        }

        /// <summary>
        ///remove user from listuser 
        /// </summary>
        /// <param name="user"></param>
        /// <returns>result of actions</returns>
        public bool Remove(User user)
        {
            if (user == null)
            {
                return false;
            }
            return ListUser.Instance.GetUsers().Remove(user);
        }

        /// <summary>
        /// Get UserList
        /// </summary>
        /// <param name="search"> reference on method</param>
        /// <param name="nameOrSurname"></param>
        /// <returns>UserList</returns>
        public System.Collections.Generic.IEnumerable<User> SearchUser(GetSearch search, string nameOrSurname)
        {
            if (search == null || String.IsNullOrEmpty(nameOrSurname))
            {
                return null;
            }
            return search.Invoke(nameOrSurname);
        }

        /// <summary>
        /// finds  users by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns> UserList</returns>
        public IEnumerable<User> SearchByName(string name)
        {
            foreach (var user in ListUser.Instance.GetUsers())
            {
                if (user.FirstName == name)
                {
                    yield return user;
                }
            }
        }

        /// <summary>
        /// finds  users by Surname
        /// </summary>
        /// <param name="surname"></param>
        /// <returns> UserList</returns>
        public IEnumerable<User> SearchBySurname(string surname)
        {
            foreach (var user in ListUser.Instance.GetUsers())
            {
                if (user.LastName == surname)
                {
                    yield return user;
                }
            }
        }

#endregion


    }
}
