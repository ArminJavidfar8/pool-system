using Services.PoolSystem.Abstaction;
using Services.PoolSystem.Core;
using System;
using UnityEngine;

namespace Management.Gameplay
{
    public class Square : MonoBehaviour, IPoolable
    {
        private const string PREFAB_NAME = "Square";
        private IPoolService _poolService;
        public string Name => PREFAB_NAME;

        private void Start()
        {
            _poolService = PoolService.Instance;
        }

        public void Initialize()
        {

        }

        public void OnGetFromPool()
        {

        }

        public void OnReleaseToPool()
        {

        }

        public void SetPosition(Vector3 screenMousePosition)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(screenMousePosition);
            mousePosition.z = 0;
            transform.position = mousePosition;
        }

        private void OnMouseDown()
        {
            _poolService.ReleaseGameObject(gameObject);
        }
    }
}