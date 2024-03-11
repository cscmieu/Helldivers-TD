using Turrets.Scripts.Common;

namespace Turrets.Scripts.Objects
{
    public class SniperTurret : Turret
    {
        private void Update()
        {
            AssessTarget();
            if (!target) return;
            selfTransform.LookAt(target.transform);
            Shoot();
        }

        public override void Shoot()
        {
            if (lastTimeShot > 0) return;
            target.TakeDamage(damagePerHit);
        }
    }
}
