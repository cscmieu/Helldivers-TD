using Building_Placement;
using UnityEngine;

namespace Turrets.Scripts
{
    public class Turret : GridItem
    {
        private GameObject _turretPrefab;

        private void Awake()
        {
            _turretPrefab = gameObject;
        }

        public GameObject Object()
        {
            return _turretPrefab;
        }
    }
}
