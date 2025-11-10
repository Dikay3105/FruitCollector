using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("Panels")]
    public GameObject settingsPanel;
    public GameObject creditsPanel;
    public GameObject mainMenuPanel;

    public void NewGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void ContinueGame()
    {
        // Load lại data nếu có
        Debug.Log("Continue game (chưa làm lưu game - có thể thêm sau)");
        SceneManager.LoadScene("GameScene");
    }

    public void OpenSettings()
    {
        mainMenuPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void OpenCredits()
    {
        creditsPanel.SetActive(true);
    }

    public void ExitGame()
    {
        Debug.Log("Thoát game");
        Application.Quit();
    }

    public void BackToMenu()
    {
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(false);
            mainMenuPanel.SetActive(true);
        }
            
        if (creditsPanel != null)
            creditsPanel.SetActive(false);
    }

}
