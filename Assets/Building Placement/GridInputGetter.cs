using Singletons;
using UnityEngine;

namespace Building_Placement
{
    public class GridInputGetter : SimpleSingleton<GridInputGetter>
    {
        private Camera _mainCamera;
        
        private void Awake()
        {
            _mainCamera = Camera.main;
        }

        public Vector3 GetInput()
        {
            var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            return Physics.Raycast(ray, out var hit) ? hit.point : Vector3.zero;
        }
    }
}
