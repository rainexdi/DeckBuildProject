using UnityEngine;

[System.Serializable]
public class EffectData 
{
    public CardEffectType effectType;
    public int value1; // Value for the actual effect (ex. damage amount)
    public int value2; // Value for duration of the effects 
}

public enum CardEffectType
{
    HealthIncrease,
    CardDraw,
    Buff,

}