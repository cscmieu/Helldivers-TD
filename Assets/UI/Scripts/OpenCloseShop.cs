using UnityEngine;

namespace UI.Scripts
{
    public class OpenCloseShop : MonoBehaviour
    {
        #region References

        [Header("References")] 
        [SerializeField] private GameObject shopInstance;
        private bool _shopIsActive = false;

        #endregion
        
        // Start is called before the first frame update
        private void Start()
        {
        
        }

        // Update is called once per frame
        private void Update()
        {
        
        }

        public void Click()
        {
            shopInstance.SetActive(!_shopIsActive);
            _shopIsActive = !_shopIsActive;
        }
    }
}
