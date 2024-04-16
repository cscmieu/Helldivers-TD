using Singletons;
using UnityEngine;

namespace Scoring.Scripts
{
    public class MoneyManager : UndestructibleSingleton<MoneyManager>
    {
        #region References

        [Header("References")]
        private int _currentMoney;

        #endregion
        
        
        // Start is called before the first frame update
        private void Start()
        {
            SetMoney(0);
        }

        
        // Update is called once per frame
        private void Update()
        {

        }

        
        /*
         * Adds the value to the current money.
         * Removes the value if value < 0.
         */
        public void AddMoney(int value)
        {
            _currentMoney += value;
            // Will update UI.
        }
        
        
        #region Getters and Setters

        public int GetMoney()
        {
            return _currentMoney;
        }
        
        
        public void SetMoney(int newMoney)
        {
            _currentMoney = newMoney;
        }

        #endregion
    }
}
