using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class RewardCard
{
    public CardData cardData;
    public int quantity = 1; // How many times this card can appear
}

public class CardRewardSystem : MonoBehaviour
{
    [SerializeField] private RewardCard[] rewardCards;

    public CardData GetRandomCard()
    {
        if (rewardCards == null || rewardCards.Length == 0)
        {
            Debug.LogWarning("No cards available in the reward system!");
            return null;
        }

        // Build a flat list with quantities
        List<CardData> cardPool = new();
        foreach (RewardCard rewardCard in rewardCards)
        {
            for (int i = 0; i < rewardCard.quantity; i++)
            {
                cardPool.Add(rewardCard.cardData);
            }
        }

        int randomIndex = Random.Range(0, cardPool.Count);
        return cardPool[randomIndex];
    }
}
