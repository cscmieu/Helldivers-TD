using System.Collections;
using Singletons;
using UnityEngine;

namespace Scoring.Scripts
{
    public class MoneyManager : UndestructibleSingleton<MoneyManager>
    {
        #region References

        [Header("References")]
        [SerializeField] private GameObject fundsMissingWarning;
        private int _currentMoney;

        #endregion
            
        private void Start()
        {
            SetMoney(100);
        }

        /*
         * Adds the value to the current money.
         * Removes the value if value < 0.
         */
        public void AddMoney(int value)
        {
            _currentMoney += value;
        }
        
        public IEnumerator DisplayWarning()
        {
            fundsMissingWarning.SetActive(true);
            yield return new WaitForSeconds(3f);
            fundsMissingWarning.SetActive(false);
            yield return null;
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
