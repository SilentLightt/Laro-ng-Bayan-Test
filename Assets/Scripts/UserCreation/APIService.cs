using Proyecto26;
using UnityEngine;
using System;
using System.Collections.Generic;

public class APIService
{
    private readonly string basePath = "https://retoolapi.dev/ug9RqZ/data";

    public void GetUsers(Action<User[]> callback, Action<string> errorCallback)
    {
        RestClient.Get(basePath).Then(response =>
        {
            string jsonResponse = response.Text;
            jsonResponse = jsonResponse.Replace("Column 1", "name")
                                       .Replace("Column 2", "email")
                                       .Replace("Column 3", "password");

            User[] users = JsonHelper.ArrayFromJson<User>(jsonResponse);
            callback?.Invoke(users);
        }).Catch(error =>
        {
            errorCallback?.Invoke(error.Message);
        });
    }

    public void RegisterUser(User newUser, Action successCallback, Action<string> errorCallback)
    {
        RestClient.Post(basePath, newUser).Then(response =>
        {
            successCallback?.Invoke();
        }).Catch(error =>
        {
            errorCallback?.Invoke(error.Message);
        });
    }

    public void DeleteUser(int userId, Action successCallback, Action<string> errorCallback)
    {
        RestClient.Delete(basePath + "/" + userId).Then(response =>
        {
            successCallback?.Invoke();
        }).Catch(error =>
        {
            errorCallback?.Invoke(error.Message);
        });
    }
    public void UpdateUser(int userId, User updatedUser, Action successCallback, Action<string> errorCallback)
    {
        RestClient.Patch(basePath + "/" + userId, updatedUser).Then(response =>
        {
            successCallback?.Invoke();
        }).Catch(error =>
        {
            errorCallback?.Invoke(error.Message);
        });
    }

}
