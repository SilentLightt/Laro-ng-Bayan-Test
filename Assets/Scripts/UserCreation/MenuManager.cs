using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI userNameText;
    public string loginSceneName = "LoginScene";

    // Start is called before the first frame update
    void Start()
    {
        // Retrieve the stored user name from PlayerPrefs
        string userName = PlayerPrefs.GetString("UserName", "Guest");

        // Display the user name in the UI
        userNameText.text = "Welcome, " + userName;
    }

    // Logout method
    public void Logout()
    {
        // Clear the session data (user name) from PlayerPrefs
        PlayerPrefs.DeleteKey("UserName");
        PlayerPrefs.Save();

        // Return to the login scene
        SceneManager.LoadScene(loginSceneName);
    }
}
