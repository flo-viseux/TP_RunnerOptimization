using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputs : MonoBehaviour
{
    #region API
    public delegate void OnJump();

    public static OnJump _onJump;

    public void Jump()
    {
        if (_onJump != null)
            _onJump.Invoke();
    }
    #endregion
}
