using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRenderer : MonoBehaviour
{
    #region Constants
    private static int jumpHash = Animator.StringToHash("jump");
    private static int hitHash = Animator.StringToHash("hit");
    private static int faintHash = Animator.StringToHash("faint");
    private static int landHash = Animator.StringToHash("land");
    #endregion

    #region SerializedFields
    [SerializeField] private Animator animator;
    #endregion

    #region UnityMethods
    private void Start()
    {
        PlayerController._onHit += Hit;
        PlayerController._onJump += Jump;
        PlayerController._onGameOver += Faint;
        PlayerController._onLand += Land;
    }
    #endregion

    #region Private
    private void Jump()
    {
        animator.ResetTrigger(landHash);
        animator.SetTrigger(jumpHash);
    }

    private void Hit()
    {
        animator.SetTrigger(hitHash);
    }

    private void Faint()
    {
        animator.SetTrigger(faintHash);
    }

    private void Land()
    {
        animator.SetTrigger(landHash);
    }
    #endregion
}
