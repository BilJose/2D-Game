using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    string hoverOverSound = "ButtonHover";

    [SerializeField]
    string pressButtonSound = "ButtonSound";
    AudioManager audioManager;
    void Start()
    {
        audioManager = AudioManager.instance;
        if(audioManager == null)
        {
            Debug.LogError("no Sound found");
        }
    }
    public void StartGame()
    {
        audioManager.PlaySound(pressButtonSound);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+ 1);
    }
    public void QuitGame()
    {
        audioManager.PlaySound(pressButtonSound);
        Debug.Log("We quit the game");
        Application.Quit();
    }
    public void OnMouseOver()
    {
        audioManager.PlaySound(hoverOverSound);
    }

    
}
