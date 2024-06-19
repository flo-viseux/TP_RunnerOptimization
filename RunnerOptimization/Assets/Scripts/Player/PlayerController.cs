using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region SerializedFields
    [Header("References")]
    [SerializeField] private PlayerRenderer renderer = null;

    [SerializeField] private PlayerHealth health = null;

    [SerializeField] private PlayerScore score = null;

    [Header("Self")]

    [Header("Jump")]
    [SerializeField] private Rigidbody2D rb = null;
    [SerializeField] private Vector2 jumpForce = new Vector2(0, 5f);

    [SerializeField] private Vector2 offset = Vector2.zero;
    [SerializeField] private float distance = 0.1f;
    [SerializeField] private LayerMask layerMask;
    #endregion

    #region Attributes
    private bool isGrounded = true;
    private bool isJumping = true;
    #endregion

    #region API
    public static PlayerController Instance = null;

    public void Hit()
    {
        health.DecreseLifeCount();
        renderer.Hit();
    }
    #endregion

    #region UnityMethods
    private void Start()
    {
        transform.position = new Vector3(Camera.main.ViewportToWorldPoint(Vector3.zero).x + 2, transform.position.y, 0); // Set Player Pos with Camera

        PlayerInputs._onJump += Jump;
        SetIsGrounded(true);
    }

    private void FixedUpdate()
    {
        if (!isGrounded && Physics2D.Raycast((Vector2)transform.position + offset, Vector2.down, distance, layerMask))
            SetIsGrounded(true);
        else if (isGrounded && !Physics2D.Raycast((Vector2)transform.position + offset, Vector2.down, distance, layerMask))
            SetIsGrounded(false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay((Vector2)transform.position + offset, Vector2.down * distance);
    }
    #endregion

    #region Private
    private void SetIsGrounded(bool IsGrounded)
    {
        this.isGrounded = IsGrounded;
        renderer.SetIsGrounded(this.isGrounded);

        if (IsGrounded)
            isJumping = false;
    }

    private void Jump()
    {
        if (!isGrounded || isJumping)
            return;

        isJumping = true;
        rb.AddForce(jumpForce, ForceMode2D.Impulse);
        renderer.Jump();
    }
    #endregion
}
