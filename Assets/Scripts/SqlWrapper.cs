using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System.Linq;
using System.Reflection;
 
 

class student
{


    //INTEGER
    public bool mBool;
    public byte mByte;
    public sbyte msByte;
    public int mInt;
    public uint muInt;
    public long mLong;
    public ulong muLong;
    public short mShort;
    public ushort muShort;

    //REAL
    public decimal mDecimal;
    public double mDouble;
    public float mFloat;

    // TEXT
    public char mChar;
    public string mString;

    //TIMESTAMP
    public System.DateTime mDateTime;

    public student()
    {
        mBool = false;
        mByte = 1;
        msByte = 1;
        mInt = 1;
        muInt = 1;
        mLong = 1;
        muLong = 1;
        mShort = 1;
        muShort = 1;
        mDecimal = 1;
        mDouble = 1;
        mFloat = 1;
        mChar = 's';
        mString = "some shit";

        mDateTime = System.DateTime.Now;
    }
}
public class SqlWrapper : MonoBehaviour
{
    static SqliteConnection connection;
    static SqliteCommand command;
    // Start is called before the first frame update
    void Start()
    {    
        if (CreateDB("URI=file:" + Application.persistentDataPath + "/Inventory.db") == 0)
        {
            Debug.Log("Successfuly created the DB located at " + "URI=file:" + Application.persistentDataPath + "/Inventory.db");
        }
        else
        {
            Debug.LogError("Error : Unable to creat the DB located at " + "URI=file:" + Application.persistentDataPath + "/Inventory.db");
        }

        Debug.LogWarning(CreateTable(typeof(student)));
        Debug.LogWarning(InsertEntry(new student()));
        command.Dispose();
        connection.Dispose();

        
    }
    
    
    
    private void OnApplicationQuit()
    {
        Debug.Log("OnApplicationQuit");
      
    }

    public static int CreateDB(string dbName)
    {
  
        connection = new SqliteConnection(dbName);
        connection.Open();
        command = connection.CreateCommand();
        command.CommandText = "create table aTable(field1 int); drop table aTable;";
        

        return command.ExecuteNonQuery();

    } 
    public static int InsertEntry(object o)
    {
         
        System.Reflection.FieldInfo[] ffinfo = o.GetType().GetFields();
        string fields = "(" + string.Join(",",
                 o.GetType().GetFields().Select(field => field.Name).ToList() )
            + ")";

        
        
        
        string values = "('" + string.Join(",",
            o.GetType().GetFields().Select(fields => fields.GetValue(o)).ToList())
         + "')";
        values = values.Replace(",", "','");
        string commandTxt = "INSERT INTO " + o.GetType().Name + " " + fields + " VALUES " + values + ";";
        print(commandTxt);


        command.CommandText = commandTxt;
        //            "INSERT INTO score (userName, password, firstName, lastName ) VALUES (" +
        //            userName.EncSingleQoute() + "," +
        //            password.EncSingleQoute() + "," +
        //            firstName.EncSingleQoute() + "," +
        //            lastName.EncSingleQoute() +
        //            ");";

        // 1 - success , 0 - failed 
        return command.ExecuteNonQuery();
    }
    public static int CreateTable(System.Type t)
    {
        command.CommandText = "create table if not exists "+ t.Name + "( \n";
        
        System.Reflection.FieldInfo[] ffinfo = t.GetFields();
        foreach (var inf in ffinfo)
        {
            command.CommandText +=  inf.Name.ToString() + " " + TranslatoToCSharpType(inf.FieldType.ToString())
                + (inf == ffinfo[ffinfo.Length-1] ? "\n" : ",\n");
            
            // + "" + inf.Name + "\n";
        }
        command.CommandText += ");\n";
         
        //print(command.CommandText);
        return command.ExecuteNonQuery();

    }


    public static string TranslatoToCSharpType(string type)
    {
        string result = string.Empty;

        switch (type)
        {
            case "System.Boolean":
            case "System.Byte":
            case "System.SByte":
            case "System.Int32":
            case "System.UInt32":
            case "System.Int64":
            case "System.UInt64":
            case "System.Int16":
            case "System.UInt16":

                {
                    result = "INTEGER";
                    break;
                }

            case "System.Decimal":
            case "System.Double":
            case "System.Single":

                {
                    result = "REAL";
                    break;
                }
            case "System.DateTime":
                {
                    result = "TIMESTAMP";
                    break;
                }
            case "System.Char":
            case "System.String":
                {
                    result = "TEXT";
                    break;
                }
            default:
                {
                    result = "BLOB";
                    break;
                }
        }
        return result;

    }


}


 