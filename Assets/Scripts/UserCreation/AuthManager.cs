//using Proyecto26;
//using TMPro;
//using UnityEngine;

//public class AuthManager : MonoBehaviour
//{
//    public TMP_InputField nameInput;
//    public TMP_InputField emailInput;
//    public TMP_InputField passwordInput;
//    public GameObject registerPanel;
//    public GameObject loginPanel;
//    public TMP_InputField loginEmailInput;
//    public TMP_InputField loginPasswordInput;
//    public GameObject mainUIPanel;
//    public void RegisterUser()
//    {
//        // Input Validation
//        if (string.IsNullOrEmpty(nameInput.text) ||
//            string.IsNullOrEmpty(emailInput.text) ||
//            string.IsNullOrEmpty(passwordInput.text))
//        {
//            Debug.Log("All fields are required!");
//            return;
//        }

//        // Create user object
//        User newUser = new User
//        {
//            name = nameInput.text,
//            email = emailInput.text,
//            password = passwordInput.text
//        };

//        // API Call
//        RestClient.Post("https://retoolapi.dev/ug9RqZ/data", newUser).Then(response =>
//        {
//            Debug.Log("User Registered Successfully");
//            registerPanel.SetActive(false);
//            loginPanel.SetActive(true);
//        }).Catch(error =>
//        {
//            Debug.Log("Error Registering: " + error.Message);
//        });
//    }


//    public void LoginUser()
//    {
//        // Input Validation
//        if (string.IsNullOrEmpty(loginEmailInput.text) ||
//            string.IsNullOrEmpty(loginPasswordInput.text))
//        {
//            Debug.Log("Email and Password are required!");
//            return;
//        }

//        string email = loginEmailInput.text;
//        string password = loginPasswordInput.text;

//        // Fetch users from API
//        RestClient.Get("https://retoolapi.dev/aSdieg/data").Then(response =>
//        {
//            User[] allUsers = JsonHelper.ArrayFromJson<User>(response.Text);
//            User authenticatedUser = System.Array.Find(allUsers, user => user.email == email && user.password == password);

//            if (authenticatedUser != null)
//            {
//                Debug.Log("Login Successful! Welcome, " + authenticatedUser.name);

//                // 🔹 Save User Session
//                PlayerPrefs.SetString("UserEmail", authenticatedUser.email);
//                PlayerPrefs.SetInt("UserId", authenticatedUser.id);
//                PlayerPrefs.Save();

//                loginPanel.SetActive(false);
//                mainUIPanel.SetActive(true);
//            }
//            else
//            {
//                Debug.Log("Invalid email or password.");
//            }
//        }).Catch(error =>
//        {
//            Debug.Log("Error Logging In: " + error.Message);
//        });
//    }
//    public void Logout()
//    {
//        PlayerPrefs.DeleteKey("UserEmail");
//        PlayerPrefs.DeleteKey("UserId");
//        PlayerPrefs.Save();

//        mainUIPanel.SetActive(false);
//        loginPanel.SetActive(true);
//    }

//}
//using UnityEngine;
//using UnityEngine.SceneManagement;
//using Proyecto26;

//public class AuthManager : MonoBehaviour
//{
//    public APIReader apiReader;
//    public string registerSceneName = "RegisterScene";
//    public string mainSceneName = "MainScene"; // Change to your target scene
//    public string loginSceneName = "LoginScene";

//    public void RegisterUser(User newUser)
//    {
//        RestClient.Post(apiReader.GetBasePath(), newUser).Then(response =>
//        {
//            Debug.Log("Registration successful");
//            SceneManager.LoadScene(loginSceneName);
//        }).Catch(error =>
//        {
//            Debug.Log("Registration failed: " + error.Message);
//        });
//    }

//    public void LoginUser(string username, string password)
//    {
//        RestClient.Get(apiReader.GetBasePath()).Then(response =>
//        {
//            User[] users = JsonHelper.ArrayFromJson<User>(response.Text);
//            foreach (User user in users)
//            {
//                if (user.name == username && user.password == password)
//                {
//                    Debug.Log("Login successful");
//                    SceneManager.LoadScene(mainSceneName);
//                    return;
//                }
//            }
//            Debug.Log("Invalid credentials");
//        }).Catch(error =>
//        {
//            Debug.Log("Login failed: " + error.Message);
//        });
//    }

//    public void Logout()
//    {
//        SceneManager.LoadScene(loginSceneName);
//    }
//}
