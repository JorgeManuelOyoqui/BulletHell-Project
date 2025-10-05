using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 12f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public GameObject crouchVisual;
    public GameObject shieldVisual; // Sprite del escudo

    private Rigidbody2D rb;
    private PlayerControls controls;
    private Vector2 moveInput;
    private bool isGrounded;
    private bool isCrouching;

    public int maxHealth = 3;
    public int CurrentHealth { get; private set; }

    public int maxShield = 10;
    public int currentShield;
    public bool shieldActive = true;
    public float shieldCooldown = 10f;
    public float shieldTimer;
    public GameObject shieldCooldownIndicator;

    private bool isInvulnerable = false;
    private float invulnerabilityDuration = 1f;
    
    private Animator animator;

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

    void Start()
    {
        animator = GetComponent<Animator>();
        CurrentHealth = maxHealth;
        currentShield = maxShield;
        shieldActive = true;
        shieldTimer = 0f;

        UpdateShieldVisual();
        shieldCooldownIndicator.SetActive(false);
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
        rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);

        animator.SetBool("isRunning", Mathf.Abs(moveInput.x) > 0.1f);
        animator.SetBool("isJumping", !isGrounded);

        // Voltear sprite según dirección
        if (moveInput.x > 0.1f)
        {
            transform.localScale = new Vector3(0.3298527f, 0.3298527f, 0.3298527f); // Normal
        }
        else if (moveInput.x < -0.1f)
        {
            transform.localScale = new Vector3(-0.3298527f, 0.3298527f, 0.3298527f); // Reflejado horizontalmente
        }

        if (isCrouching && isGrounded)
        {
            if (crouchVisual != null) crouchVisual.SetActive(true);
        }
        else
        {
            if (crouchVisual != null) crouchVisual.SetActive(false);
        }

        // Regeneración del escudo
        if (!shieldActive)
        {
            shieldTimer += Time.deltaTime;
            if (shieldTimer >= shieldCooldown)
            {
                currentShield = maxShield;
                shieldActive = true;
                shieldTimer = 0f;
                UpdateShieldVisual();
                GameManager.Instance.sfxManager.PlayShieldRegen();

                // Desactivar el indicador visual
                shieldCooldownIndicator.SetActive(false);

            }
        }
    }

    public void TakeDamage(int damage)
    {
        if (isInvulnerable) return;

        if (shieldActive)
        {
            currentShield -= damage;
            if (currentShield <= 0)
            {
                shieldActive = false;
                shieldTimer = 0f;
                UpdateShieldVisual();
                GameManager.Instance.sfxManager.PlayShieldBreak();
                
                shieldCooldownIndicator.SetActive(true);
                shieldCooldownIndicator.GetComponent<Animator>().Play("ShieldCooldownProgress");
            }
        }
        else
        {
            CurrentHealth -= damage;
            GameManager.Instance.sfxManager.PlayPlayerHit();
            StartCoroutine(ApplyInvulnerabilityWithFlicker());

            if (CurrentHealth <= 0)
            {
                Die();
            }
        }
    }

    void ApplyKnockback()
    {
        Vector2 knockbackDir = new Vector2(-moveInput.x, 1).normalized;
        rb.AddForce(knockbackDir * 300f); // Ajusta la fuerza según lo que se sienta bien
    }

    System.Collections.IEnumerator ApplyInvulnerabilityWithFlicker()
    {
        isInvulnerable = true;
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        float elapsed = 0f;
        float flickerRate = 0.1f;

        while (elapsed < invulnerabilityDuration)
        {
            if (sr != null)
            {
                sr.enabled = !sr.enabled; // Alterna visibilidad
            }
            yield return new WaitForSeconds(flickerRate);
            elapsed += flickerRate;
        }

        if (sr != null)
        {
            sr.enabled = true; // Asegura que quede visible al final
        }

        isInvulnerable = false;
    }

    void UpdateShieldVisual()
    {
        if (shieldVisual != null)
        {
            shieldVisual.SetActive(shieldActive);
        }
    }

    void TryJump()
    {
        if (isGrounded && !isCrouching)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    void Die()
    {
        Debug.Log("¡Derrota! El jugador ha muerto.");
        GameManager.Instance.ShowDefeat();
        gameObject.SetActive(false);
        shieldCooldownIndicator.SetActive(false);
    }
}

