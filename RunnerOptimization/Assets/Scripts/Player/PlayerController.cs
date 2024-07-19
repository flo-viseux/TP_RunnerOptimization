using System.Collections;
using UnityEngine;

namespace Managers
{
    public class PlayerController : Singleton<PlayerController> 
    {
        #region SerializedFields
        [Header("Score")]
        [SerializeField] private float scoreMultiplicatorCoeff = 1f;

        [Header("Jump")]
        [SerializeField] private Vector2 jumpForce = new Vector2(0, 5f);

        [SerializeField] private GameObject _GameOverCanvas;
        #endregion

        #region Attributes
        private float score = 0;

        private bool isGrounded = true;
        private bool isJumping = false;

        private Rigidbody2D rb = null;
        private PlayerHealth _PlayerHealth = null;
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

        public void Hit()
        {
            _onHit?.Invoke();

            if (_PlayerHealth.LifeCount < 1)
            {
                _onGameOver?.Invoke();
                _GameOverCanvas.SetActive(true);
                Time.timeScale = 0;
            }
        }
        #endregion

        #region UnityMethods
        private void Start()
        {
            transform.position = new Vector3(Camera.main.ViewportToWorldPoint(Vector3.zero).x + 3, transform.position.y, 0); // Set Player Pos with Camera

            PlayerInputs._onJump += Jump;
            SetIsGrounded(true);
            _PlayerHealth = GetComponent<PlayerHealth>();
            rb = GetComponent<Rigidbody2D>();

            StartCoroutine(IncreaseScoreCoroutine());
        }

        private IEnumerator IncreaseScoreCoroutine()
        {
            yield return new WaitForEndOfFrame();

            IncreaseScore();

            StartCoroutine(IncreaseScoreCoroutine());
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

            _onLand?.Invoke();
            isJumping = false;
        }

        private void Jump()
        {
            if (!isGrounded || isJumping)
                return;

            isJumping = true;
            SetIsGrounded(false);
            rb.AddForce(jumpForce, ForceMode2D.Impulse);
            _onJump?.Invoke();
        }

        private void IncreaseScore()
        {
            score += scoreMultiplicatorCoeff * Time.deltaTime;

            _onIncreaseScore?.Invoke(score);
        }
        #endregion
    }
}
