using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinUI : MonoBehaviour
{
    public Button MainMenuButton;
    
    void Start()
    {
        MainMenuButton.onClick.AddListener(MainMenuButtonClicked);
    }

    private void MainMenuButtonClicked()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}
