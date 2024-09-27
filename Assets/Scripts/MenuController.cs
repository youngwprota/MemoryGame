using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MenuController : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject playMenuPanel;

    void Awake()
    {
        playMenuPanel.SetActive(false);
    }

    void Start()
    {
  
    }

    public void PlayGame(int difficulty)
    {
        PlayerPrefs.SetInt("Difficulty", difficulty);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

 }
