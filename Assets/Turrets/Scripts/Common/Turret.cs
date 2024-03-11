using System;
using Building_Placement;
using Enemy;
using UnityEngine;

namespace Turrets.Scripts.Common
{
    public class Turret : GridItem
    {
        protected float     range;
        protected int     damagePerHit;
        protected float     shotsPerSecond;
        protected int       level;
        protected EnemyHealthManager target;
        protected Transform selfTransform;
        protected LayerMask enemyLayer;
        protected float     lastTimeShot;
        
        private void Awake()
        {
            selfTransform = transform;
        }

        private void Update()
        {
            lastTimeShot -= Time.deltaTime;
        }

        public virtual void Shoot() {}

        protected void AssessTarget()
        {
            if (target && (target.transform.position - selfTransform.position).magnitude < range) return; // Target id Valid
            Physics.SphereCast(selfTransform.position, range, Vector3.forward, out var HitInfo, 0f, enemyLayer);
            if (HitInfo.transform.TryGetComponent<EnemyHealthManager>(out var healthManager))
                target = healthManager;
        }
    }
}
