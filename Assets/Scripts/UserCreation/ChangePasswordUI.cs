using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ChangePasswordUI : MonoBehaviour
{
    private APIService apiService = new APIService();

    [Header("Input Fields")]
    public TMP_InputField emailInput;
    public TMP_InputField oldPasswordInput;
    public TMP_InputField newPasswordInput;

    [Header("UI Elements")]
    public Button changePasswordButton;
    public TMP_Text changePasswordText;

    [Header("Scenes")]
    public string ForgotPasswordScene = "ForgotAccount";
    public string loginSceneName = "LoginScene";
    public string registerSceneName = "RegisterScene";
    public bool redirectToRegister = false; // Set this in the Inspector

    private void Start()
    {
        changePasswordButton.onClick.AddListener(ChangePassword);
        changePasswordText.enabled = false; // Hide text at start
    }

    public void ChangePassword()
    {
        if (!InputValidator.ValidateField(emailInput) ||
            !InputValidator.ValidateField(oldPasswordInput) ||
            !InputValidator.ValidateField(newPasswordInput))
        {
            ShowMessage("All fields must be filled!");
            return;
        }

        string email = emailInput.text;
        string oldPassword = oldPasswordInput.text;
        string newPassword = newPasswordInput.text;

        apiService.GetUsers(users =>
        {
            foreach (User user in users)
            {
                if (user.email == email && user.password == oldPassword)
                {
                    int userId = user.id;

                    User updatedUser = new User
                    {
                        id = userId,
                        name = user.name,
                        email = user.email,
                        password = newPassword
                    };

                    apiService.UpdateUser(userId, updatedUser, () =>
                    {
                        ShowMessage("Password changed successfully!");
                        Invoke("RedirectToScene", 1.5f); // Delay before loading scene
                    }, error =>
                    {
                        ShowMessage("Error updating password!");
                    });

                    return;
                }
            }

            ShowMessage("Invalid email or password!");

        }, error =>
        {
            ShowMessage("Error connecting to API!");
        });
    }
    private void RedirectToScene()
    {
        if (redirectToRegister)
        {
            SceneManager.LoadScene(registerSceneName);
        }
        else
        {
            SceneManager.LoadScene(loginSceneName);
        }
    }
    private void ShowMessage(string message)
    {
        changePasswordText.text = message;
        changePasswordText.enabled = true;
    }
}
