using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-1)]
public class PlayerInput : MonoBehaviour
{
    InputSystem_Actions inputs;
    InputAction _Move;
    InputAction _Jump;
    InputAction _Look;
    public InputAction _Light;
    public InputAction interar;

    public InputAction Move
    {
        get => _Move;
        set => _Move = value;
    }

    public Vector2 Direction => Move.ReadValue<Vector2>();

    public Vector2 LookDirection => _Look.ReadValue<Vector2>();

    public bool JumpPressed => _Jump.WasPressedThisFrame();

    private void Awake()
    {
        inputs = new InputSystem_Actions();

        Move = inputs.Player.Move;
        _Jump = inputs.Player.Jump;
        _Look = inputs.Player.Look;
        _Light = inputs.Player.Light;
        interar = inputs.Player.Interact;

        inputs.Enable();
    }

    private void OnDisable()
    {
        inputs.Disable();
    }
}