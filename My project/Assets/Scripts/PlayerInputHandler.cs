using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerInputActions inputActions;
    public Vector2 MoveInput { get; private set; }
    public Vector2 MousePosition { get; private set; }
    public bool IsShooting { get; private set; }

    void Awake()
    {
        inputActions = new PlayerInputActions();
        inputActions.Player.Enable();

        inputActions.Player.Move.performed += ctx => MoveInput = ctx.ReadValue<Vector2>();
        inputActions.Player.Move.canceled += ctx => MoveInput = Vector2.zero;

        inputActions.Player.Look.performed += ctx => MousePosition = ctx.ReadValue<Vector2>();
        inputActions.Player.Look.canceled += ctx => MousePosition = Vector2.zero;

        inputActions.Player.Shoot.performed += ctx => IsShooting = true;
        inputActions.Player.Shoot.canceled += ctx => IsShooting = false;

        // Los otros eventos se pueden conectar luego: Overdrive, Heal, Pause
    }

    void Update()
    {
        // Ejemplo de uso
        if (IsShooting)
        {
            Debug.Log("Disparando!");
        }
    }

    private void OnDisable()
    {
        inputActions.Player.Disable();
    }
}
