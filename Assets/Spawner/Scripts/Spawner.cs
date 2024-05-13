using System;
using System.Collections;
using Singletons;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Serialization;

namespace Spawner.Scripts
{
    public class Spawner : SimpleSingleton<Spawner>
    {
        public                   Transform                destination;
        public                   int                      currentWaveLevel;
        public                   int                      enemyCount;
        [SerializeField] private float                    timeBetweenSwarmers     = 0.1f;
        [SerializeField] private float                    timeBetweenChargers     = 1f;
        [SerializeField] private float                    timeBetweenPseudoWaves  = 1f;
        [SerializeField] private int                      numberOfSwarmersPerWave = 5;
        [SerializeField] private int                      numberOfChargersPerWave = 3;
        [SerializeField] private AssetReferenceGameObject swarmerGameObjects;
        [SerializeField] private AssetReferenceGameObject chargerGameObjects;
        [SerializeField] private AssetReferenceGameObject titanGameObject;
        [SerializeField] private TMP_Text                 waveNumber;
        [SerializeField] private TMP_Text                 enemiesLeft;
        [SerializeField] private AudioClip                spawnSound;
        private                  AudioSource              _audioSource;
        private                  Transform                _transform;
        private                  int                      _currentWaveId;

        private void Awake()
        {
            _transform   = transform;
            _audioSource = GetComponent<AudioSource>();
        }

        private void Update()
        {
            enemiesLeft.text = enemyCount.ToString();
        }

    #region Loading Of Adressables

        private void LoadSwarmer()
        {
            swarmerGameObjects.InstantiateAsync(_transform.position, Quaternion.identity, _transform);
            _audioSource.PlayOneShot(spawnSound);
            enemyCount++;
        }
        
        private void LoadCharger()
        {
            chargerGameObjects.InstantiateAsync(_transform.position, Quaternion.identity, _transform);
            _audioSource.PlayOneShot(spawnSound);
            enemyCount++;
        }
        
        private void LoadTitan()
        {
            titanGameObject.InstantiateAsync(_transform.position, Quaternion.identity, _transform);
            _audioSource.PlayOneShot(spawnSound);
            enemyCount++;
        }

    #endregion

    #region Spawner Coroutines

        #region Wave Spawners
            
        private IEnumerator SpawnWave1()
        {
            StartCoroutine(SpawnSwarmerBreach(numberOfSwarmersPerWave));
            yield return new WaitForSeconds(timeBetweenPseudoWaves + numberOfSwarmersPerWave * timeBetweenSwarmers);
            StartCoroutine(SpawnSwarmerBreach(numberOfSwarmersPerWave));
            yield return null;
        }
        
        private IEnumerator SpawnWave2()
        {
            StartCoroutine(SpawnChargerBreach(numberOfChargersPerWave));
            yield return null;
        }
        
        private IEnumerator SpawnWave3()
        {
            StartCoroutine(SpawnSwarmerBreach(numberOfSwarmersPerWave));
            yield return new WaitForSeconds(timeBetweenPseudoWaves + numberOfSwarmersPerWave * timeBetweenSwarmers);
            StartCoroutine(SpawnChargerBreach(2));
            yield return null;
        }

        private IEnumerator SpawnBossWave()
        {
            SpawnTitan();
            yield return null;
        }
        
    #endregion

        #region Swarmer Spawers
        
        private void SpawnSwarmer()
        {
            LoadSwarmer();
        }

        private IEnumerator SpawnSwarmerBreach(int numberOfSwarmers)
        {
            for (var i = 0; i < numberOfSwarmers; i++)
            {
                SpawnSwarmer();
                yield return new WaitForSeconds(timeBetweenSwarmers);
            }
            yield return null;
        }
        
    #endregion

        #region Charger Spawners

        private void SpawnCharger()
        {
            LoadCharger();
        }

        private IEnumerator SpawnChargerBreach(int numberOfChargers)
        {
            for (var i = 0; i < numberOfChargers; i++)
            {
                SpawnCharger();
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
                waveNumber.text = _currentWaveId.ToString();
                return;
            }
            
            switch (_currentWaveId % 3)
            {
                case 0 :
                    currentWaveLevel++;
                    StartCoroutine(SpawnWave1());
                    break;
                case 1 :
                    StartCoroutine(SpawnWave2());
                    break;
                case 2 :
                    StartCoroutine(SpawnWave3());
                    break;
            }
            _currentWaveId++;
            waveNumber.text = _currentWaveId.ToString();
        }
    }
}
