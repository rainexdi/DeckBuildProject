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

    private int cardsToDraw = 2;
    private int cardsDrawnThisReward = 0;


    private void OnEnable()
    {
        if (countdownTimer != null)
        {
            countdownTimer.OnTimerComplete.AddListener(OnTimerComplete);
        }
        
        if (rewardButton != null)
        {
            rewardButton.onClick.AddListener(OnDrawCardButtonClicked);
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
            rewardButton.onClick.RemoveListener(OnDrawCardButtonClicked);
        }
    }

    private void OnDrawCardButtonClicked()
    {

            if (CardDrawSystem.Instance != null)
            {
                CardDrawSystem.Instance.DrawCards(cardsToDraw);
            }
        UpdateButtonState();
    }

    private void UpdateButtonState()
    {
        if (rewardButton != null)
        {
            bool canDraw = handView.CanDrawMore(cardsToDraw);
            rewardButton.interactable = canDraw;
        }
    }

    private void OnTimerComplete()
    {
        cardsToDraw = 2;
        cardsDrawnThisReward = 0;

        switch (killCounter.killCount)
        {
            case > 10 and < 20:
                cardsToDraw += 1;
                break;
            case >= 20 and < 30:
                cardsToDraw += 2;
                break;
            case >= 30 and < 40:
                cardsToDraw += 3;
                break;
            case >= 40:
                cardsToDraw += 4;
                break;
            default:
                cardsToDraw += 0;
                break;
        }

        rewardPanel.SetActive(true);
        rewardText.text = "Congratulations! You killed " + killCounter.killCount + " enemies! You have access to " + cardsToDraw + " cards.";
        
        UpdateButtonState();
    }

    public void IncreaseCardsToDraw(int amount)
    {
        cardsToDraw += amount;
    }

    public void OnRewardCardDrawn(int amount)
    {
        cardsDrawnThisReward += amount;
    }

    public bool CanDrawFromReward(int amount)
    {
        return cardsDrawnThisReward + amount <= cardsToDraw;
    }
}
