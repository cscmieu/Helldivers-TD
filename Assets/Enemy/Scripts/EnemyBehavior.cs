using UnityEngine;
using UnityEngine.AI;

namespace Enemy.Scripts
{
    public class EnemyBehavior : MonoBehaviour
    {
        #region References

        [Header("References")] 
        private NavMeshAgent _agent;
        private Transform _destinationPoint;

        #endregion
        
        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
        }

        private void Start()
        {
            _agent.SetDestination(_destinationPoint.position);
        }


        public void SetDestination(Transform target)
        {
            _destinationPoint = target;
        }


        #region Getters and Setters

        public void SetSpeed(float newSpeed)
        {
            _agent.speed = newSpeed;
        }

        #endregion
        
        
    }
}
