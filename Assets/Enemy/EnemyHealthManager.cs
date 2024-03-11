using UnityEngine;

namespace Enemy
{
    public class EnemyHealthManager : MonoBehaviour
    {
        [SerializeField] private int maxHitPoints;

        private int _currentHitPoints;
        
        // Start is called before the first frame update.
        private void Start()
        {
            _currentHitPoints = maxHitPoints;
        }

        
        // Update is called once per frame.
        private void Update()
        {
            
        }

        
        public void TakeDamage(int damageValue)
        {
            _currentHitPoints -= damageValue;

            if (_currentHitPoints <= 0)
            {
                Death();
            }
        }

        private void Death()
        {
            Destroy(gameObject);
            // Triggers EnemyControl.OnDestroy().
        }

        
        #region Getters and Setters

        public int GetCurrentHitPoints()
        {
            return _currentHitPoints;
        }
        
        
        public void SetMaxHitPoints(int newMaxHitPoints)
        {
            maxHitPoints = newMaxHitPoints;
        }

        #endregion
        
        
    }
}
