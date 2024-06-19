using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObstacleManager : MonoBehaviour
{
    public static ObstacleManager Instance;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
            Debug.LogError($"There is already an instance of {this.GetType()}.");
        }
    }

    [Tooltip("The base speed that is always applied, non dependant of time")]
    [SerializeField] private float _ObstacleBaseSpeed = 3;
    [Tooltip("The multiplier of the evolution curve on time")]
    [SerializeField] private float _ObstacleEvolutionSpeed = 1;
    [Tooltip("The evolution Curve of the speed multiplier based on time passed")]
    [SerializeField] private AnimationCurve _ObstacleSpeedCurve;
    [Tooltip("How much time it takes for the speed to reach its highestValue")]
    [SerializeField] private float _TimeToReachMaxSpeed = 200;
    private float _ObstacleSpeed;
    public float ObstacleSpeed { get; private set; }
    


    private IEnumerator Start()
    {
        float interpolater = 0;
        while(interpolater < _TimeToReachMaxSpeed)
        {
            _TimeToReachMaxSpeed += Time.deltaTime;

            yield return new WaitForEndOfFrame();

            _ObstacleSpeed = _ObstacleSpeedCurve.Evaluate(interpolater / _TimeToReachMaxSpeed) * _ObstacleEvolutionSpeed + _ObstacleSpeed;
        }
    }


    IObjectPool<ObstacleBehaviour> m_Pool;
    public IObjectPool<ObstacleBehaviour> Pool
    {
        get
        {
            if (m_Pool == null)
            {
                m_Pool = new LinkedPool<ObstacleBehaviour>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, collectionChecks, maxPoolSize);
            }
            return m_Pool;
        }
        
    }






}
