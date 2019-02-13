using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    [SerializeField]
    string mouseHoverSound = "MouseButton";

    [SerializeField]
    string buttonPressSound = "ButtonPress";

    AudioManager audioManager;
    void Start()
    {
        audioManager = AudioManager.instance;
        if (audioManager == null)
        {
            Debug.LogError("no sound found");
        }
    }
    public void Quit()
    {
        audioManager.PlaySound(buttonPressSound);
        Debug.Log("Application Quit");
        Application.Quit();
    }
    public void Retry()
    {
        audioManager.PlaySound(buttonPressSound);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnMouseOver()
    {
        audioManager.PlaySound(mouseHoverSound);
    }
}
