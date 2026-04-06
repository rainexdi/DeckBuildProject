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
        }

        if (CardDrawSystem.Instance.DrawCards(cardsToDraw))
        {
            Debug.Log($"Successfully drew {cardsToDraw} card(s).");
        }
        else
        {
            Debug.Log("Cannot draw card: Deck is empty or hand is full.");
        }
    }
   

}
