using System;
using Enemies.Scripts;
using Scoring.Scripts;
using UnityEngine;

namespace Base.Scripts
{
    public class BaseManager : MonoBehaviour
    {
        [SerializeField] private AudioClip   reachSound;
        private                  AudioSource _audioSource;
        private static readonly  int         reachEnd = Animator.StringToHash("ReachEnd");

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void OnTriggerEnter(Collider other)
        {
            Destroy(other.gameObject);
            other.GetComponent<EnemyControl>().GetComponentInChildren<Animator>().SetTrigger(reachEnd);
            ScoreManager.Instance.ScoreByEndOfPath(other.GetComponent<EnemyControl>().GetScore());
            _audioSource.PlayOneShot(reachSound);
        }
    }
}
