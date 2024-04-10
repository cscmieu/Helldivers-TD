using System.Collections.Generic;
using Building_Placement;
using Enemy;
using UnityEngine;

namespace Turrets.Scripts.Common
{
    public class Turret : GridItem
    {
        [SerializeField] protected TurretScriptableObject   turretData;
        [SerializeField] protected Transform                muzzlePoint;
        [SerializeField] protected LayerMask                enemyLayer;
        protected                  float                    range;
        protected                  int                      damagePerHit;
        protected                  float                    timeBetweenShots;
        protected                  int                      level;
        protected                  Transform                selfTransform;
        protected                  float                    lastTimeShot;
        protected                  SphereCollider           targettingArea;
        [SerializeField] protected                  List<EnemyHealthManager> targetList = new List<EnemyHealthManager>();
        
        protected void Awake()
        {
            selfTransform          = transform;
            range                  = turretData.range;
            damagePerHit           = turretData.damagePerBullet;
            timeBetweenShots       = 1 / turretData.shotsPerSecond;
            targettingArea        = GetComponent<SphereCollider>();
            targettingArea.radius = range;
        }

        public virtual void Shoot() {}

        protected void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent<EnemyHealthManager>(out var enemy)) return;
            targetList.Add(enemy);
        }
        
        protected void OnTriggerExit(Collider other)
        {
            if (!other.TryGetComponent<EnemyHealthManager>(out var enemy)) return;
            targetList.Remove(enemy);
        }

        public virtual void LevelUp() {}
    }
}
