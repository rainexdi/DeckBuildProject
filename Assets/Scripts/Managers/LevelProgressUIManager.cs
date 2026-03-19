using TMPro;
using UnityEngine;

public class LevelProgressUIManager : MonoBehaviour
{
    private int killCount = 0;
    [SerializeField] private TextMeshProUGUI killCountText;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private CountdownTimer countdownTimer;

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
        killCount++;
        UpdateKillCountUI();
    }

    private void UpdateKillCountUI()
    {
        if (killCountText != null)
        {
            killCountText.text = $"Kills: {killCount}";
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
        Debug.Log("Timer complete!");
        // Add your logic here (end level, show game over screen, etc.)
    }
}
