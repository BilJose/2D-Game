using System.Collections;
using UnityEngine;
using UnityStandardAssets._2D;


[RequireComponent(typeof(Platformer2DUserControl))]
public class Player : MonoBehaviour
{

    public int fallBoundary = -20;
    public string deathSoundName = "DeathVoice";
    public string damageSoundName = "Grunt";


    AudioManager audioManager;


    [SerializeField]

    private StatusIndicator statusIndicator;

    private PlayerStats stats;


    void Start()
    {
        stats = PlayerStats.instance;
        stats.curHealth = stats.maxHealth;
        if (statusIndicator == null)
        {
            Debug.LogError("No status Indicator");
        }
        else
        {
           statusIndicator.SetHealth(stats.curHealth, stats.maxHealth);
        }
        GameMaster.gm.onToggleUpgradeMenu += OnUpgradeMenuToggle;


        audioManager = AudioManager.instance;
        if (audioManager == null)
        {
            Debug.LogError("no audio manager in scene");
        }
        InvokeRepeating("RegenHealth", 1f/stats.healthRegenRate, 1f/stats.healthRegenRate);
    }
    void RegenHealth()
    {
        stats.curHealth += 1;
        statusIndicator.SetHealth(stats.curHealth, stats.maxHealth);
    }
    void Update()
    {
        if (transform.position.y <= fallBoundary)
            DamagePlayer(999999);
   }
    void OnUpgradeMenuToggle(bool active)
    {
        GetComponent<Platformer2DUserControl>().enabled = !active;
        Weapon _weapon = GetComponentInChildren<Weapon>();
        if (_weapon != null)
            _weapon.enabled = !active;
    }
    public void DamagePlayer(int damage)
    {
        stats.curHealth -= damage;
        if (stats.curHealth <= 0)
        {
            audioManager.PlaySound(deathSoundName);
            GameMaster.KillPlayer(this);
        }
        else
        {
            audioManager.PlaySound(damageSoundName);
        }
        statusIndicator.SetHealth(stats.curHealth, stats.maxHealth);
    }
    void OnDestroy()
    {
        GameMaster.gm.onToggleUpgradeMenu -= OnUpgradeMenuToggle;
    }
}
