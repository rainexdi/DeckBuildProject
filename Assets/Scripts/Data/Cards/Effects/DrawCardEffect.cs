using System;
using UnityEngine;

public class DrawCardEffect : CardEffects
{
    public int cardsToDraw = 1;

    public DrawCardEffect(int cards = 1)
    {
        cardsToDraw = cards;
    }

    public override void Execute()
    {
        if (CardDrawSystem.Instance == null)
        {
            Debug.LogError("CardDrawSystem instance not found! Cannot execute DrawCardEffect.");
            return;
        }

        if (CardDrawSystem.Instance.DrawCards(cardsToDraw))
        {
            Debug.Log($"Successfully drew {cardsToDraw} card(s).");
            
            var rewardHandler = CardDrawSystem.Instance.GetRewardHandler();
            if (rewardHandler != null)
            {
                rewardHandler.OnRewardCardDrawn(cardsToDraw);
                rewardHandler.IncreaseCardsToDraw(cardsToDraw);
            }
        }
        else
        {
            Debug.Log("Cannot draw card: Deck is empty or hand is full.");
        }
    }
}
