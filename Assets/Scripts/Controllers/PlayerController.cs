using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "PlayerController", menuName = "InputController/PlayerController")]
public class PlayerController : InputController
{
    public override bool RetrieveJumpInput(InputAction.CallbackContext context)
    {
        return context.performed;
    }

    public override float RetrieveMoveInput(InputAction.CallbackContext context)
    {
        return context.ReadValue<Vector2>().x;
    }
}
