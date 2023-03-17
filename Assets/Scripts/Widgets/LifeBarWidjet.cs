using UnityEngine;

namespace Scripts.Widgets
{
    public class LifeBarWidjet : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private ProgressBarWidget _lifeBar;
        [SerializeField] private HealthComponent _hp;

        private int _maxHp;

        private void Start()
        {
            if (_hp == null)
            {
                _hp = GetComponentInParent<HealthComponent>();
            }

            _maxHp = _hp.Health;

            _hp.OnChangeHealth += OnChangeHealth;
            _hp.OnDie += OnDie;
        }

        private void OnDie()
        {
            Destroy(gameObject);
        }

        private void OnChangeHealth(int hp)
        {
            var progress = (float)hp / _maxHp;
            _lifeBar.SetProgress(progress);
        }

        private void OnDestroy()
        {
            _hp.OnChangeHealth -= OnChangeHealth;
            _hp.OnDie -= OnDie;
        }
    }
}