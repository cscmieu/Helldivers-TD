using System.Collections.Generic;
using Building_Placement;
using Singletons;
using UnityEngine;

namespace Turrets.Scripts
{
    public class TurretInventory : SimpleSingleton<TurretInventory>
    {
        [SerializeField] private List<Turret> inventory;


        public void UpdateTurretToPlace(int turretIndex)
        {
            TurretPlacer.Instance.SelectTurret(inventory[turretIndex]);
        }
    }
}
