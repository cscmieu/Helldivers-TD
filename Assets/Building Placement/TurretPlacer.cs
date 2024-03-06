using Turrets.Scripts;
using UnityEngine;

namespace Building_Placement
{
    public class TurretPlacer : MonoBehaviour
    {
        private Turret _turretToPlace;
        
        public void PlaceTurret()
        {
            GridManager.Instance.PlaceTurret(GridInputGetter.Instance.GetInput(),_turretToPlace);
        }
    }
}
