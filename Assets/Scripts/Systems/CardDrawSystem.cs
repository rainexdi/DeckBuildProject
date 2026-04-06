using UnityEngine;
using System.Collections;

public class CardDrawSystem : MonoBehaviour
{
    public static CardDrawSystem Instance { get; private set; }

    [SerializeField] private CardRewardSystem cardRewardSystem;
    [SerializeField] private CardViewCreator cardViewCreator;
    [SerializeField] private HandView handView;
    [SerializeField] private RewardHandler rewardHandler;  // Add this field


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }   
        
        Instance = this;
    }

    public bool DrawCard()
        {
        if (!handView.hasSpace)
        {
            Debug.LogWarning("Cannot draw card: hand is full!");
            return false;
        }

        CardData cardData = cardRewardSystem.GetRandomCard();
        if (cardData == null)
        {
            Debug.LogError("No card data available! Check if CardRewardSystem is properly set up.");
            return false;
        }

        Debug.Log("Drawing card:" + cardData.cardName);
        Card card = new(cardData);
        CardView cardView = CardViewCreator.Instance.CreateCardView(card, Vector3.zero, Quaternion.identity);

        if (cardView == null)
        {
            Debug.LogError("Failed to create card! Check if CardViewCreator prefab is assigned.");
            return false;
        }

        cardView.SetCardData(card);
        StartCoroutine(handView.AddCard(cardView));

        return true;
    }

    public bool DrawCards(int count)
    {
        bool allSuccessful = true;

        for (int i = 0; i < count; i++)
        {
            bool cardDrawSuccessful = DrawCard();

            if (!cardDrawSuccessful)
            {
                allSuccessful = false;
                break;
            }   
        }
        return allSuccessful;
    }

    public bool CanDrawCard()
    {
        return handView != null && handView.hasSpace;
    }

    public RewardHandler GetRewardHandler() => rewardHandler;  // Add this getter
}
