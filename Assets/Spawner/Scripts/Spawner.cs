using System.Collections;
using System.Collections.Generic;
using Enemy.Scripts;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Spawner.Scripts
{
    public class Spawner : MonoBehaviour
    {
        
        [Header("References")]
        // Operation handle used to load and release assets
        private AsyncOperationHandle<IList<GameObject>> _loadHandle;
        [SerializeField] private float     timeBetweenSwarmers;
        [SerializeField] private float     timeBetweenChargers;
        [SerializeField] private float     timeBetweenPseudoWaves;
        [SerializeField] private Transform destination;
        private                  Transform _transform;
        private                  int       _waveId;
        
        
        // Load Addressable by Label
        private IEnumerator Start()
        {
            _transform  = transform;
            _loadHandle = Addressables.LoadAssetsAsync<GameObject>("Enemy", null);
            _loadHandle.Completed += (_) => {};
            yield return _loadHandle;
        }

        public IEnumerator SpawnWave1(int level)
        {
            yield return null;
        }
        
        public IEnumerator SpawnWave2(int level)
        {
            yield return null;
        }
        
        public IEnumerator SpawnWave3(int level)
        {
            yield return null;
        }

        public IEnumerator SpawnBossWave()
        {
            yield return null;
        }
        
        private void SpawnSwarmer()
        {
            var swarmer             = _loadHandle.Result[0];
            var instantiatedSwarmer = Instantiate(swarmer, _transform.position, Quaternion.identity);
            if (instantiatedSwarmer.TryGetComponent<EnemyBehavior>(out var enemyScript))
            {
                enemyScript.SetDestination(destination);
            }
        }

        private void SpawnCharger()
        {
            var charger             = _loadHandle.Result[1];
            var instantiatedCharger = Instantiate(charger, _transform.position, Quaternion.identity);
            if (instantiatedCharger.TryGetComponent<EnemyBehavior>(out var enemyScript))
            {
                enemyScript.SetDestination(destination);
            }
        }
        
        private void SpawnTitan()
        {
            var titan             = _loadHandle.Result[2];
            var instantiatedTitan = Instantiate(titan, _transform.position, Quaternion.identity);
            if (instantiatedTitan.TryGetComponent<EnemyBehavior>(out var enemyScript))
            {
                enemyScript.SetDestination(destination);
            }
        }
        
        
        private void OnDestroy()
        {
            Addressables.Release(_loadHandle);
        }
    }
}
