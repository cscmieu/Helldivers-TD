using System.Collections;
using System.Collections.Generic;
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
        [SerializeField] private float spawnCooldown;
        
        
        // Load Addressables by Label
        private IEnumerator Start()
        {
            _loadHandle = Addressables.LoadAssetsAsync<GameObject>(
                "Enemy",
                null);
            _loadHandle.Completed += (operation) =>
            {
                StartCoroutine(SpawnEnemy());
            };

            yield return _loadHandle;
        }

        
        private IEnumerator SpawnEnemy()
        {
            while (true)
            {
                yield return new WaitForSeconds(spawnCooldown);

                GameObject enemy = _loadHandle.Result[Random.Range(0, _loadHandle.Result.Count)];
                Instantiate(enemy, transform.position, Quaternion.identity, transform);
            }
        }
        
        
        private void OnDestroy()
        {
            Addressables.Release(_loadHandle);
        }
        
        
        // Update is called once per frame
        private void Update()
        {
            
        }

        #region Getters and Setters

        public void SetSpawnCooldown(float newSpawnCooldown)
        {
            spawnCooldown = newSpawnCooldown;
        }

        #endregion
    }
}
