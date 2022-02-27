using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;

public class SQLiteExample : MonoBehaviour
{
    // Start is called before the first frame update
    private string dbName;
    

    void Start()
    {
        dbName = "URI=file:" + Application.persistentDataPath + "/Inventory.db";
        Debug.Log(dbName);
        CreateDB();
        AddUser("ali@ggf.com","somePassword","ali","ahmed");
        AddUser("nanny@ggf.com","somePassword1111","ahmed","ashraf");
        PrintUsers();
    }

    private void PrintUsers()
    {
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText ="SELECT * FROM score;";
                using(IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Debug.Log(
                            "UserInfoID =" + reader["UserInfoID"]+
                            "\tuserName=" + reader["userName"] +
                            "\tpassword=" + reader["password"] +
                            "\tfirstName=" + reader["firstName"] +
                            "\tlastName=" + reader["lastName"] +
                            "\tscore=" + reader["score"] +
                            "\tcreatedTime=" + reader["createdTime"] 

                            );
                    }
                }
            }
            connection.Close();
        }
    }

    

    private void AddUser(string userName, string password, string firstName, string lastName)
    {
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText =
                    "INSERT INTO score (userName, password, firstName, lastName ) VALUES (" +
                    userName.EncSingleQoute() + "," +
                    password.EncSingleQoute() + "," +
                    firstName.EncSingleQoute() + "," +
                    lastName.EncSingleQoute() +
                    ");";
                command.ExecuteNonQuery();
            }
            connection.Close();
        }
    }
     
    private void CreateDB()
    {
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "CREATE TABLE IF NOT EXISTS score ( " +
                    "UserInfoID INTEGER PRIMARY KEY AUTOINCREMENT," +
                    "userName TEXT," +
                    "password TEXT," +
                    "firstName TEXT," +
                    "lastName TEXT," +
                    "score INTEGER default 0," +
                    "createdTime TIMESTAMP DEFAULT CURRENT_TIMESTAMP"+

                    ");";
                command.ExecuteNonQuery();
            }
            connection.Close();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    
    
}
public static class Extension
{

    public static string EncSingleQoute(this string str)
    {
        return "'" + str + "'";
    }
 
}
