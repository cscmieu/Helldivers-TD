using Scoring.Scripts;
using UnityEngine;

namespace Enemies.Scripts
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
            //GameObject o;
            //(o = gameObject).SetActive(false);
            //Destroy(o, 1f);
            gameObject.GetComponentInChildren<Animator>().SetTrigger("DeathByTurret");
            Destroy(gameObject, 2f);
            ScoreManager.Instance.ScoreByDeath(_enemyControl.GetScore());
            MoneyManager.Instance.AddMoney(_enemyControl.GetMoney());
        }

        
        #region Getters & Setters
        
        public void SetHitPoints(int newHitPoints)
        {
            _currentHitPoints = newHitPoints;
        }


        #endregion
        
        
    }
}
