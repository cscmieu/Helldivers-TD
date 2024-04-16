using System;
using System.Collections;
using System.Collections.Generic;
using Building_Placement;
using Enemy;
using Enemy.Scripts;
using UnityEngine;

namespace Turrets.Scripts.Common
{
    public class Turret : GridItem
    {
        [SerializeField] protected TurretScriptableObject   turretData;
        [SerializeField] protected Transform                muzzlePoint;
        [SerializeField] protected List<EnemyHealthManager> targetList = new List<EnemyHealthManager>();
        protected                  float                    range;
        protected                  int                      damagePerHit;
        protected                  float                    timeBetweenShots;
        protected                  int                      level;
        protected                  float                    lastTimeShot;
        protected                  SphereCollider           targettingArea;
        private                    TrailRenderer            _bulletTracer;
        
        protected void Awake()
        {
            range                 = turretData.range;
            damagePerHit          = turretData.damagePerBullet;
            timeBetweenShots      = 1f / turretData.shotsPerSecond;
            targettingArea        = GetComponent<SphereCollider>();
            targettingArea.radius = range;
            _bulletTracer         = GetComponent<TrailRenderer>();
        }

        private void Update()
        {
            lastTimeShot -= Time.deltaTime;
            Shoot();
        }

        public void Shoot()
        {
            if (lastTimeShot > 0) return;
            if (targetList.Count == 0) return;
            targetList[0].TakeDamage(damagePerHit);
            var tracer = Instantiate(_bulletTracer, muzzlePoint.position, Quaternion.identity);
            StartCoroutine(GenerateTracer(tracer, targetList[0].transform.position));
            lastTimeShot = timeBetweenShots;
        }
        
        private IEnumerator GenerateTracer(TrailRenderer trail, Vector3 hitPoint)
        {
            var time          = 0f;
            var startingPoint = trail.transform.position;

            while (time < 1)
            {
                trail.transform.position =  Vector3.Lerp(startingPoint, hitPoint, time);
                time                     += Time.deltaTime / trail.time;
                yield return null;
            }

            trail.transform.position = hitPoint;
            Destroy(trail.gameObject, trail.time);
        }

        protected void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent<EnemyHealthManager>(out var enemy)) return;
            targetList.Add(enemy);
            Debug.Log("Added Enemy");
        }
        
        protected void OnTriggerExit(Collider other)
        {
            if (!other.TryGetComponent<EnemyHealthManager>(out var enemy)) return;
            targetList.Remove(enemy);
            Debug.Log("Removed Enemy");
        }

        public void LevelUp()
        {
            level++;
            switch (level)
            {
                case 1 :
                    damagePerHit = 15;
                    break;
                case 2 :
                    timeBetweenShots = 1;
                    break;
                case 3 :
                    damagePerHit = 20;
                    break;
                case 4 :
                    range                 = 10;
                    targettingArea.radius = 10;
                    break;
            }
        }
    }
}
