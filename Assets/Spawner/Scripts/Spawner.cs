using System.Collections;
using System.Collections.Generic;
using Singletons;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Spawner.Scripts
{
    public class Spawner : SimpleSingleton<Spawner>
    {
        public                   Transform                      destination;
        [SerializeField] private float                          timeBetweenSwarmers     = 0.1f;
        [SerializeField] private float                          timeBetweenChargers     = 1f;
        [SerializeField] private float                          timeBetweenPseudoWaves  = 1f;
        [SerializeField] private int                            numberOfSwarmersPerWave = 5;
        [SerializeField] private int                            numberOfChargersPerWave = 3;
        [SerializeField] private List<AssetReferenceGameObject> swarmerGameObjects;
        [SerializeField] private List<AssetReferenceGameObject> chargerGameObjects;
        [SerializeField] private AssetReferenceGameObject       titanGameObject;
        [SerializeField] private TMP_Text                       waveNumber;
        [SerializeField] private TMP_Text                       enemiesLeft;
        private                  Transform                      _transform;
        private                  int                            _currentWaveId;

        private void Awake()
        {
            _transform = transform;
        }

    #region Loading Of Adressables

        private void LoadSwarmer(int level)
        {
            swarmerGameObjects[level].InstantiateAsync(_transform.position, Quaternion.identity, _transform);
        }
        
        private void LoadCharger(int level)
        {
            chargerGameObjects[level].InstantiateAsync(_transform.position, Quaternion.identity, _transform);
        }
        
        private void LoadTitan()
        {
            titanGameObject.InstantiateAsync(_transform.position, Quaternion.identity, _transform);
        }
        //
        // private void InstantiateEnemy(AsyncOperationHandle<GameObject> obj)
        // {
        //     if (obj.Status == AsyncOperationStatus.Succeeded)
        //     {
        //         var pendingEnemy = Instantiate(obj.Result, _transform.position, Quaternion.identity);
        //         if (pendingEnemy.TryGetComponent<EnemyBehavior>(out var enemyScript))
        //         {
        //             enemyScript.SetDestination(destination);
        //         }
        //         return;
        //     }
        //     Debug.LogError("Enemy Loading Failed");
        // }

    #endregion

    #region Spawner Coroutines

        #region Wave Spawners
            
        private IEnumerator SpawnWave1(int level)
        {
            enemiesLeft.text = 10.ToString();
            StartCoroutine(SpawnSwarmerBreach(numberOfSwarmersPerWave, level));
            yield return new WaitForSeconds(timeBetweenPseudoWaves + numberOfSwarmersPerWave * timeBetweenSwarmers);
            StartCoroutine(SpawnSwarmerBreach(numberOfSwarmersPerWave, level));
            yield return null;
        }
        
        private IEnumerator SpawnWave2(int level)
        {
            enemiesLeft.text = 3.ToString();
            StartCoroutine(SpawnChargerBreach(numberOfChargersPerWave, level));
            yield return null;
        }
        
        private IEnumerator SpawnWave3(int level)
        {
            enemiesLeft.text = 7.ToString();
            StartCoroutine(SpawnSwarmerBreach(numberOfSwarmersPerWave, level));
            yield return new WaitForSeconds(timeBetweenPseudoWaves + numberOfSwarmersPerWave * timeBetweenSwarmers);
            StartCoroutine(SpawnChargerBreach(2, level));
            yield return null;
        }

        private IEnumerator SpawnBossWave()
        {
            enemiesLeft.text = 1.ToString();
            SpawnTitan();
            yield return null;
        }
        
    #endregion

        #region Swarmer Spawers
        
        private void SpawnSwarmer(int level)
        {
            LoadSwarmer(level);
        }

        private IEnumerator SpawnSwarmerBreach(int numberOfSwarmers, int level)
        {
            for (var i = 0; i < numberOfSwarmers; i++)
            {
                SpawnSwarmer(level);
                yield return new WaitForSeconds(timeBetweenSwarmers);
            }
            yield return null;
        }
        
    #endregion

        #region Charger Spawners

        private void SpawnCharger(int level)
        {
            LoadCharger(level);
        }

        private IEnumerator SpawnChargerBreach(int numberOfChargers, int level)
        {
            for (var i = 0; i < numberOfChargers; i++)
            {
                SpawnCharger(level);
                yield return new WaitForSeconds(timeBetweenChargers);
            }
            yield return null;
        }

    #endregion

        #region Titan Spawner
        
        private void SpawnTitan()
        {
            LoadTitan();
        }

    #endregion

    #endregion

        public void SendNextWave()
        {
            if (_currentWaveId % 10 == 9)
            {
                StartCoroutine(SpawnBossWave());
                _currentWaveId++;
                return;
            }

            var currentWaveLevel = _currentWaveId / 3;
            currentWaveLevel %= 3;
            switch (_currentWaveId % 3)
            {
                case 0 :
                    StartCoroutine(SpawnWave1(currentWaveLevel));
                    break;
                case 1 :
                    StartCoroutine(SpawnWave2(currentWaveLevel));
                    break;
                case 2 :
                    StartCoroutine(SpawnWave3(currentWaveLevel));
                    break;
            }
            _currentWaveId++;
            waveNumber.text = _currentWaveId.ToString();
        }
    }
}
