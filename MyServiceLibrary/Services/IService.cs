using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MyServiceLibrary.Services
{
    public interface IService
    {

        void Add(User user);
        void Remove(User user);
        IEnumerable<User> Search(Predicate<User> searchObject);
       

    }
}
