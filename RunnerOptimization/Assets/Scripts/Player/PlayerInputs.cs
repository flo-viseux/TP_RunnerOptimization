using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInputs : MonoBehaviour
{
    #region API
    public delegate void OnJump();

    public static OnJump _onJump;

    public void Jump(CallbackContext cbx)
    {
        if (cbx.performed && _onJump != null)
            _onJump.Invoke();
    }
    #endregion
}
