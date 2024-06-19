using UnityEngine;

namespace Managers
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance {  get; private set; }

        protected virtual void Awake()
        {
            if (Instance != null)
                Destroy(this);
            else Instance = this as T;
        }
    }
}
