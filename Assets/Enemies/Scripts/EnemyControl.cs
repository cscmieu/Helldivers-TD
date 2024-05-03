using UnityEngine;

namespace Enemies.Scripts
{
    public class EnemyControl : MonoBehaviour
    {
        #region References

        [Header("References")] 
        private int _score; // NYI
        private int _money; // NYI
        
        [Header("Script References")] 
        public EnemyDataScriptable enemyData;
        [SerializeField] private EnemyHealthManager healthManager;
        [SerializeField] private EnemyBehavior enemyBehavior;

        #endregion

        private void Awake()
        {
            enemyData.level = Spawner.Scripts.Spawner.Instance.currentWaveLevel;
            _score          = enemyData.score * enemyData.level;
            _money          = enemyData.money * enemyData.level;
            healthManager.SetHitPoints(enemyData.maxHitPoints * enemyData.level);
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
