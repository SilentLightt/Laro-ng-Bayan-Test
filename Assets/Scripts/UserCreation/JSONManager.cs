using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JSONManager : MonoBehaviour
{
    public TextAsset JSONFile;
    public UserCollection userCollection = new UserCollection();
    public void CreateJSON()
    { 
        string data = JsonUtility.ToJson(userCollection);
        File.WriteAllText($"{Application.dataPath}/text.txt", data);
    }

    public void LoadJSON()
    {
        if (JSONFile == null) { return; }
        userCollection =  JsonUtility.FromJson<UserCollection>(JSONFile.text);  
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            CreateJSON();
            Debug.Log("JSON Created");
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            LoadJSON();
            Debug.Log("JSON Loaded");
        }
    }

}
