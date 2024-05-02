using Singletons;
using TMPro;
using UnityEngine;

namespace Map
{
    public class CameraMover : SimpleSingleton<CameraMover>
    {
        public  float     rotationSpeed = 100f;
        public  float     moveSpeed     = 10f;
        private Vector3   _movement;
        private Camera    _mainCamera;
        private Transform _mainCameraTransform;

        private void Awake()
        {
            _mainCamera = Camera.main;
            if (_mainCamera != null) _mainCameraTransform = _mainCamera.transform;
        }

        private void Update()
        {
        #region Rotation
            if (Input.GetKey(KeyCode.E))
            {
                _mainCameraTransform.Rotate(Vector3.forward, -rotationSpeed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.Q))
            {
                _mainCameraTransform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
            }
        #endregion

        #region Movement

            _movement = new Vector3();
            if (Input.GetKey(KeyCode.W))
            {
                _movement += _mainCameraTransform.up;
            }
            
            if (Input.GetKey(KeyCode.S))
            {
                _movement -= _mainCameraTransform.up;
            }
            
            if (Input.GetKey(KeyCode.D))
            {
                _movement += _mainCameraTransform.right;
            }
            
            if (Input.GetKey(KeyCode.A))
            {
                _movement -= _mainCameraTransform.right;
            }
            
            _movement.Normalize();
            _movement *= moveSpeed * Time.deltaTime;

            _mainCameraTransform.position += _movement;

        #endregion
        }
    }
}