using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region SerializedFields
    [Header("Score")]
    [SerializeField] private float scoreMultiplicatorCoeff = 1f;

    [Header("Jump")]
    [SerializeField] private Rigidbody2D rb = null;
    [SerializeField] private Vector2 jumpForce = new Vector2(0, 5f);
    #endregion

    #region Attributes
    private int lifeCount = 3;
    private float score = 0;

    private bool isGrounded = true;
    #endregion

    #region Delegates
    // Jump
    public delegate void OnJump();
    public static OnJump _onJump;

    public delegate void OnLand();
    public static OnLand _onLand;

    // Score
    public delegate void OnIncreaseScore(float score);
    public static OnIncreaseScore _onIncreaseScore;

    // Health
    public delegate void OnHit();
    public static OnHit _onHit;

    public delegate void OnGameOver();
    public static OnGameOver _onGameOver;
    #endregion

    #region API
    public static PlayerController Instance = null;

    public void Hit()
    {
        --lifeCount;

        if (lifeCount > 0)
        {
            if (_onHit != null)
                _onHit.Invoke();
        }
        else
        {
            GameOver();
        }

    }
    #endregion

    #region UnityMethods
    private void Start()
    {
        lifeCount = 3;

        transform.position = new Vector3(Camera.main.ViewportToWorldPoint(Vector3.zero).x + 2, transform.position.y, 0); // Set Player Pos with Camera

        PlayerInputs._onJump += Jump;
        SetIsGrounded(true);
    }

    private void Update()
    {
        IncreaseScore();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isGrounded || collision.gameObject.layer != 3)
            return;

        SetIsGrounded(true);
    }
    #endregion

    #region Private
    private void SetIsGrounded(bool IsGrounded)
    {
        this.isGrounded = IsGrounded;

        if (!IsGrounded)
            return;

        if (_onLand != null)
            _onLand.Invoke();
    }

    private void Jump()
    {
        if (!isGrounded)
            return;

        SetIsGrounded(false);
        rb.AddForce(jumpForce, ForceMode2D.Impulse);

        if (_onJump != null)
            _onJump.Invoke();
    }

    private void IncreaseScore()
    {
        score += scoreMultiplicatorCoeff * Time.deltaTime;

        if (_onIncreaseScore != null)
            _onIncreaseScore(score);
    }

    private void GameOver()
    {
        if (_onGameOver != null)
            _onGameOver.Invoke();

        enabled = false;
        Time.timeScale = 0;
    }
    #endregion
}
