using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServiceLibrary
{
    public class ListUser
    {
        #region Private Fields
        private static readonly ListUser instance = new ListUser();
        private readonly List<User> users;
        #endregion

        #region Constructors
        static ListUser()
        {
           
        }

        private ListUser()
        {
            users = new List<User>();
        }
        #       endregion

        #region Public Properties and Methods
        /// <summary>
        /// Provides instance of the ListUser
        /// </summary>
        public static ListUser Instance
        {
            get
            {
                return instance;
            }
        }

        /// <summary>
        /// Store a mark into the ListUser
        /// </summary>
        /// <param name="user">student</param>
      
        public bool Add(User user)
        {
            foreach (var _user in users)
            {
                if (user.FirstName == _user.FirstName && user.LastName==_user.LastName)
                {
                    return false;
                }
            }
            users.Add(user);
            return true;
           
        }

        /// <summary>
        /// Gets users from the ListUser
        /// </summary>
        public List<User> GetUsers()
        {

            return users;
        }
        # endregion

    }
}
