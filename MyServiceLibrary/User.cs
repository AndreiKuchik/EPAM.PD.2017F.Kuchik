using System;

namespace MyServiceLibrary
{
    [Serializable]
    public class User : IEquatable<User>, ICloneable
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
        public int GetHashCode()
        {
            return Id + FirstName.GetHashCode() + LastName.GetHashCode();
        }
        public bool Equals(User other)
        {
            if (other == null)
            {
                return false;
            }
            if (this.FirstName == other.FirstName && this.LastName == other.LastName)
            {
                return true;
            }
            return false;
        }

        object ICloneable.Clone()
        {
            return Clone();
        }
        private User Clone()
        {
            return new User { FirstName = this.FirstName, LastName = this.LastName, Id = this.Id};
        }
        public override string ToString()
        {
            return String.Format("Id - {0}, Firstname - {1}, LastName - {2}", Id, FirstName,LastName);
        }
    }
}
