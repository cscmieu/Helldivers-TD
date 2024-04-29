using Scoring.Scripts;
using TMPro;
using UnityEngine;

namespace Shop.Scripts
{
    public class ShopManager : MonoBehaviour
    {
        [SerializeField] private GameObject shopObject;
        [SerializeField] private TMP_Text   moneyCount;
        private                  bool       _shopStatus;
        
        public void OpenCloseShop()
        {
            _shopStatus = !_shopStatus;
            shopObject.SetActive(_shopStatus);
        }

        private void Update()
        {
            moneyCount.text = MoneyManager.Instance.GetMoney().ToString();
        }
    }
}
