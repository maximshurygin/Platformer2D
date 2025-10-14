using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ObjectPool
{
    public class ObjectPool : MonoBehaviour, IFactory<GameObject>
    {
        [SerializeField] private GameObject _prefab;
        private List<GameObject> _objectPool = new List<GameObject>();
        private DiContainer _diContainer;

        [Inject]
        private void Construct(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        public GameObject Get()
        {
            for (int i = 0; i < _objectPool.Count; i++)
            {
                if (_objectPool[i].activeInHierarchy) continue;
                _objectPool[i].SetActive(true);
                return _objectPool[i];
            }

            GameObject newObject = Create();
            newObject.SetActive(true);
            return newObject;
        }

        public GameObject Create()
        {
            GameObject gameObject = _diContainer.InstantiatePrefab(_prefab);
            gameObject.SetActive(false);
            _objectPool.Add(gameObject);
            return gameObject;
        }
    }
}