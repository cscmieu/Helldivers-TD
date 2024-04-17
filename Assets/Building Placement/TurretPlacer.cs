using Singletons;
using Turrets.Scripts.Common;
using UnityEngine;

namespace Building_Placement
{
    public class TurretPlacer : SimpleSingleton<TurretPlacer>
    {
        private Turret _turretToPlace;
        private bool   _placing;

        
        private void PlaceTurret()
        {
            DisablePlacing();
            var ok = PlacementInputGetter.Instance.GetInput(out var position);
            if (!ok) return;
            Instantiate(_turretToPlace.gameObject, position, Quaternion.identity);
        }

        public void SelectTurret(Turret newTurret)
        {
            _turretToPlace = newTurret;
        }

        public void EnablePlacing()
        {
            _placing = true;
        }
        
        public void DisablePlacing()
        {
            _placing = false;
        }

        public bool GetPlacingState()
        {
            return _placing;
        }
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && _placing)
            {
                PlaceTurret();
            }
        }
    }
}
