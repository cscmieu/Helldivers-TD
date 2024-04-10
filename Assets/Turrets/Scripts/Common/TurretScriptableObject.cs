using UnityEngine;

namespace Turrets.Scripts.Common
{
    [CreateAssetMenu(fileName = "Default Turret Data", menuName = "ScriptableObjects/TurretDataScriptable", order = 1)]
    public class TurretScriptableObject : ScriptableObject
    {
        public string turretName;
        public float range;
        public int damagePerBullet;
        public float shotsPerSecond;
    }
}
