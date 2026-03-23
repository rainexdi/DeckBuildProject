using UnityEngine;
using UnityEngine.Events;

public class CountdownTimer : MonoBehaviour 
{
    [SerializeField] private float duration = 300f; // 5 minutes
    private float timeRemaining;
    private bool isRunning;

    public UnityEvent OnTimerComplete = new UnityEvent();
    public UnityEvent<string> OnTimerTick = new UnityEvent<string>(); // Reports formatted time

    private void Start()
    {
        ResetTimer();
    }

    private void Update()
    {
        if (!isRunning) return;

        timeRemaining -= Time.deltaTime;

        if (timeRemaining <= 0)
        {
            timeRemaining = 0;
            isRunning = false;
            OnTimerTick?.Invoke(FormatTime(0));
            OnTimerComplete?.Invoke();
        }
        else
        {
            OnTimerTick?.Invoke(FormatTime(timeRemaining));
        }
    }

    private string FormatTime(float seconds)
    {
        int minutes = (int)(seconds / 60f);
        int secs = (int)(seconds % 60f);
        return $"{minutes:00}:{secs:00}";
    }

    public void StartTimer() => isRunning = true;
    public void PauseTimer() => isRunning = false;
    public void ResetTimer() => timeRemaining = duration;
    public float GetTimeRemaining() => timeRemaining;
    public bool IsRunning() => isRunning;
    public string GetFormattedTime() => FormatTime(timeRemaining);
}