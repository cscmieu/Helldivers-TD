using System.Collections;
using Enemy.Scripts;
using UnityEngine;

namespace Turrets.Scripts.Common
{
    public class Turret : MonoBehaviour
    {
        [SerializeField] private TurretScriptableObject turretData;
        [SerializeField] private LayerMask              enemyLayer;
        [SerializeField] private Transform              muzzlePoint;
        [SerializeField] private TrailRenderer          bulletTracer;
        private                  EnemyHealthManager     _target;
        private                  float                  _range;
        private                  int                    _damagePerHit;
        private                  int                    _turretCost;
        private                  float                  _timeBetweenShots;
        private                  int                    _level;
        private                  float                  _lastTimeShot;
        private                  Camera                 _mainCamera;
        private                  Transform              _mainCameraTransform;

        private void Awake()
        {
            _range                 = turretData.range;
            _damagePerHit          = turretData.damagePerBullet;
            _turretCost            = turretData.turretCost;
            _timeBetweenShots      = 1f / turretData.shotsPerSecond;
            _mainCamera = Camera.main;
            if (_mainCamera is not null) _mainCameraTransform = _mainCamera.transform;
        }

        private void Update()
        {
            _lastTimeShot -= Time.deltaTime;
            AssessTarget();
            Shoot();
        }

        private void Shoot()
        {
            if (_lastTimeShot > 0) return;
            if (_target is null) return; //Check if target is valid
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
                time                            += Time.deltaTime * tracer.time;
                yield return null;
            }
            tracer.transform.position = target;
            Destroy(tracer, tracer.time);
        }

        private void AssessTarget()
        {
            if (_target is not null && !_target.IsDoomed()) return; // Already have a living target
            var enemiesInRange = Physics.OverlapSphere(transform.position, _range, enemyLayer);
            if (enemiesInRange.Length == 0) return; // No Enemies in Range
            if (!enemiesInRange[0].TryGetComponent<EnemyHealthManager>(out var enemyHealth)) 
                Debug.LogError("Enemy doesn't have HealthManager"); // Failsafe if an Enemy has no health manager script
            _target = enemyHealth;
        }

        public void LevelUp()
        {
            _level++;
            switch (_level)
            {
                case 1 :
                    _damagePerHit = 15;
                    break;
                case 2 :
                    _timeBetweenShots = 1;
                    break;
                case 3 :
                    _damagePerHit = 20;
                    break;
                case 4 :
                    _range                 = 10;
                    break;
            }
        }

        public int GetCost()
        {
            return _turretCost;
        }
    }
}
