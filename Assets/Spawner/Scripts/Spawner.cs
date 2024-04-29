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
        [SerializeField] private float     spawnCooldown;
        [SerializeField] private Transform destination;
        private                  Transform _transform;
        
        
        // Load Addressable by Label
        private IEnumerator Start()
        {
            _transform = transform;
            _loadHandle = Addressables.LoadAssetsAsync<GameObject>(
                "Enemy",
                null);
            _loadHandle.Completed += (_) =>
            {
                StartCoroutine(SpawnEnemy());
            };

            yield return _loadHandle;
        }

        
        private IEnumerator SpawnEnemy()
        { //Ca c'est de la merde là hein
            while (true)
            {
                yield return new WaitForSeconds(1f);
                var enemy             = _loadHandle.Result[Random.Range(0, _loadHandle.Result.Count)];
                var instantiatedEnemy = Instantiate(enemy, _transform.position, Quaternion.identity, _transform);
                if (instantiatedEnemy.TryGetComponent<EnemyBehavior>(out var enemyScript))
                {
                    enemyScript.SetDestination(destination);
                }
                yield return new WaitForSeconds(spawnCooldown);
            }
        }
        
        
        private void OnDestroy()
        {
            Addressables.Release(_loadHandle);
        }

        #region Getters and Setters

        public void SetSpawnCooldown(float newSpawnCooldown)
        {
            spawnCooldown = newSpawnCooldown;
        }

        #endregion
    }
}
