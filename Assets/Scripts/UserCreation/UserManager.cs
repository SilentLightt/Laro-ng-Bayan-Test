using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class UserManager : MonoBehaviour
{
    private APIService apiService = new APIService();

    [Header("Input Fields")]
    public TMP_InputField nameInput;
    public TMP_InputField emailInput;
    public TMP_InputField passwordInput;
    public TMP_InputField idInput;
    public TextMeshProUGUI notifText;

    public string loginSceneName = "LoginScene";
    public string registerSceneName = "RegisterScene";
    public string mainSceneName = "MenuScene";

    public void Update()
    {
        DeleteMenuShorcut();
    }

    public void RegisterUser()
    {
        if (!InputValidator.ValidateField(nameInput) ||
            !InputValidator.ValidateField(emailInput) ||
            !InputValidator.ValidateField(passwordInput))
        {
            ShowNotification("Fields cannot be empty!");
            return;
        }

        apiService.GetUsers(users =>
        {
            foreach (User user in users)
            {
                if (user.email == emailInput.text)
                {
                    ShowNotification("User already exists with ID: " + user.id);
                    return;
                }
            }

            User newUser = new User
            {
                name = nameInput.text,
                email = emailInput.text,
                password = passwordInput.text
            };

            apiService.RegisterUser(newUser, () =>
            {
                ShowNotification("Registration successful!");
                SceneManager.LoadScene(loginSceneName);
            }, error => ShowNotification("Error: " + error));
        }, error => ShowNotification("Error fetching users: " + error));
    }

    public void LoginUser()
    {
        if (!InputValidator.ValidateField(emailInput) ||
            !InputValidator.ValidateField(passwordInput))
        {
            ShowNotification("Email and Password cannot be empty!");
            return;
        }

        apiService.GetUsers(users =>
        {
            foreach (User user in users)
            {
                if (user.email == emailInput.text)
                {
                    if (user.password == passwordInput.text)
                    {
                        ShowNotification("Login successful! User ID: " + user.id);
                        SceneManager.LoadScene(mainSceneName);
                        return;
                    }
                    else
                    {
                        ShowNotification("Incorrect password.");
                        return;
                    }
                }
            }
            ShowNotification("Email not found.");
        }, error => ShowNotification("Login failed: " + error));
    }

    public void DeleteUserFromInput()
    {
        if (!InputValidator.ValidateField(idInput))
        {
            ShowNotification("Please enter a valid User ID!");
            return;
        }

        if (int.TryParse(idInput.text, out int userId))
        {
            apiService.DeleteUser(userId, () =>
            {
                ShowNotification("User deleted successfully.");
            }, error => ShowNotification("Error deleting user: " + error));
        }
        else
        {
            ShowNotification("Invalid ID format!");
        }
    }

    public void DeleteMenuShorcut()
    {
        if (Input.GetKey(KeyCode.LeftAlt))
        {
            SceneManager.LoadScene(4);
        }
    }

    private void ShowNotification(string message)
    {
        notifText.text = message;
        notifText.gameObject.SetActive(true);
        StartCoroutine(HideNotification());
    }

    private IEnumerator HideNotification()
    {
        yield return new WaitForSeconds(5);
        notifText.gameObject.SetActive(false);
    }
}
