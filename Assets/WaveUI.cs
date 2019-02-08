using UnityEngine.UI;
using UnityEngine;

public class WaveUI : MonoBehaviour
{
    [SerializeField]
    WaveSpanner spawner;
    [SerializeField]
    Animator waveAnimator;

    [SerializeField]
    Text waveCountdownText;

    [SerializeField]
    Text WaveCountText;

    private WaveSpanner.SpawnState previousState;

    // Start is called before the first frame update
    void Start()
    {
        if (spawner == null)
        {
            Debug.LogError("Spawner Referenced needed");
            this.enabled = false;
        }
        if (waveAnimator == null)
        {
            Debug.LogError("Spawner Referenced needed");
            this.enabled = false;
        }
        if (waveCountdownText == null)
        {
            Debug.LogError("Spawner Referenced needed");
            this.enabled = false;
        }
        if (WaveCountText == null)
        {
            Debug.LogError("Spawner Referenced needed");
            this.enabled = false;
        }

    }
     


// Update is called once per frame
void Update()
    {
        switch (spawner.State)
        {
            case WaveSpanner.SpawnState.Counting:
                UpdateCountingUI();
                break;
            case WaveSpanner.SpawnState.Spawning:
                UpdateSpawningUI();
                break;
        }
        previousState = spawner.State;
    }
    void UpdateCountingUI()
    {
        if (previousState!= WaveSpanner.SpawnState.Counting)
        {
            waveAnimator.SetBool("WaveIncoming", false);
            waveAnimator.SetBool("WaveCountdown", true);
            //Debug.Log("Counting");
        }
        waveCountdownText.text = ((int)spawner.WaveCountdown).ToString();
        
    }
    void UpdateSpawningUI()
    {
        if (previousState != WaveSpanner.SpawnState.Spawning)
        {
            waveAnimator.SetBool("WaveCountdown", false);
            waveAnimator.SetBool("WaveIncoming", true);
            waveCountdownText.text = spawner.NextWave.ToString();
            //Debug.Log("Spawning");
        }
    }
}
