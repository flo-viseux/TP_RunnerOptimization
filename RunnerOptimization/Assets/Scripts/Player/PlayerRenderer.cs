using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRenderer : MonoBehaviour
{
    #region Constants
    private static int jumpHash = Animator.StringToHash("jump");
    private static int hitHash = Animator.StringToHash("hit");
    private static int faintHash = Animator.StringToHash("faint");
    private static int isGroundedHash = Animator.StringToHash("isGrounded");
    #endregion

    #region SerializedFields
    [SerializeField] private Animator animator;
    #endregion

    #region API
    public void Jump()
    {
        animator.SetTrigger(jumpHash);
    }

    public void Hit()
    {
        animator.SetTrigger(hitHash);
    }

    public void Faint()
    {
        animator.SetTrigger(faintHash);
    }

    public void SetIsGrounded(bool IsGrounded)
    {
        animator.SetBool(isGroundedHash, IsGrounded);
    }
    #endregion
}
