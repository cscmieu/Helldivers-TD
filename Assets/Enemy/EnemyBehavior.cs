using System;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class EnemyBehavior : MonoBehaviour
    {
        #region References

        [Header("References")] 
        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private Transform destinationPoint;

        #endregion
        
        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
        }


        // Start is called before the first frame update
        private void Start()
        {
        
        }

        
        // Update is called once per frame
        private void Update()
        {
            agent.SetDestination(destinationPoint.position);
        }
    }
}
