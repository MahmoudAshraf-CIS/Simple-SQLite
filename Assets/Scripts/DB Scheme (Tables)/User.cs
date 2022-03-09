
using UnityEngine;
using SQLite4Unity3d;

namespace Scheme
{
    /// <summary>
    /// Defines the <see cref="User" />.
    /// </summary>
    public class User
    {

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [Unique]
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public string FullName { get; set; }
        public string Age { get; set; }

        public System.DateTime Created { get; set; }

        public override string ToString()
        {
            return string.Format("[Person: " +
                "Id={0}, " +
                "UserName ={1},  " +
                "Password={2}, " +
                "Email={3}, " +
                "FirstName={4}, " +
                "Surname={5}, " +
                "Age={6}]"
                , Id, UserName, Password, Email, FullName, Age);
        }
    }

}
