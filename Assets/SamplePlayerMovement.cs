using UnityEngine;
using UnityEngine.InputSystem;

public class SamplePlayerMovement : MonoBehaviour
{

    public InputAction moveAction;
    public InputAction jumpAction;

    private int moveDirection = 0;
    public float moveSpeed;
    public float jumpForce;

    private Rigidbody2D rb;


    private void OnEnable()
    {
        moveAction.Enable();
        jumpAction.Enable();

        moveAction.performed += Move_Performed;
        moveAction.canceled += Move_Performed;

        jumpAction.performed += Jump_Performed;
        rb= GetComponent<Rigidbody2D>();    
    }

    private void Move_Performed(InputAction.CallbackContext c)
    {
        moveDirection = (int)c.ReadValue<float>();
    }

    private void Jump_Performed(InputAction.CallbackContext c)
    {
        rb.AddForce(Vector2.up * jumpForce);

    }

    private void FixedUpdate()
    {
        rb.linearVelocityX = moveDirection * moveSpeed;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
  
}
