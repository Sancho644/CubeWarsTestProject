using Scripts.ObjectPool;
using UnityEngine;

namespace Scripts
{
    public class ApplyDamage : MonoBehaviour
    {
        [SerializeField] private int _damage;
        [SerializeField] private CreatureDef _defDamage;

        private PoolItem _poolItem;

        private void Awake()
        {
            _poolItem = GetComponentInParent<PoolItem>();
            _poolItem.OnRestart += OnRestart;
        }

        private void Start()
        {
            _damage = _defDamage.Damage;
        }

        private void OnRestart()
        {
            _damage = _defDamage.Damage;
        }

        public void OnUpDamage()
        {
            _damage++;
        }

        public void Apply(GameObject target)
        {
            if (target.TryGetComponent(out HealthComponent health))
            {
                health.ModifyHealth(_damage, gameObject);
            }
        }

        private void OnDestroy()
        {
            _poolItem.OnRestart -= OnRestart;
        }
    }
}