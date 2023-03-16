using UnityEngine;

namespace Scripts
{
    public class ApplyDamage : MonoBehaviour
    {
        [SerializeField] private int _damage;
        [SerializeField] private CreatureDef _defDamage;

        private void Start()
        {
            _damage = _defDamage.Damage;
        }

        public void Apply(GameObject target)
        {
            if (target.TryGetComponent<HealthComponent>(out HealthComponent health))
            {
                health.ModifyHealth(_damage, gameObject);
            }
        }
    }
}