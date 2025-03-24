//using Models;
//using Proyecto26;
//using System;
//using System.Collections;
//using System.Collections.Generic;
//using TMPro;
//using UnityEngine;
//using UnityEngine.SceneManagement;
//using static UnityEngine.UIElements.UxmlAttributeDescription;
//using UnityEngine.UI;

//public class APIReader : MonoBehaviour
//{
//    private readonly string basePath = "https://retoolapi.dev/ug9RqZ/data";
//    [Header("INPUT FIELDS")]
//    public TMP_InputField nameInput;
//    public TMP_InputField emailInput;
//    public TMP_InputField passwordInput;
//    public TMP_InputField idInput;

//    public string registerSceneName = "RegisterScene";
//    public string mainSceneName = "MenuScene"; // Change to your target scene
//    public string loginSceneName = "LoginScene";

//    public User userData;
//    public User[] users;
//    public void Start()
//    {
//        Get();
//    }
//    public void Get()
//    {
//        RestClient.Get(basePath).Then(response =>
//        {
//            string jsonResponse = response.Text;

//            // Replace incorrect column names to match your User class
//            jsonResponse = jsonResponse.Replace("Column 1", "name")
//                                       .Replace("Column 2", "email")
//                                       .Replace("Column 3", "password");

//            users = JsonHelper.ArrayFromJson<User>(jsonResponse);

//            if (users != null)
//            {
//                Debug.Log("Number of users: " + users.Length);
//            }

//        }).Catch(error =>
//        {
//            Debug.Log("Error: " + error.Message);
//        });
//    }
//    private void HighlightEmptyField(TMP_InputField inputField)
//    {
//        inputField.image.color = Color.red;
//    }

//    private void ResetFieldColor(TMP_InputField inputField)
//    {
//        inputField.image.color = Color.white;
//    }
//    public void RegisterUser()
//    {
//        bool hasEmptyField = false;

//        if (string.IsNullOrEmpty(nameInput.text))
//        {
//            HighlightEmptyField(nameInput);
//            hasEmptyField = true;
//        }
//        else
//        {
//            ResetFieldColor(nameInput);
//        }

//        if (string.IsNullOrEmpty(emailInput.text))
//        {
//            HighlightEmptyField(emailInput);
//            hasEmptyField = true;
//        }
//        else
//        {
//            ResetFieldColor(emailInput);
//        }

//        if (string.IsNullOrEmpty(passwordInput.text))
//        {
//            HighlightEmptyField(passwordInput);
//            hasEmptyField = true;
//        }
//        else
//        {
//            ResetFieldColor(passwordInput);
//        }

//        if (hasEmptyField)
//        {
//            Debug.Log("Fields cannot be empty!");
//            return;
//        }

//        // Check if email already exists
//        RestClient.Get(basePath).Then(response =>
//        {
//            User[] users = JsonHelper.ArrayFromJson<User>(response.Text);
//            foreach (User user in users)
//            {
//                if (user.email == emailInput.text)
//                {
//                    Debug.Log("User already exists with ID: " + user.id);
//                    return;
//                }
//            }

//            // If email is not found, register the new user
//            User newUser = new User
//            {
//                name = nameInput.text,
//                email = emailInput.text,
//                password = passwordInput.text
//            };

//            RestClient.Post(basePath, newUser).Then(postResponse =>
//            {
//                Debug.Log("Registration successful!");
//                SceneManager.LoadScene(loginSceneName);
//            }).Catch(error =>
//            {
//                Debug.Log("Error: " + error.Message);
//            });

//        }).Catch(error =>
//        {
//            Debug.Log("Error fetching users: " + error.Message);
//        });
//    }

//    public void LoginUser()
//    {
//        bool hasEmptyField = false;

//        if (string.IsNullOrEmpty(emailInput.text))
//        {
//            HighlightEmptyField(emailInput);
//            hasEmptyField = true;
//        }
//        else
//        {
//            ResetFieldColor(emailInput);
//        }

//        if (string.IsNullOrEmpty(passwordInput.text))
//        {
//            HighlightEmptyField(passwordInput);
//            hasEmptyField = true;
//        }
//        else
//        {
//            ResetFieldColor(passwordInput);
//        }

//        if (hasEmptyField)
//        {
//            Debug.Log("Email and Password cannot be empty!");
//            return;
//        }

//        // Check for user in API
//        RestClient.Get(basePath).Then(response =>
//        {
//            string jsonResponse = response.Text;

//            // Fix column names to match your User class
//            jsonResponse = jsonResponse.Replace("Column 1", "name")
//                                       .Replace("Column 2", "email")
//                                       .Replace("Column 3", "password");

//            User[] users = JsonHelper.ArrayFromJson<User>(jsonResponse);

//            foreach (User user in users)
//            {
//                if (user.email == emailInput.text)
//                {
//                    if (user.password.ToString() == passwordInput.text) // Convert input to int if needed
//                    {
//                        Debug.Log("Login successful! User ID: " + user.id);

//                        // ✅ SWITCH SCENE HERE
//                        SceneManager.LoadScene("MenuScene"); // Change "MainScene" to your target scene name
//                        return;
//                    }
//                    else
//                    {
//                        Debug.Log("Incorrect password.");
//                        return;
//                    }
//                }
//            }

//            Debug.Log("Email not found.");

//        }).Catch(error =>
//        {
//            Debug.Log("Login failed: " + error.Message);
//        });

//    }
//    public void DeleteUser(int userId)
//    {
//        RestClient.Delete(basePath + "/" + userId).Then(response =>
//        {
//            Debug.Log("User with ID " + userId + " deleted successfully.");
//        }).Catch(error =>
//        {
//            Debug.Log("Error deleting user: " + error.Message);
//        });
//    }

//    public void DeleteUserFromInput()
//    {
//        if (string.IsNullOrEmpty(idInput.text))
//        {
//            Debug.Log("Please enter a valid User ID!");
//            return;
//        }

//        int userId;
//        if (int.TryParse(idInput.text, out userId))
//        {
//            DeleteUser(userId);
//        }
//        else
//        {
//            Debug.Log("Invalid ID format!");
//        }
//    }

//    public string GetBasePath()
//    {
//        return basePath;
//    }

//}
//ORIGINAL SCRIPT ~~~~~DO NOT REMOVE!!!!~~~~~
using Models;
using Proyecto26;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class APIReader : MonoBehaviour
{
    private readonly string basePath = "https://retoolapi.dev/ug9RqZ/data";
    public User[] users;

    public User userData;


    public void Start()
    {
        Get();
        //DeleteUser(95);   
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Post();
        }


    }
    public string GetBasePath()
    {
        return basePath;
    }

    public void Get()
    {
        RestClient.Get(basePath).Then(response =>
        {
            try
            {
                string jsonResponse = response.Text;
                users = JsonHelper.ArrayFromJson<User>(jsonResponse);

                if (users != null)
                {
                    Debug.Log("Number of users : " + users.Length);
                }

            }
            catch (Exception ex)
            {
                Debug.Log(ex + "User array is null");
            }

        }).Catch(error =>
        {
            Debug.Log(error.Message);
        }

        );
    }


    public void Post()
    {
        RestClient.Post(basePath, userData).Then(response =>
        {
            try
            {
                if (response != null)
                {
                    Debug.Log("Successful");
                }
                else
                {
                    Debug.Log("No Response");
                }

            }
            catch (Exception ex)
            {
                Debug.Log(ex + "Error posting UserData");
            }

        }).Catch(error =>
        {
            Debug.Log(error.Message);
        }

        );
    }

    public void DeleteUser(int userId)
    {
        RestClient.Delete(basePath + "/" + userId).Then(response =>
        {
            try
            {
                if (response != null)
                {
                    Debug.Log("Deleted Successfully");
                }
                else
                {
                    Debug.Log("No Response");
                }

            }
            catch (Exception ex)
            {
                Debug.Log(ex + "Error posting UserData");
            }

        }).Catch(error =>
        {
            Debug.Log(error.Message);
        }

           );
    }

    public void PatchUser()
    {
        RestClient.Patch(basePath, userData).Then(response =>
        {
            try
            {
                if (response != null)
                {
                    Debug.Log("Patched Successfully");
                }
                else
                {
                    Debug.Log("No Response");
                }

            }
            catch (Exception ex)
            {
                Debug.Log(ex + "Error Patching UserData");
            }

        }).Catch(error =>
        {
            Debug.Log(error.Message);
        }

    );
    }

}
