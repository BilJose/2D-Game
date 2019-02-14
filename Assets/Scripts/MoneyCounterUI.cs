using UnityEngine.UI;
using UnityEngine;
[RequireComponent(typeof(Text))]
public class MoneyCounterUI : MonoBehaviour
{

    private Text MoneyText;
    // Start is called before the first frame update
    void Awake()
    {
        MoneyText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
       MoneyText.text = "Money: " + GameMaster.Money.ToString();
    }
}
