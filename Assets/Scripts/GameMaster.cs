using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public static GameMaster gm;

    [SerializeField]
    private WaveSpanner waveSpanner;

    [SerializeField]
    private int maxLives = 3;
    private static int _remainingLives;
    public static int RemainingLives
    {
        get { return _remainingLives; }
    }
    [SerializeField]
    private int startingMoney; 
    public static int Money;
    void Awake()
    {
        if (gm == null)
        {
            gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        }
    }
    
    public Transform playerPrefab;
    public Transform spawnPoint;
    public Transform spawnPrefab;
    public string spawnSoundName = "Spawn";
    public string respawnCountdownSoundName = "Respawn";
    public string gameOverSoundName = "GameOver";

    public int spawnDelay = 2;

    public CameraShake cameraShake;
    [SerializeField]
    private GameObject gameOverUI;
    [SerializeField]
    private GameObject upgradeMenu;

    public delegate void UpgradeMenuCallBack(bool active);
    public UpgradeMenuCallBack onToggleUpgradeMenu;

    private AudioManager audioManager;

    void Start()
    {
        if (cameraShake == null)
        {
            Debug.LogError("no Camera Shake");
        }
        _remainingLives = maxLives;
        Money = startingMoney;

        audioManager = AudioManager.instance;
        if (audioManager == null)
        {
            Debug.LogError("no adio manager found");
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ToggleUpgradeMenu();
        }
    }
    private void ToggleUpgradeMenu()
    {
        upgradeMenu.SetActive(!upgradeMenu.activeSelf);
        waveSpanner.enabled = !upgradeMenu.activeSelf;
        onToggleUpgradeMenu.Invoke(upgradeMenu.activeSelf);
    }
    public void EndGame()
    {
        audioManager.PlaySound(gameOverSoundName);
        Debug.Log("gameOver");
        gameOverUI.SetActive(true);
    }

    public IEnumerator _RespawnPlayer()
    {
        audioManager.PlaySound(respawnCountdownSoundName);

        yield return new WaitForSeconds(spawnDelay);
        audioManager.PlaySound(spawnSoundName);
        Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
        Transform clone= Instantiate(spawnPrefab, spawnPoint.position, spawnPoint.rotation)as Transform;
        Destroy(clone.gameObject, 3f);
        
    }
    public static void KillPlayer(Player player)
    {
        Destroy (player.gameObject);
        _remainingLives--;
        if(_remainingLives<=0)
        {
            gm.EndGame();
        }
        else
        {
            gm.StartCoroutine(gm._RespawnPlayer());
        }       
    }

    public static void KillEnemy(Enemy enemy)
    {
        gm._killEnemy(enemy);
    }

    public void _killEnemy(Enemy _enemy)
    {

        audioManager.PlaySound(_enemy.deathSoundName);

        Money += _enemy.moneyDrop;
        audioManager.PlaySound("Money");

       Transform _clone = Instantiate(_enemy.deathParticles, _enemy.transform.position, Quaternion.identity)as Transform;
        Destroy(_clone.gameObject, 5f);

        cameraShake.Shake(_enemy.shakeAmt, _enemy.shakeLength);
        Destroy(_enemy.gameObject);
    }
}
