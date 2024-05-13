using Map;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Shop.Scripts
{
    public class UpgradeButton : MonoBehaviour
    {
        [SerializeField] private TMP_Text costText;
        [SerializeField] private Button   upgradeButton;
        [SerializeField] private Image    range;


        public void SwitchVisibility()
        {
            upgradeButton.gameObject.SetActive(!upgradeButton.gameObject.activeSelf);
            range.gameObject.SetActive(!range.gameObject.activeSelf);
        }
        
        public void SwitchUpgrade()
        {
            upgradeButton.interactable = !upgradeButton.interactable;
        }

        public void UpdateCost(string newCost)
        {
            costText.text = newCost;
        }

        private void Update()
        {
            transform.rotation = Quaternion.Euler(0f,CameraMover.Instance.mainCamera.transform.rotation.eulerAngles.y, 0f);
        }
    }
}
