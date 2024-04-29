using Singletons;
using UnityEngine;

namespace Map
{
    public class CameraMover : SimpleSingleton<CameraMover>
    {
        public  float  rotationSpeed = 100f;
        private Camera _mainCamera;

        private void Awake()
        {
            _mainCamera = Camera.main;
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.E))
            {
                _mainCamera.transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.Q))
            {
                _mainCamera.transform.Rotate(Vector3.forward, -rotationSpeed * Time.deltaTime);
            }
        }
    }
}
