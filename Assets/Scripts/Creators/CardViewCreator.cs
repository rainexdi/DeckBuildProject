using UnityEngine;
using DG.Tweening;

public class CardViewCreator : MonoBehaviour
{
    public static CardViewCreator Instance { get; private set; }
    [SerializeField] private CardView cardViewPrefab;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    public CardView CreateCardView(Card card, Vector3 position, Quaternion rotation)
    {
        CardView cardView = Instantiate(cardViewPrefab, position, rotation);
        cardView.transform.localScale = Vector3.zero;
        cardView.transform.DOScale(Vector3.one, 0.15f);
        cardView.SetCardData(card);
        return cardView;
    }

}
