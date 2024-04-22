using Singletons;
using UI.Scripts;
using UnityEngine;

namespace Scoring.Scripts
{
    public class ScoreManager : UndestructibleSingleton<ScoreManager>
    {
        #region References

        [Header("References")] 
        private int _currentScore;
        private int _currentMult;
        private int _currentStreak;
        [SerializeField] private int maxMult = 10; // May be changed after testing.
        [SerializeField] private int maxStreak = 15; // May be changed after testing.

        #endregion
        
        
        // Start is called before the first frame update
        private void Start()
        {
            SetScore(0);
            _currentMult = 1;
            _currentStreak = 0;
        }

       
        // Update is called once per frame
        private void Update()
        {
        
        }

        
        /*
         * Adds the value to the current score.
         * Removes the value if value < 0.
         */
        public void AddScore(int value)
        {
            _currentScore += value;
            UIManager.Instance.UpdateScoreText(_currentScore);
            UIManager.Instance.UpdateMultText(_currentMult);
        }

        
        /*
         * Updates the score when an enemy is killed by a turret.
         */
        public void ScoreByDeath(int enemyScore)
        {
            if (_currentMult < 0)
            {
                _currentMult = 1;
                _currentStreak = 0;
            }

            if (_currentStreak < maxStreak)
                _currentStreak++;
            else
            {
                _currentStreak = 0;
                if (_currentMult < maxMult)
                    _currentMult++;
            }
            
            AddScore(enemyScore * _currentMult);
        }

        
        /*
         * Updates the score when an enemy reaches the end of the path.
         */
        public void ScoreByEndOfPath(int enemyScore)
        {
            if (_currentMult > 0)
            {
                _currentMult = -1;
                _currentStreak = 0;
            }
            
            if (_currentStreak < maxStreak)
                _currentStreak++;
            else
            {
                _currentStreak = 0;
                if (_currentMult > -maxMult)
                    _currentMult--;
            }
            
            AddScore(enemyScore * _currentMult);
        }
        

        #region Getters and Setters

        public int GetScore()
        {
            return _currentScore;
        }
        
        public void SetScore(int newScore)
        {
            _currentScore = newScore;
        }

        #endregion
    }
}
