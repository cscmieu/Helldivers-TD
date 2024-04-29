using Scoring.Scripts;
using UnityEngine;

namespace Enemy.Scripts
{
    public class EnemyHealthManager : MonoBehaviour
    {
        private float        _currentHitPoints;
        private EnemyControl _enemyControl;
        private bool         _doomed;

        private void Awake()
        {
            _enemyControl = GetComponent<EnemyControl>();
        }

        public void TakeDamage(float damageValue)
        {
            _currentHitPoints -= damageValue;

            if (_currentHitPoints <= 0)
            {
                _doomed = true; // Set to be killed at the end of frame
            }
        }

        /*
         * What happens when an enemy is killed by a turret
         */
        private void LateUpdate()
        {
            if (!_doomed) return;
            gameObject.SetActive(false);
            Destroy(gameObject, 1f);
            ScoreManager.Instance.ScoreByDeath(_enemyControl.GetScore());
            MoneyManager.Instance.AddMoney(_enemyControl.GetMoney());
        }

        
        #region Getters & Setters

        public float GetHitPoints()
        {
            return _currentHitPoints;
        }
        
        public bool IsDoomed()
        {
            return _doomed;
        }
        
        public void SetHitPoints(int newHitPoints)
        {
            _currentHitPoints = newHitPoints;
        }


        #endregion
        
        
    }
}
