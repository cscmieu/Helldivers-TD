using System.Collections;
using Turrets.Scripts.Common;
using UnityEngine;

namespace Turrets.Scripts.Objects
{
    public class SniperTurret : Turret
    {
        private TrailRenderer _bulletTracer;

        private void Start()
        {
            _bulletTracer = GetComponent<TrailRenderer>();
        }
        
        private void Update()
        {
            lastTimeShot -= Time.deltaTime;
            if (targetList.Count == 0) return;
            selfTransform.LookAt(targetList[0].transform);
            Shoot();
        }

        public override void Shoot()
        {
            if (lastTimeShot > 0) return;
            targetList[0].TakeDamage(damagePerHit);
            var tracer = Instantiate(_bulletTracer, muzzlePoint.position, Quaternion.identity);
            StartCoroutine(GenerateTracer(tracer, targetList[0].transform.position));
            lastTimeShot = 1f / timeBetweenShots;
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

        public override void LevelUp()
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
