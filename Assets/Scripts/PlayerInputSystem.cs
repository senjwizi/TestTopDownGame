using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputSystem : MonoBehaviour
{
    PlayerMovement playerMovement;
    PlayerAttack playerAttack;

    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerAttack = GetComponent<PlayerAttack>();
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        playerAttack.shooting = context.ReadValueAsButton();
    }

    public void OnMove(InputAction.CallbackContext callback)
    {
        playerMovement.Move(callback.ReadValue<Vector2>());
    }
}
