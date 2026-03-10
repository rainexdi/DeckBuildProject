using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(Rigidbody2D))]

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] PlayerStatsSO playerStats;   
    private  Vector2 movementInput;
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
        _rb.linearVelocity = movementInput * playerStats.moveSpeed;
    }

    private void MovementInput()
    {
        // Defines where the player wants to move based on the input. This example uses WASD and arrow keys for movement
        movementInput = new Vector2(0, 0);
        if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed) movementInput.y = 1;
        if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed) movementInput.y = -1;
        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) movementInput.x = -1;
        if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) movementInput.x = 1;

        // Following code depends on wheter you want to allow diagonal movement or not. If you want to allow diagonal movement, comment out the following lines. If you want to restrict movement to only one direction at a time, uncomment the following lines.
        //if (movementInput.x != 0) movementInput.y = 0;
        //if (movementInput.y != 0) movementInput.x = 0;
    }

}
