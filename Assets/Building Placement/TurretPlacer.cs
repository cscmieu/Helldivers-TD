using Singletons;
using Turrets.Scripts;
using Turrets.Scripts.Common;
using UnityEngine;

namespace Building_Placement
{
    public class TurretPlacer : SimpleSingleton<TurretPlacer>
    {
        private Turret _turretToPlace;
        private bool   _placing;

        
        public void PlaceTurret()
        {
            GridManager.Instance.PlaceTurret(GridInputGetter.Instance.GetInput(),_turretToPlace, out var worldCoordinates, out var ok);
            switch (ok)
            {
                case -1:
                    Debug.Log("Square Out Of Bounds !");
                    return;
                case 1:
                    Debug.Log("Square Occupied !");
                    return;
                default:
                {
                    var instantiatedTurret = Instantiate(_turretToPlace.gameObject, worldCoordinates, Quaternion.identity);
                    DisablePlacing();
                    break;
                }
            }
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
