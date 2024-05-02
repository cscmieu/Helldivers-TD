using Scoring.Scripts;
using Singletons;
using TMPro;
using Turrets.Scripts.Common;
using UnityEngine;

namespace Building_Placement
{
    public class TurretPlacer : SimpleSingleton<TurretPlacer>
    {
        [SerializeField] private TMP_Text indicationPanel;
        private                  Turret     _turretToPlace;
        private                  bool       _placing;


        private void PlaceTurret()
        {
            DisablePlacing();
            var ok = PlacementInputGetter.Instance.GetInput(out var position);
            if (!ok) return;
            Instantiate(_turretToPlace.gameObject, position, Quaternion.identity);
            MoneyManager.Instance.AddMoney(-_turretToPlace.turretData.turretCost);
        }

        public void SelectTurret(Turret newTurret)
        {
            _turretToPlace = newTurret;
        }

        public void EnablePlacing()
        {
            if (MoneyManager.Instance.GetMoney() >= _turretToPlace.turretData.turretCost)
            {
                _placing = true;
                DisplayIndication();
                return;
            }
            StopCoroutine(MoneyManager.Instance.DisplayWarning());
            StartCoroutine(MoneyManager.Instance.DisplayWarning());
        }

        private void DisablePlacing()
        {
            _placing = false;
            HideIndication();
        }

        private void DisplayIndication()
        {
            indicationPanel.gameObject.SetActive(true);
            indicationPanel.text = "Now Placing " + _turretToPlace.turretData.turretName + ",\n\nRight Click To Cancel";
        }
        
        private void HideIndication()
        {
            indicationPanel.gameObject.SetActive(false);
        }
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && _placing)
            {
                PlaceTurret();
            }
            else if (Input.GetKeyDown(KeyCode.Mouse1) && _placing)
            {
                DisablePlacing();
            }
        }
    }
}
