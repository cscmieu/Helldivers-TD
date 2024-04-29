using UnityEngine;

namespace Turrets.Scripts.Common
{
    [CreateAssetMenu(fileName = "Default Turret Data", menuName = "ScriptableObjects/TurretDataScriptable", order = 1)]
    public class TurretScriptableObject : ScriptableObject
    {
        [Header("Statistics")]
        public string turretName;
        public float range;
        public int damagePerBullet;
        public float shotsPerSecond;
        public int turretCost;

        [Header("Upgrades")] 
        public string[] upgradeStats;
        public float[]  upgradeValues;
    }
}
