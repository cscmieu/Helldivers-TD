using System.Collections;
using Singletons;
using UnityEngine;

namespace Building_Placement
{
    public class PlacementInputGetter : SimpleSingleton<PlacementInputGetter>
    {
        [SerializeField] private LayerMask  placementLayer;
        [SerializeField] private GameObject placementWarning;
        private                  Camera     _mainCamera;
        private                  Collider[] _turretCollisionDetectionBuffer;
        
        private void Awake()
        {
            _mainCamera = Camera.main;
        }

        public bool GetInput(out Vector3 inputPosition)
        {
            inputPosition = Vector3.up;
            var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(ray, out var hit, Mathf.Infinity, placementLayer)) return false;
            if (hit.collider.gameObject.layer == 9) return false;
            if (Physics.CheckBox(hit.point, Vector3.one, Quaternion.identity, 1 << 9))
            {
                StopAllCoroutines();
                StartCoroutine(DisplayWarning());
                return false;
            }
            inputPosition = hit.point;
            return true;
        }

        private IEnumerator DisplayWarning()
        {
            placementWarning.SetActive(true);
            yield return new WaitForSeconds(3f);
            placementWarning.SetActive(false);
            yield return null;
        }
    }
}
