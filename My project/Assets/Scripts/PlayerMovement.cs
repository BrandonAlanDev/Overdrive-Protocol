using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Camera mainCamera;

    private Vector2 movementInput;
    private Vector3 moveDirection;

    private Rigidbody rb;
    private PlayerInputActions inputActions;
    private Animator animator;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        animator = GetComponent<Animator>();

        inputActions = new PlayerInputActions();
        inputActions.Player.Move.performed += ctx => movementInput = ctx.ReadValue<Vector2>();
        inputActions.Player.Move.canceled += ctx => movementInput = Vector2.zero;
    }

    void OnEnable() => inputActions.Enable();
    void OnDisable() => inputActions.Disable();

    void FixedUpdate()
    {
        Move();
        RotateToMouse();
        UpdateAnimation();
        rb.AddForce(Vector3.down * 90f, ForceMode.Acceleration);
    }

    void Move()
    {
        moveDirection = new Vector3(movementInput.x, 0, movementInput.y);
        rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime);
    }

    void RotateToMouse()
    {
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit hit, 100f, LayerMask.GetMask("Ground")))
        {
            Vector3 lookDir = hit.point - transform.position;
            lookDir.y = 0f;
            if (lookDir.sqrMagnitude > 0.01f)
            {
                Quaternion rotation = Quaternion.LookRotation(lookDir);
                rb.MoveRotation(rotation);
            }
        }
    }

    void UpdateAnimation()
    {
        float speed = moveDirection.magnitude;
        float direction = Vector3.Dot(transform.forward, moveDirection.normalized);

        animator.SetFloat("Speed", speed);
        animator.SetFloat("Direction", direction);
    }

}
