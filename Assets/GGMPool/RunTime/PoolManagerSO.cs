using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGMPool
{
    [CreateAssetMenu (menuName = "SO/Pool/Manager")]
    public class PoolManagerSO : ScriptableObject
    {
        public List<PoolingItemSO> poolingItemList;
        [SerializeField] private Transform _rootTrm; //풀매니징할 루트

        private Dictionary<PoolTypeSO, Pool> _pools;

        public void InitializePool(Transform root)
        {
            _rootTrm = root;
            _pools = new Dictionary<PoolTypeSO, Pool>();

            foreach(var item in poolingItemList)
            {
                IPoolable poolable = item.prefab.GetComponent<IPoolable>();
                Debug.Assert(poolable != null, $"Poolitem does not have IPoolable {item.prefab.name}");

                Pool pool = new Pool(poolable, _rootTrm, item.initCount);
                _pools.Add(item.poolType, pool);
            }
        }

        public IPoolable Pop(PoolTypeSO type)
        {
            if(_pools.TryGetValue(type, out Pool pool))
            {
                return pool.Pop();
            }
            return null;
        }

        public void Push(IPoolable item)
        {
            if(_pools.TryGetValue(item.PoolType, out Pool pool))
            {
                pool.Push(item);
            }
        }
    }
}
