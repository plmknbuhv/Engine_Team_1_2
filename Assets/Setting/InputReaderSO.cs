using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static Controls;

[CreateAssetMenu(menuName = "SO/InputReaderSO")]
public class InputReaderSO : ScriptableObject, IPlayerActions
{
    private Controls _controls;
    public Vector3 Movement {get; private set;} 
    public Vector2 MousePos {get; private set;} 
    
    public event Action<bool> OnLeftClickEvent; 

    private void OnEnable()
    {
        _controls = new Controls();
        _controls.Player.SetCallbacks(this);
        _controls.Player.Enable();
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        var input = context.ReadValue<Vector2>();
        Movement = new Vector3(input.x, 0, input.y);
    }

    public void OnMouse(InputAction.CallbackContext context)
    {
        MousePos = context.ReadValue<Vector2>();
    }

    public void OnForwardBackward(InputAction.CallbackContext context)
    {
        Movement = new Vector3(Movement.x, context.ReadValue<float>(), Movement.z);
    }

    public void OnRightClick(InputAction.CallbackContext context)
    {
        if (context.performed)
            OnLeftClickEvent?.Invoke(true);
        else if (context.canceled)
            OnLeftClickEvent?.Invoke(false);
    }
}
