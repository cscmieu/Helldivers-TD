using Singletons;
using Turrets.Scripts;
using UnityEngine;

namespace Building_Placement
{
    public class TurretPlacer : SimpleSingleton<TurretPlacer>
    {
        [SerializeField] private Turret turretToPlace;
        
        private                  bool   _placing;
        
        public void PlaceTurret()
        {
            GridManager.Instance.PlaceTurret(GridInputGetter.Instance.GetInput(),turretToPlace, out var worldCoordinates, out var ok);
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
                    var instantiatedTurret = Instantiate(turretToPlace.gameObject, worldCoordinates, Quaternion.identity);
                    DisablePlacing();
                    break;
                }
            }
        }

        public void SelectTurret(Turret newTurret)
        {
            turretToPlace = newTurret;
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
