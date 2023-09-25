
 
using SQLite4Unity3d;
using UnityEngine;

namespace Scheme
{
    /// <summary>
    /// Defines the <see cref="User" />.
    /// </summary>
   
    [System.Serializable]
    public class User 
    {

        [PrimaryKey, AutoIncrement]
        public int Id { get => this._id; set=>this._id = value; }

        [SerializeField]
        int _id;


        [Unique]
        public string UserName { get=>this._UserName; set=>this._UserName = value; }
        [SerializeField]
        string _UserName;

        public string Password { get => this._Password; set=> this._Password = value; }
        [SerializeField] 
        string _Password;
        public string Email { get => this._Email; set => this._Email = value; }
        [SerializeField] 
        string _Email;

        public string FullName { get => this._FullName; set => this._FullName = value; }
        [SerializeField] 
        string _FullName;
        public string Age { get => this._Age; set => this._Age = value; }
        [SerializeField] 
        string _Age;

        public System.DateTime Created { get => this._Created; set => this._Created = value; }
        [SerializeField] 
        System.DateTime _Created;


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
