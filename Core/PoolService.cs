using Services.PoolSystem.Abstaction;
using Services.PoolSystem.Data;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;

namespace Services.PoolSystem.Core
{
    public class PoolService : IPoolService
    {
        private Dictionary<string, GameObject> _prefabs;
        private List<GameObject> _poolList;
        private IServiceProvider _serviceProvider;

        [Preserve]
        public PoolService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _prefabs = new Dictionary<string, GameObject>();
            _poolList = new List<GameObject>();
            FillPrefabs();
        }

        private void FillPrefabs()
        {
            GameObject[] gameObjects = Resources.Load<PoolData>("PoolSystem/PoolData").Prefabs;
            foreach (GameObject item in gameObjects)
            {
                if (item.TryGetComponent<IPoolable>(out IPoolable poolable) && !_prefabs.ContainsKey(poolable.Name))
                {
                    _prefabs.Add(poolable.Name, item);
                }
            }
        }
        
        public GameObject GetGameObject(string name)
        {
            foreach (GameObject gameObject in _poolList)
            {
                if (gameObject.name.StartsWith(name) && gameObject.activeSelf == false)
                {
                    gameObject.SetActive(true);
                    gameObject.GetComponent<IPoolable>().OnGetFromPool();
                    return gameObject;
                }
            }
            if (_prefabs.TryGetValue(name, out GameObject prefab))
            {
                GameObject gameObject = GameObject.Instantiate(prefab);
                _poolList.Add(gameObject);
                gameObject.SetActive(true);
                IPoolable poolable = gameObject.GetComponent<IPoolable>();
                poolable.Initialize(_serviceProvider);
                poolable.OnGetFromPool();
                return gameObject;
            }
            throw new Exception($"No GameObject found with name {name} in pool");
        }

        public void ReleaseGameObject(GameObject gameObject)
        {
            gameObject.SetActive(false);
            gameObject.GetComponent<IPoolable>().OnReleaseToPool();
        }
    }
}