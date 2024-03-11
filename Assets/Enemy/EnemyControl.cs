using System;
using UnityEngine;

namespace Enemy
{
    public class EnemyControl : MonoBehaviour
    {
        #region References

        [Header("References")] 
        [SerializeField] private float score; // NYI
        [SerializeField] private float money; // NYI
        
        [Header("Script References")] 
        [SerializeField] private EnemyDataScriptable enemyData;
        [SerializeField] private EnemyHealthManager healthManager;
        [SerializeField] private EnemyBehavior enemyBehavior;

        #endregion

        private void Awake()
        {
            score = enemyData.score;
            money = enemyData.money;
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

        private void OnDestroy()
        {
            // What happens when enemy dies.
            // Score and money somewhere.
            // Distinguish death by out of HP or by reaching end line.
        }
    }
}
