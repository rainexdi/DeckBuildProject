using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(Rigidbody2D))]

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    public  Vector2 movementInput;
    private Rigidbody2D _rb;
    private PlayerAttack playerAttack;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        MovementInput();

    }

    private void FixedUpdate()
    {
        _rb.linearVelocity = movementInput * moveSpeed;
    }

    private void MovementInput()
    {
        movementInput = new Vector2(0, 0);
        if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed) movementInput.y = 1;
        if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed) movementInput.y = -1;
        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) movementInput.x = -1;
        if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) movementInput.x = 1;

        //if (movementInput.x != 0) movementInput.y = 0;
        //if (movementInput.y != 0) movementInput.x = 0;
    }

}
