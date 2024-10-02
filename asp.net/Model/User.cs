using System.Diagnostics;

namespace Capstone.Model
{
    public class User
    {
        public string ID { get; private set; }
        public string PasswordHash { get; private set; }
        public string Name { get; private set; }
        public EUserType EUserType { get; private set; }

        public User(string id, string passwordHash, string name, EUserType eUserType)
        {
            Debug.Assert(name != null);
            Debug.Assert(id != null);
            Debug.Assert(passwordHash != null);

            Name = name;
            ID = id;
            PasswordHash = passwordHash;
            EUserType = eUserType;
        }

        private struct UserDTO()
        {
            public string Name { get; }
            public EUserType UserType { get; }

            public UserDTO(string name, EUserType userType)
                : this()
            {
                Name = name;
                UserType = userType;
            }
        }
    }
}
