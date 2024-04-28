using Scoring.Scripts;
using Turrets.Scripts.Common;
using UnityEngine;

namespace UI.Scripts
{
    public class TurretUpgradeButton : MonoBehaviour
    {
        #region References

        [Header("References")]
        [SerializeField] private int turretUpgradeCost; // Need to match it with button text manually. Maybe add a script to automate it ?
        [SerializeField] private Turret turretReference;

        #endregion
        
        // Start is called before the first frame update
        private void Start()
        {
        
        }

        // Update is called once per frame
        private void Update()
        {
        
        }
        
        public void Click()
        {
            if (MoneyManager.Instance.GetMoney() < turretUpgradeCost) return; // TODO : Add a visual information.
            
            // Turret upgrade instructions.
            turretReference.LevelUp();
            
            MoneyManager.Instance.AddMoney(-turretUpgradeCost);
        }
    }
}
