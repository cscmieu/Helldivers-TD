using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Shop.Scripts
{
    [RequireComponent(typeof(Button))]
    public class UpgradeButton : MonoBehaviour
    {
        [SerializeField] private TMP_Text costText;
        [SerializeField] private Button   button;
        
        public void SwitchVisibility()
        {
            button.gameObject.SetActive(!button.gameObject.activeSelf);
        }
        
        public void SwitchUpgrade()
        {
            button.interactable = !button.interactable;
        }

        public void UpdateCost(string newCost)
        {
            costText.text = newCost;
        }
    }
}
