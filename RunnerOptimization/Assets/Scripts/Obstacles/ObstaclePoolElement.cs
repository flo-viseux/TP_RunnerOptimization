public partial class ObstacleManager
{
    [System.Serializable]
    struct ObstaclePoolElement
    {
        public EObstacleTypes Type;
        public int BaseNumberOfElements;
        public ObstacleBehaviour ObstaclePrefab;
    }
}
