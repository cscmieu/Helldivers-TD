using System;
using System.Collections;
using Enemy.Scripts;
using Scoring.Scripts;
using Shop.Scripts;
using TMPro;
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
        private                  EnemyHealthManager     _target;
        private                  float                  _range;
        private                  float                  _damagePerHit;
        private                  int                    _turretCost;
        private                  float                  _timeBetweenShots;
        [SerializeField] private                  int                    _level;
        private                  int                    _levelUpCost;
        private                  float                  _lastTimeShot;

        private void Awake()
        {
            _range                 = turretData.range;
            _damagePerHit          = turretData.damagePerBullet;
            _turretCost            = turretData.turretCost;
            _timeBetweenShots      = 1f / turretData.shotsPerSecond;
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
            if (_lastTimeShot > 0) return;
            _target = null;
            if (!AssessTarget()) return;
            _target.TakeDamage(_damagePerHit);
            _lastTimeShot = _timeBetweenShots;
            var tracer = Instantiate(bulletTracer, muzzlePoint.position, Quaternion.identity);
            StartCoroutine(InstantiateTracer(tracer, _target.transform.position));
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
            var enemiesInRange = Physics.OverlapSphere(transform.position, _range, enemyLayer);
            if (enemiesInRange.Length == 0) return false; // No Enemies in Range
            if (!enemiesInRange[0].TryGetComponent<EnemyHealthManager>(out var enemyHealth)) 
                Debug.LogError("Enemy doesn't have HealthManager"); // Failsafe if an Enemy has no health manager script
            _target = enemyHealth;
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
