using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamoOverUI : MonoBehaviour
{
    public Button MainMenuButton;
    public Button RetryButton;
    
    void Start()
    {
        MainMenuButton.onClick.AddListener(MainMenuButtonClicked);
        RetryButton.onClick.AddListener(RetryButtonClicked);

    }

    private void MainMenuButtonClicked()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    private void RetryButtonClicked()
    {
        SceneManager.LoadScene("Main", LoadSceneMode.Single);
    }
    
}
