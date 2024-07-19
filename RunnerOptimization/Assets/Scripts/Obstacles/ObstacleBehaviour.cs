using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers; 

[RequireComponent(typeof(Rigidbody2D))]
public class ObstacleBehaviour : MonoBehaviour
{
    [SerializeField] private float[] _PossibleSpawnHeights;
    [SerializeField] private bool _IsTrigger;
    private Rigidbody2D _Rb;
    private ObstacleManager _ObstacleManager;
    
    private void Awake()
    {
        _Rb = GetComponent<Rigidbody2D>();
        _ObstacleManager = ObstacleManager.Instance;
    }

    private void OnEnable()
    {
        int rdInt = Random.Range(0, _PossibleSpawnHeights.Length);
        transform.position = new Vector2(_ObstacleManager.ObstacleSpawnPosX, _PossibleSpawnHeights[rdInt]);
        StartCoroutine(Release());
    }

    private IEnumerator Release()
    {
        yield return new WaitUntil(() => transform.position.x < -15);

        Debug.Log("Release");

        _ObstacleManager.Pool.Release(this);
    }
    
    private void FixedUpdate()
    {
        float speed = Time.deltaTime * _ObstacleManager.ObstacleSpeed;
        _Rb.velocity = Vector2.left * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_IsTrigger) return;

        if (collision.collider.CompareTag("Player"))
        {
            PlayerController.Instance.Hit();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_IsTrigger) return;
        if (collision.CompareTag("Player"))
        {
            PlayerController.Instance.Hit();
        }
    }
}
