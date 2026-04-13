using UnityEngine;

[System.Serializable]
public class EffectData 
{
    public CardEffectType effectType;
    public int value1; // Value for the actual effect (ex. damage amount)
    public int value2; // Value for duration of the effects 

    public EffectData(CardEffectType type, int val1 = 0, int val2 = 0)
    {
        effectType = type;
        value1 = val1;
        value2 = val2;
    }
}

public enum CardEffectType
{
    HealthIncrease,
    CardDraw,
    Buff,

}