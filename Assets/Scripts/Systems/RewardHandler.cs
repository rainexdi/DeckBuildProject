using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class RewardHandler : MonoBehaviour
{
    [SerializeField] private Button rewardButton;
    [SerializeField] private TextMeshProUGUI rewardText;
    [SerializeField] private GameObject rewardPanel;
    [SerializeField] private HandView handView;
    [SerializeField] private CountdownTimer countdownTimer;
    [SerializeField] private KillCounter killCounter;

    private void OnEnable()
    {
        if (countdownTimer != null)
        {
            countdownTimer.OnTimerComplete.AddListener(OnTimerComplete);
        }
        
        if (rewardButton != null)
        {
            rewardButton.onClick.AddListener(DrawCard);
        }
        
        UpdateButtonState();
    }

    private void OnDisable()
    {
        if (countdownTimer != null)
        {
            countdownTimer.OnTimerComplete.RemoveListener(OnTimerComplete);
        }
        
        if (rewardButton != null)
        {
            rewardButton.onClick.RemoveListener(DrawCard);
        }
    }

    private void DrawCard()
    {
        if (!handView.hasSpace)
        {
            Debug.LogWarning("Cannot draw card: hand is full!");
            return;
        }

        Debug.Log("Drawing card as reward...");
        CardView cardView = CardViewCreator.Instance.CreateCardView(Vector3.zero, Quaternion.identity);

        if (cardView == null)
        {
            Debug.LogError("Failed to create card! Check if CardViewCreator prefab is assigned.");
            return;
        }

        StartCoroutine(handView.AddCard(cardView));
        
        UpdateButtonState();
    }

    private void UpdateButtonState()
    {
        if (rewardButton != null)
        {
            rewardButton.interactable = handView != null && handView.hasSpace;
        }
    }

    private void OnTimerComplete()
    {
        switch (killCounter.killCount)
        {
            case > 10 and < 20:
                handView.maxCards += 1;
                break;
            case >= 20 and < 30:
                handView.maxCards += 2;
                break;
            case >= 30 and < 40:
                handView.maxCards += 3;
                break;
            case >= 40:
                handView.maxCards += 4;
                break;
            default:
                handView.maxCards += 0;
                break;
        }

        rewardPanel.SetActive(true);
        rewardText.text = "Congratulations! You killed " + killCounter.killCount + " enemies! You have access to " + handView.maxCards + " cards.";
        
        UpdateButtonState();
    }
}
