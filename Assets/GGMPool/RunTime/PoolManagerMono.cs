using UnityEngine;

namespace GGMPool
{
    public class PoolManagerMono : MonoBehaviour
    {
        [SerializeField] private PoolManagerSO _poolManager;

        private void Awake()
        {
            _poolManager.InitializePool(transform);
        }
    }
}
