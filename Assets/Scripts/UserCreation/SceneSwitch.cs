using UnityEngine;
using UnityEngine.SceneManagement; 

public class SceneSwitch : MonoBehaviour
{
   // public GameObject[] menuPanels;       // Array of menu panels (Main Menu, Settings, etc.)
    //private int currentPanelIndex = 0;

    public void ScenebyInteger(int scenenum)
    {
        SceneManager.LoadScene(scenenum);
    }
    public void SceneByName(string name)
    {
        SceneManager.LoadScene(name);
    }
    //public void ShowPanel(int panelIndex)
    //{
        
    //    // Hide all panels
    //    foreach (var panel in menuPanels)
    //    {
    //        panel.SetActive(false);
    //    }

    //    // Show the selected panel
    //    menuPanels[panelIndex].SetActive(true);
    //    currentPanelIndex = panelIndex;
    //}
    //public void BackButton()
    //{
      
    //    // Return to the previous menu panel, or stay on the first one if already there
    //    int previousPanelIndex = Mathf.Max(currentPanelIndex - 1, 0);
    //    ShowPanel(previousPanelIndex);
    //}
}
