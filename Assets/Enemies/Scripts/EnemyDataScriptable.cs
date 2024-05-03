using UnityEngine;

namespace Enemies.Scripts
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/EnemyDataScriptable", order = 1)]
    public class EnemyDataScriptable : ScriptableObject
    {
        public string enemyName;
        public float  speed;        // Enemy movement speed.
        public int    maxHitPoints; // Enemy max health on initialization.
        public int    score;        // Score gained (or lost) on enemy death.
        public int    money;        // Money gained on enemy death.
        public int    level;        // Level, acting as a multiplier for stats
    }
}
