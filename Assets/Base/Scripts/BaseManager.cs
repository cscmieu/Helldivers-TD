using System;
using UnityEngine;

namespace Base.Scripts
{
    public class BaseManager : MonoBehaviour
    {
        // Start is called before the first frame update
        private void Start()
        {
        
        }

        // Update is called once per frame
        private void Update()
        {
        
        }

        private void OnTriggerEnter(Collider other)
        {
            Destroy(other.gameObject);
        }
    }
}