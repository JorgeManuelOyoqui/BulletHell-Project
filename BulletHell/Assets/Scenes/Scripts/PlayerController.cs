using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 12f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public GameObject crouchVisual; // opcional: para mostrar que está agachado

    private Rigidbody2D rb;
    private PlayerControls controls;
    private Vector2 moveInput;
    private bool isGrounded;
    private bool isCrouching;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        controls = new PlayerControls();

        controls.Gameplay.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.Gameplay.Move.canceled += ctx => moveInput = Vector2.zero;

        controls.Gameplay.SlowMode.performed += ctx => moveSpeed = 2f;
        controls.Gameplay.SlowMode.canceled += ctx => moveSpeed = 5f;

        controls.Gameplay.Jump.performed += ctx => TryJump();

        controls.Gameplay.Crouch.performed += ctx => isCrouching = true;
        controls.Gameplay.Crouch.canceled += ctx => isCrouching = false;
    }

    void OnEnable() => controls.Gameplay.Enable();
    void OnDisable() => controls.Gameplay.Disable();

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);

        // Movimiento horizontal
        rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);

        // Agacharse
        if (isCrouching && isGrounded)
        {
            if (crouchVisual != null) crouchVisual.SetActive(true);
        }
        else
        {
            if (crouchVisual != null) crouchVisual.SetActive(false);
        }
    }

    void TryJump()
    {
        if (isGrounded && !isCrouching)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }
}