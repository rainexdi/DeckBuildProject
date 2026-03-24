using TMPro;
using UnityEngine;

public class LevelProgressUIManager : MonoBehaviour
{
    [SerializeField] private KillCounter killCounter;
    [SerializeField] private TextMeshProUGUI killCountText;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private CountdownTimer countdownTimer;
    [SerializeField] private GameObject spawner;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerAttack playerAttack;

    private void Awake()
    {
        UpdateKillCountUI();
    }

    private void OnEnable()
    {
        EnemyHealth.OnEnemyDeath.AddListener(onEnemyKilled);
        
        if (countdownTimer != null)
        {
            countdownTimer.OnTimerTick.AddListener(UpdateTimerUI);
            countdownTimer.OnTimerComplete.AddListener(OnTimerComplete);
        }
    }

    private void OnDisable()
    {
        EnemyHealth.OnEnemyDeath.RemoveListener(onEnemyKilled);
        
        if (countdownTimer != null)
        {
            countdownTimer.OnTimerTick.RemoveListener(UpdateTimerUI);
            countdownTimer.OnTimerComplete.RemoveListener(OnTimerComplete);
        }
    }

    private void Start()
    {
        if (countdownTimer != null)
        {
            countdownTimer.StartTimer();
        }
    }

    private void onEnemyKilled()
    {
        UpdateKillCountUI();
    }

    private void UpdateKillCountUI()
    {
        if (killCountText != null)
        {
            killCountText.text = $"Kills: {killCounter.killCount}";
        }
    }

    private void UpdateTimerUI(string formattedTime)
    {
        if (timerText != null)
        {
            timerText.text = formattedTime;
        }
    }

    private void OnTimerComplete()
    {
        EnemyMovement[] allEnemies = FindObjectsByType<EnemyMovement>(FindObjectsSortMode.None);
        foreach (EnemyMovement enemy in allEnemies)
        {
            PoolManager.instance.ReturnObject(enemy.gameObject);
        }

        if (spawner != null)
        {
            spawner.SetActive(false);
        }

        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }

        if (playerAttack != null)
        {
            playerAttack.enabled = false;
        }
    }
}
