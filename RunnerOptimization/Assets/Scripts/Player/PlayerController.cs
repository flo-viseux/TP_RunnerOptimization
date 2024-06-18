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
    #endregion

    #region API
    public PlayerController Instance = null;

    public bool IsGrounded = true;

    public void Hit()
    {
        health.DecreseLifeCount();
        renderer.Hit();
    }
    #endregion

    #region UnityMethods
    private void Awake()
    {
        PlayerInputs._onJump += Jump;
        IsGrounded = true;
    }

    private void FixedUpdate()
    {
        if (!IsGrounded && Physics.Raycast((Vector2)transform.position + offset, Vector3.down, distance))
            SetIsGrounded(true);
        else if (IsGrounded && !Physics.Raycast((Vector2)transform.position + offset, Vector3.down, distance))
            SetIsGrounded(false);
    }
    #endregion

    #region Private
    private void SetIsGrounded(bool isGrounded)
    {
        IsGrounded = isGrounded;

    }

    private void Jump()
    {
        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast((Vector2)transform.position + offset, Vector3.down, out hit, distance))
        {
            rb.AddForce(jumpForce, ForceMode2D.Impulse);
            renderer.Jump();
        }

    }
    #endregion
}
