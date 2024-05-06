using UnityEngine;
using UnityEngine.AI;

namespace Enemies.Scripts
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
            _agent            = GetComponent<NavMeshAgent>();
            _destinationPoint = Spawner.Scripts.Spawner.Instance.destination;
        }

        private void Start()
        {
            _agent.SetDestination(_destinationPoint.position);
        }

        public void DestroyAgent()
        {
            Destroy(_agent);
        }


        #region Getters and Setters

        public void SetSpeed(float newSpeed)
        {
            _agent.speed = newSpeed;
        }
        
        public void SetDestination(Transform target)
        {
            _destinationPoint = target;
        }

        #endregion
        
        
    }
}
