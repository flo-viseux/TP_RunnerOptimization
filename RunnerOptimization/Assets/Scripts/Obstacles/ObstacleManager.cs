using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public partial class ObstacleManager : MonoBehaviour
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

    [SerializeField] private float _ObstacleSpawnPosX;
    public float ObstacleSpawnPosX { get => _ObstacleSpawnPosX; set => _ObstacleSpawnPosX = value; }

    //ObstacleSpeed
    [Tooltip("The base speed that is always applied, non dependant of time")]
    [SerializeField] private float _ObstacleBaseSpeed = 3;
    [Tooltip("The multiplier of the evolution curve on time")]
    [SerializeField] private float _ObstacleEvolutionSpeed = 1;
    [Tooltip("The evolution Curve of the speed multiplier based on time passed")]
    [SerializeField] private AnimationCurve _ObstacleSpeedCurve;
    [Tooltip("How much time it takes for the speed to reach its highestValue")]
    [SerializeField] private float _TimeToReachMaxSpeed = 200;
    private float _ObstacleSpeed;
    public float ObstacleSpeed { get => _ObstacleSpeed; set => _ObstacleSpeed = value; }


    //Obstacle Spawn Frequency
    [Tooltip("The base spawn rate that is multiplied by the evolution curve evaluation")]
    [SerializeField] private float _ObstacleBaseSpawnTimer = 3;
    [Tooltip("The evolution Curve of the speed multiplier based on time passed")]
    [SerializeField] private AnimationCurve _ObstacleSpawnTimerCurve;
    [Tooltip("How much time it takes for the speed to reach its highestValue")]
    [SerializeField] private float _TimeToReachMinSpawnTimer = 200;
    private float _ObstacleSpawnTimer;

    //Pooling 
    [SerializeField] private List<ObstaclePoolElement> _ObstaclePoolElements;
    [SerializeField] private Transform _PoolFolder;
    [SerializeField] private Transform _PoolActiveFolder;
    private PoolingSystem<ObstacleBehaviour, EObstacleTypes> _Pool;
    public PoolingSystem<ObstacleBehaviour, EObstacleTypes> Pool { get => _Pool; set => _Pool = value; }

    private void Start()
    {
        StartCoroutine(ObstacleSpeedEvolution());
        StartCoroutine(ObstacleSpawnRateEvolution());
        GeneratePool();
        StartCoroutine(ObstacleSpawner());
    }

    IEnumerator ObstacleSpeedEvolution()
    {
        float interpolater = 0;
        while (interpolater < _TimeToReachMaxSpeed)
        {
            interpolater += Time.deltaTime;

            yield return new WaitForEndOfFrame();

            _ObstacleSpeed = _ObstacleSpawnTimerCurve.Evaluate(interpolater / _TimeToReachMaxSpeed) * _ObstacleEvolutionSpeed + _ObstacleBaseSpeed;
        }
    }

    private void GeneratePool()
    {
        Dictionary<EObstacleTypes, Tuple<ObstacleBehaviour, int>> poolPrefabDictionary = new Dictionary<EObstacleTypes, Tuple<ObstacleBehaviour, int>>();
        foreach(var element in _ObstaclePoolElements)
        {
            poolPrefabDictionary.Add(element.Type, new Tuple<ObstacleBehaviour, int>(element.ObstaclePrefab, element.BaseNumberOfElements));
        }
        _Pool = new PoolingSystem<ObstacleBehaviour, EObstacleTypes>(poolPrefabDictionary, _PoolFolder, _PoolActiveFolder);
    }

    IEnumerator ObstacleSpawnRateEvolution()
    {
        float interpolater = 0;
        while (interpolater < _TimeToReachMinSpawnTimer)
        {
            interpolater += Time.deltaTime;

            yield return new WaitForEndOfFrame();

            _ObstacleSpawnTimer = _ObstacleSpawnTimerCurve.Evaluate(interpolater / _TimeToReachMinSpawnTimer) *  _ObstacleBaseSpawnTimer;
        }
    }

    IEnumerator ObstacleSpawner()
    {
        //Replace true by gameover condition
        while (true)
        {
            yield return new WaitForSeconds(_ObstacleSpawnTimer);
            EObstacleTypes type = (EObstacleTypes)UnityEngine.Random.Range(0, System.Enum.GetValues(typeof(EObstacleTypes)).Length);
            _Pool.Get(type, new Vector3(10, 0, 0));
        }
    }
}
