using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class User
{
    public int id;
    public string name;
    public string email;
    public string password;
}

[System.Serializable]
public class UserCollection
{
    public User[] users;
}
