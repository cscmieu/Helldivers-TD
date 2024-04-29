using Scoring.Scripts;
using TMPro;
using Turrets.Scripts.Common;
using UnityEngine;
using UnityEngine.UI;

namespace Shop.Scripts
{
    [RequireComponent(typeof(Button))]
    public class TurretAvailability : MonoBehaviour
    {
        [SerializeField] private TurretScriptableObject turret;
        [SerializeField] private TMP_Text               turretIdText;
        [SerializeField] private TMP_Text               costText;
        private                  Button                 _button;

        private void Awake()
        {
            _button           = GetComponent<Button>();
            turretIdText.text = turret.turretName;
            costText.text     = turret.turretCost.ToString();
        }

        private void Update()
        {
            if (turret.turretCost > MoneyManager.Instance.GetMoney())
            {
                _button.interactable = false;
                return;
            }
            _button.interactable = true;
        }
    }
}
