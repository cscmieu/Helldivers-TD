using Turrets.Scripts;
using UnityEngine;

namespace Building_Placement
{
    public class TurretPlacer : MonoBehaviour
    {
        [SerializeField] private Turret turretToPlace;
        
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
                    break;
                }
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                PlaceTurret();
            }
        }
    }
}
