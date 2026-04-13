using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;


public class CardView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text description;
    [SerializeField] private TMP_Text level;
    [SerializeField] private SpriteRenderer imageSR;
    [SerializeField] private GameObject wrapper;
    
    private Collider2D cardCollider;
    private SortingGroup sortingGroup;

    public event Action<CardView> OnCardPlayed;

    public Card Card { get; private set; }

    private void Start()
    {
        cardCollider = GetComponent<Collider2D>();
        sortingGroup = GetComponent<SortingGroup>();

    }


    public void SetCardData(Card card)
    {
        Card = card;

        if (title != null) title.text = card.Name;
        if (description != null) description.text = card.Description;
        if (level != null) level.text = card.Level.ToString();
        if (imageSR != null) imageSR.sprite = card.Image;
    }


    private void PlayCard()
    {
        if (Card == null) return;

        Card.Play(); 
        OnCardPlayed?.Invoke(this);

        Destroy(gameObject);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Vector3 hoverPos = new Vector3(0, -2, transform.position.z);
        sortingGroup.sortingOrder = 10;
        CardHoverSystem.Instance.Show(Card, hoverPos);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        sortingGroup.sortingOrder = 3;
        CardHoverSystem.Instance.Hide();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) return;
        OnPointerExit(eventData);
        PlayCard();
    }



}
