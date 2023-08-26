using Services.PoolSystem.Abstaction;
using Services.PoolSystem.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Management.Gameplay
{
    public class SquareManager : MonoBehaviour
    {
        private const string SQUARE_OBJECT_NAME = "Square";

        private IPoolService _poolService;

        private void Start()
        {
            _poolService = PoolService.Instance;
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(1))
            {
                Square square = _poolService.GetGameObject(SQUARE_OBJECT_NAME).GetComponent<Square>();
                square.SetPosition(Input.mousePosition);
            }
        }
    }
}