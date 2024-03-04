using System;
using UnityEngine;

namespace Singletons
{
    public class UndestructibleSingleton<T> : MonoBehaviour where T : Component
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance is not null) return _instance;
                _instance = (T)FindObjectOfType(typeof(T));
                if (_instance is not null) return _instance;
                SetupIstance();
                return _instance;
            }
        }

        private void Awake()
        {
            RemoveDuplicates();
        }

        private static void SetupIstance()
        {
            var blankGameObject = new GameObject 
                                  {
                                                     name = typeof(T).Name
                                  };
            _instance = blankGameObject.AddComponent<T>();
            DontDestroyOnLoad(blankGameObject);
        }
        private void RemoveDuplicates()
        {
            if (_instance is null)
            {
                _instance = this as T;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
