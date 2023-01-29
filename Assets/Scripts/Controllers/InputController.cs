using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public abstract class InputController : ScriptableObject
{

    public abstract float RetrieveMoveInput(InputAction.CallbackContext context);
    public abstract bool RetrieveJumpInput(InputAction.CallbackContext context);
}
