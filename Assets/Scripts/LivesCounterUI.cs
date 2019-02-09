using UnityEngine.UI;
using UnityEngine;
[RequireComponent(typeof(Text))]
public class LivesCounterUI : MonoBehaviour
{
    
    private Text LivesText;
    // Start is called before the first frame update
    void Awake()
    {
        LivesText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        LivesText.text = "LIVES: " + GameMaster.RemainingLives.ToString();
    }
}
