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
        }

        // Start is called before the first frame update
        private void Start()
        {
            healthManager.SetMaxHitPoints(enemyData.maxHitPoints);
            enemyBehavior.SetSpeed(enemyData.speed);
        }

        // Update is called once per frame
        private void Update()
        {
        
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
