using UnityEngine;

public class Card 
{
    public string Name => data.cardName;
    public string Description => data.description;
    public int Level => data.level;
    public Sprite Image  => data.imageSR;

    private readonly CardData data;

    public Card(CardData cardData)
    {
        data = cardData;
    }

    public void Play()
    {
        data.ExecuteEffects();
        Debug.Log($"Card '{Name}' was played!");
    }
}
