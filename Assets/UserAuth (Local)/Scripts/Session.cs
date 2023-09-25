using Scheme;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Session : MonoBehaviour
{
    public static Session Instance { get; private set; }
    public Scheme.User User;
     


    private void Awake()
    {
        User = new Scheme.User();
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void SetUser(Scheme.User mUser)
    {
        User = mUser;
    }


}

 

