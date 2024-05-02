using Enemies.Scripts;
using UnityEngine;

namespace Enemy.Scripts
{
    public class EnemyControl : MonoBehaviour
    {
        #region References

        [Header("References")] 
        private int _score; // NYI
        private int _money; // NYI
        
        [Header("Script References")] 
        [SerializeField] private EnemyDataScriptable enemyData;
        [SerializeField] private EnemyHealthManager healthManager;
        [SerializeField] private EnemyBehavior enemyBehavior;

        #endregion

        private void Awake()
        {
            _score = enemyData.score;
            _money = enemyData.money;
            healthManager.SetHitPoints(enemyData.maxHitPoints);
            enemyBehavior.SetSpeed(enemyData.speed);
        }

        #region Getters and Setters

        public int GetScore()
        {
            return _score;
        }


        public int GetMoney()
        {
            return _money;
        }

        #endregion
    }
}
