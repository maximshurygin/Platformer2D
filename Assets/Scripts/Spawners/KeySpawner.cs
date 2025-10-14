using UnityEngine;

namespace Spawners
{
    public class KeySpawner : MonoBehaviour
    {
        [SerializeField] private ObjectPool.ObjectPool _objectPool;

        public void SpawnObject(Vector3 position)
        {
            GameObject obj = _objectPool.Get();
            obj.transform.position = position;
            obj.transform.SetParent(transform);
        }
    }
}