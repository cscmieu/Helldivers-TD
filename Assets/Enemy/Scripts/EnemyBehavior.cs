using UnityEngine;
using UnityEngine.AI;

namespace Enemy.Scripts
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
            destinationPoint = GameObject.Find("Destination").GetComponent<Transform>(); // Might be upgradable.
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


        #region Getters and Setters

        public void SetSpeed(float newSpeed)
        {
            agent.speed = newSpeed;
        }

        #endregion
        
        
    }
}
