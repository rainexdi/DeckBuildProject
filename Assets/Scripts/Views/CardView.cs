using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class CardView : MonoBehaviour
{
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text description;
    [SerializeField] private TMP_Text level;
    [SerializeField] private SpriteRenderer imageSR;
    [SerializeField] private GameObject wrapper;
    
    private Collider2D cardCollider;
    private bool isCurrentlyHovered = false;

    public Card Card { get; private set; }

    private void Start()
    {
        cardCollider = GetComponent<Collider2D>();

    }

    private void Update()
    {
        // Manual hover detection - reliable alternative to OnMouseEnter/Exit
        DetectHover();
    }

    private void DetectHover()
    {
        // Get mouse position in world coordinates
        Vector3 mouseScreenPos = Mouse.current.position.ReadValue();
        

        // Convert screen position to world position
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
        
        // Check if mouse point is inside this collider
        bool isMouseOver = cardCollider.OverlapPoint(mouseWorldPos);

        // Trigger events on state change
        if (isMouseOver && !isCurrentlyHovered)
        {
            isCurrentlyHovered = true;
            OnCardHoverEnter();
        }
        else if (!isMouseOver && isCurrentlyHovered)
        {
            isCurrentlyHovered = false;
            OnCardHoverExit();
        }
    }

    public void SetCardData(Card card)
    {
        Card = card;

        if (title != null) title.text = card.Name;
        if (description != null) description.text = card.Description;
        if (level != null) level.text = card.Level.ToString();
        if (imageSR != null) imageSR.sprite = card.Image;
    }

    private void OnCardHoverEnter()
    {

        wrapper.SetActive(false);
        Vector3 hoverPos = new(transform.position.x, -2, transform.position.z);
        CardHoverSystem.Instance.Show(Card, hoverPos);
    }

    private void OnCardHoverExit()
    {

        if (CardHoverSystem.Instance != null)
        {
            CardHoverSystem.Instance.Hide();
        }

        wrapper.SetActive(true);
    }
}
