using System.Collections;
using Enemies.Scripts;
using Scoring.Scripts;
using Shop.Scripts;
using UnityEngine;

namespace Turrets.Scripts.Common
{
    public class Turret : MonoBehaviour
    {
        public                   TurretScriptableObject turretData;
        [SerializeField] private LayerMask              enemyLayer;
        [SerializeField] private Transform              muzzlePoint;
        [SerializeField] private TrailRenderer          bulletTracer;
        [SerializeField] private UpgradeButton          upgradeButton;
        [SerializeField] private Transform              rotatingPart;
        [SerializeField] private AudioClip              shotSound;
        private                  AudioSource            _audioSource;
        private                  EnemyHealthManager     _target;
        private                  float                  _range;
        private                  float                  _damagePerHit;
        private                  int                    _turretCost;
        private                  float                  _timeBetweenShots;
        private                  int                    _level;
        private                  int                    _levelUpCost;
        private                  float                  _lastTimeShot;

        private void Awake()
        {
            _range            = turretData.range;
            _damagePerHit     = turretData.damagePerBullet;
            _turretCost       = turretData.turretCost;
            _timeBetweenShots = 1f / turretData.shotsPerSecond;
            _audioSource      = GetComponent<AudioSource>();
        }

        private void Start()
        {
            _levelUpCost = _turretCost * (_level + 1) / 2;
            upgradeButton.UpdateCost(_levelUpCost.ToString());
        }

        private void Update()
        {
            _lastTimeShot -= Time.deltaTime;
            Shoot();
        }

        private void Shoot()
        {
            _target = null;
            if (!AssessTarget()) return;
            if (_lastTimeShot > 0) return;
            _target!.TakeDamage(_damagePerHit);
            _lastTimeShot = _timeBetweenShots;
            var tracer = Instantiate(bulletTracer, muzzlePoint.position, Quaternion.identity);
            StartCoroutine(InstantiateTracer(tracer, _target.transform.position));
            _audioSource.PlayOneShot(shotSound);
        }

        private IEnumerator InstantiateTracer(TrailRenderer tracer, Vector3 target)
        {
            var time   = 0f;
            while (time < 1f)
            {
                tracer.transform.position =  Vector3.Lerp(muzzlePoint.position, target, time);
                time                            += Time.deltaTime / tracer.time;
                yield return null;
            }
            tracer.transform.position = target;
            Destroy(tracer.gameObject, tracer.time);
            yield return null;
        }

        private bool AssessTarget()
        {
            // OverlapSphereNonAlloc marche pas T_T
            var enemiesInRange = Physics.OverlapSphere(transform.position, _range, enemyLayer);
            if (enemiesInRange.Length == 0) return false; // No Enemies in Range
            if (!enemiesInRange[0].TryGetComponent<EnemyHealthManager>(out var enemyHealth) && !enemyHealth.enabled) return false; //Enemy is already destroyed;
            _target = enemyHealth;
            rotatingPart.LookAt(enemyHealth.transform);
            return true;
        }

        public void LevelUp()
        {
            if (_level > 3) return;
            if (_levelUpCost > MoneyManager.Instance.GetMoney())
            {
                StopCoroutine(MoneyManager.Instance.DisplayWarning());
                StartCoroutine(MoneyManager.Instance.DisplayWarning());
                return;
            }
            MoneyManager.Instance.AddMoney(-_levelUpCost);
            var newValue = turretData.upgradeValues[_level];
            switch (turretData.upgradeStats[_level])
            {
                case "Damage" :
                    _damagePerHit = newValue;
                    break;
                case "Range" :
                    _range = newValue;
                    break;
                case "Time Between Shots" :
                    _timeBetweenShots = newValue;
                    break;
            }
            _level++;
            if (_level > 3)
            {
                upgradeButton.UpdateCost("MAX");
                upgradeButton.SwitchUpgrade();
                return;
            }
            _levelUpCost = _turretCost * (_level + 1) / 2;
            upgradeButton.UpdateCost(_levelUpCost.ToString());
        }
    }
}
