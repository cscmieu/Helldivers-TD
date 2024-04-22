using Singletons;
using TMPro;
using UnityEngine;

namespace UI.Scripts
{
    public class UIManager : UndestructibleSingleton<UIManager>
    {
        #region References

        [Header("UI Texts")] 
        [SerializeField] private TMP_Text scoreText;
        [SerializeField] private TMP_Text multText;
        [SerializeField] private TMP_Text moneyText;
        [SerializeField] private TMP_Text waveNumberText;
        [SerializeField] private TMP_Text enemyRemainingText;

        #endregion
        
        // Start is called before the first frame update
        private void Start()
        {
        
        }

        // Update is called once per frame
        private void Update()
        {
        
        }

        #region UI Text Updates

        public void UpdateScoreText(int newScore)
        {
            scoreText.text = "Score : " + newScore;
        }

        public void UpdateMultText(int newMult)
        {
            multText.text = "Mult : " + newMult;
        }

        public void UpdateMoneyText(int newMoney)
        {
            moneyText.text = "Money : " + newMoney;
        }

        public void UpdateWaveNumberText(int newWaveNumber, int maxWaveNumber)
        {
            waveNumberText.text = "Wave " + newWaveNumber + " / " + maxWaveNumber;
        }

        public void UpdateEnemyRemainingText(int newEnemyRemaining)
        {
            enemyRemainingText.text = "Enemy Remaining : " + newEnemyRemaining;
        }

        #endregion
         
    }
}
