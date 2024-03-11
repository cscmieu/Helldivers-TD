using UnityEngine;

namespace Enemy
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/EnemyDataScriptable", order = 1)]
    public class EnemyDataScriptable : ScriptableObject
    {
        public string prefabName;

        public float speed; // Enemy movement speed.
        public int maxHitPoints; // Enemy max health on initialization.
        public float score; // Score gained (or lost) on enemy death.
        public float money; // Money gained on enemy death.
    }
}
