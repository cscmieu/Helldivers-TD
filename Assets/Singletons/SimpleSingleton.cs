using UnityEngine;

namespace Singletons
{
    public class SimpleSingleton<T> : MonoBehaviour where T : Component
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance is not null) return _instance;
                _instance = (T)FindObjectOfType(typeof(T));
                if (_instance is not null) return _instance;
                SetupInstance();
                return _instance;
            }
        }

        private void Awake()
        {
            RemoveDuplicates();
        }

        private static void SetupInstance()
        {
            var blankGameObject = new GameObject 
                                  {
                                      name = typeof(T).Name
                                  };
            _instance = blankGameObject.AddComponent<T>();
        }
        private void RemoveDuplicates()
        {
            if (_instance is null)
            {
                _instance = this as T;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
