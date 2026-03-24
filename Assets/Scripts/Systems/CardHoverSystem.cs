using UnityEngine;

public class CardHoverSystem : MonoBehaviour
{
    public static CardHoverSystem Instance;
    [SerializeField] private CardView cardViewHover;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    public void Show(Card card, Vector3 position)
    {
        cardViewHover.gameObject.SetActive(true);
        cardViewHover.SetCardData(card);
        cardViewHover.transform.position = position;

    }

    public void Hide()
    {
        cardViewHover.gameObject.SetActive(false);
    }



}
