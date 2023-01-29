using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "AIController", menuName = "InputController/AIController")]
public class AIController : InputController
{
    public override bool RetrieveJumpInput(InputAction.CallbackContext context)
    {
        return true;
    }

    public override float RetrieveMoveInput(InputAction.CallbackContext context)
    {
        return 1f;
    }
}
