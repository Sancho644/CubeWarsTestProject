using UnityEngine;

namespace Scripts
{
    public class ApplyDamage : MonoBehaviour
    {
        [SerializeField] private int _hpDelta;

        public void Apply(GameObject target)
        {
            if (target.TryGetComponent<HealthComponent>(out HealthComponent health))
            {
                health.ModifyHealth(_hpDelta);
            }
        }
    }
}