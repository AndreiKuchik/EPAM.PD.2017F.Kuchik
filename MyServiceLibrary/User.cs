using System;

namespace MyServiceLibrary
{
    public class User
    {
        public User()
        {
            
        }
        public User(string fn, string ln)
        {
            FirstName = fn;
            LastName = ln;
        }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
    }
}
