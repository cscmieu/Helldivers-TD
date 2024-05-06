using Enemies.Scripts;
using Scoring.Scripts;
using UnityEngine;

namespace Base.Scripts
{
    public class BaseManager : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            Destroy(other.gameObject);
            other.GetComponent<EnemyControl>().GetComponentInChildren<Animator>().SetTrigger("ReachEnd");
            ScoreManager.Instance.ScoreByEndOfPath(other.GetComponent<EnemyControl>().GetScore());
        }
    }
}
