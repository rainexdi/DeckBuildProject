using UnityEngine;
using UnityEngine.InputSystem;

public class DrawCardTestSystem : MonoBehaviour
{
    [SerializeField] private HandView handView;
    private void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            if (!handView.hasSpace)
            {
                return;
            }

            CardView cardView = CardViewCreator.Instance.CreateCardView(Vector3.zero, Quaternion.identity);
            StartCoroutine(handView.AddCard(cardView));
        }
    }

}
