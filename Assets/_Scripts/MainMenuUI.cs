using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    public Button StartGameButton;
    public Slider UISlider;
    [SerializeField]
    private TextMeshProUGUI GridSizeText;

    void Start()
    {
        StartGameButton.onClick.AddListener(StartGameButtonClicked);
    }

    private void StartGameButtonClicked()
    {
        PlayerPrefs.SetInt("GridSize", (int)UISlider.value);
        SceneManager.LoadScene("Main", LoadSceneMode.Single);
    }

    private void Update()
    {
        GridSizeText.text = "Grid Size: " + UISlider.value;
    }
}
