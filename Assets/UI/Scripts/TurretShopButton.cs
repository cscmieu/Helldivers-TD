using Scoring.Scripts;
using UnityEngine;

namespace UI.Scripts
{
    public class TurretShopButton : MonoBehaviour
    {
        #region References

        [Header("References")]
        [SerializeField] private int turretCost; // Need to match it with button text manually. Maybe add a script to automate it ?

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
            if (MoneyManager.Instance.GetMoney() < turretCost) return; // TODO : Add a visual information.
            
            // Placing turret instructions.
            
            MoneyManager.Instance.AddMoney(-turretCost);
        }
    }
}
