using Scoring.Scripts;
using UnityEngine;

namespace Enemies.Scripts
{
    public class EnemyHealthManager : MonoBehaviour
    {
        public                   bool         doomed;
        [SerializeField] private Animator     animator;
        [SerializeField] private AudioClip    deathSound;
        private                  AudioSource  _audioSource;
        private                  float        _currentHitPoints;
        private                  EnemyControl _enemyControl;
        private static readonly  int          deathByTurret = Animator.StringToHash("DeathByTurret");

        private void Awake()
        {
            _enemyControl = GetComponent<EnemyControl>();
            _audioSource  = GetComponent<AudioSource>();
        }

        public void TakeDamage(float damageValue)
        {
            _currentHitPoints -= damageValue;

            if (!(_currentHitPoints <= 0) || doomed) return;
            doomed = true; // Set to be destroyed after animation finishes
            ScoreManager.Instance.ScoreByDeath(_enemyControl.GetScore());
            MoneyManager.Instance.AddMoney(_enemyControl.GetMoney());
            animator.SetTrigger(deathByTurret);
            Destroy(gameObject, 2f);
            gameObject.layer = 10;
            _audioSource.PlayOneShot(deathSound);
            Spawner.Scripts.Spawner.Instance.enemyCount--;
        }

        
        #region Getters & Setters
        
        public void SetHitPoints(int newHitPoints)
        {
            _currentHitPoints = newHitPoints;
        }


        #endregion
        
        
    }
}
