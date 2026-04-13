using System;
using UnityEngine;

public class Card 
{
    public string Name => data.cardName;
    public string Description => data.description;
    public int Level => data.level;
    public Sprite Image  => data.imageSR;

    private readonly CardData data;
    private CardEffects[] effects;

    public event Action OnCardDrawPlayed;
    public event Action OnCardPlayedComplete;

    public Card(CardData cardData)
    {
        data = cardData;
    }

    public void Play()
    {
        effects = data.CreateEffects();
        data.ExecuteEffects();
        Debug.Log($"Card '{Name}' was played!");

        BroadcastEffectEvents();
        OnCardPlayedComplete?.Invoke();
    }

    // This method checks the effects of the cards and if it finds a DrawCardEffect, it invokes the OnCardDrawPlayed event.
    private void BroadcastEffectEvents()
    {
        foreach (CardEffects effect in effects)
        {
            if (effect is DrawCardEffect)
            {
                OnCardDrawPlayed?.Invoke();
            }
        }
    }
}
