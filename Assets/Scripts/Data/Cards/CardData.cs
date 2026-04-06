using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "Data/CardData")]

public class CardData : ScriptableObject
{
    public string cardName;
    public string description;
    public int level;
    public Sprite imageSR;

    [SerializeField] private EffectData[] effectsData = new EffectData[0];

    private CardEffects CreateEffectFromData(EffectData data)
    {
        return data.effectType switch
        {
            CardEffectType.CardDraw => new DrawCardEffect(data.value1),
            _ => null
        };
    }

    public CardEffects[] CreateEffects()
    {
        List<CardEffects> effects = new();

        foreach (EffectData data in effectsData)
        {
            CardEffects effect = CreateEffectFromData(data);
            if (effect != null)
            {
                effects.Add(effect);
            }
        }

        return effects.ToArray();
    }

    public void ExecuteEffects()
    {
        CardEffects[] effects = CreateEffects();
        foreach (CardEffects effect in effects)
        {
            effect?.Execute();
        }
    }
}
