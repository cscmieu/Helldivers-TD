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
        private                  float    _cameraRotationSpeed;

        private void Awake()
        {
            _cameraRotationSpeed = CameraMover.Instance.rotationSpeed;
        }

        public void SwitchVisibility()
        {
            upgradeButton.gameObject.SetActive(!upgradeButton.gameObject.activeSelf);
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
            if (Input.GetKey(KeyCode.E))
            {
                transform.Rotate(Vector3.up, _cameraRotationSpeed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.Q))
            {
                transform.Rotate(Vector3.up, -_cameraRotationSpeed * Time.deltaTime);
            }
        }
    }
}
