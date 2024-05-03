using Singletons;
using TMPro;
using UnityEngine;

namespace Scoring.Scripts
{
    public class ScoreManager : SimpleSingleton<ScoreManager>
    {
        #region References

        [Header("References")] 
        [SerializeField] private TMP_Text scoreText;
        [SerializeField] private TMP_Text multiplierText;
        [SerializeField] private int      maxMult   = 10; // May be changed after testing.
        [SerializeField] private int      maxStreak = 15; // May be changed after testing.
        private                  int      _currentScore;
        private                  int      _currentMultiplier;
        private                  int      _currentStreak;

        #endregion
        
        
        private void Start()
        {
            SetScore(0);
            _currentMultiplier = 1;
            _currentStreak = 0;
        }

        
        /*
         * Adds the value to the current score.
         * Removes the value if value < 0.
         */
        private void AddScore(int value)
        {
            _currentScore  += value;
            scoreText.text =  _currentScore.ToString();
        }

        
        /*
         * Updates the score when an enemy is killed by a turret.
         */
        public void ScoreByDeath(int enemyScore)
        {
            if (_currentMultiplier < 0)
            {
                _currentMultiplier = 1;
                _currentStreak = 0;
            }

            if (_currentStreak < maxStreak)
                _currentStreak++;
            else
            {
                _currentStreak = 0;
                if (_currentMultiplier < maxMult)
                    _currentMultiplier++;
            }
            
            AddScore(enemyScore * _currentMultiplier);
            multiplierText.text = "x " + _currentMultiplier;
        }

        
        /*
         * Updates the score when an enemy reaches the end of the path.
         */
        public void ScoreByEndOfPath(int enemyScore)
        {
            if (_currentMultiplier > 0)
            {
                _currentMultiplier = -1;
                _currentStreak = 0;
            }
            
            if (_currentStreak < maxStreak)
                _currentStreak++;
            else
            {
                _currentStreak = 0;
                if (_currentMultiplier > -maxMult)
                    _currentMultiplier--;
            }
            
            AddScore(enemyScore * _currentMultiplier);
            multiplierText.text = "x " + _currentMultiplier;
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
