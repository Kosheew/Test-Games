using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    private void Update()
    {
        if(Movement.instance == null)
        {
            panel.SetActive(true);
            Time.timeScale = 0;
        }
    }
    public void ButtonQuite()
    {
        Application.Quit();
    }
    public void ButtonPlay() 
    {
        Time.timeScale = 1;
        panel.SetActive(false);
    }
    public void ButtonReload()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
        panel.SetActive(false);
    }
    public void ButtonPause()
    {
        Time.timeScale = 0;
        panel.SetActive(true);
    }

}
