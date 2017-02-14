using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MyServiceLibrary
{
    public interface IService
    {
        bool Add(User user);
        bool Remove(User user);
        IEnumerable<User> SearchBySurname(string surname);
        IEnumerable<User> SearchByName(string name);

    }
}
