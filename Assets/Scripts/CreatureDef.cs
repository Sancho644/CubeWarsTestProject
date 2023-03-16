using UnityEngine;

namespace Scripts
{
    [CreateAssetMenu(menuName = "Defs/CreatureDef", fileName = "CreatureDef")]
    public class CreatureDef : ScriptableObject
    {
        [SerializeField] [Range(0, 100)] private int _health;
        [SerializeField] [Range(5, 50)] private int _damage;

        public int Health => _health;
        public int Damage => _damage;
    }
}