using Scripts.ObjectPool;
using UnityEngine;

namespace Scripts.Widgets
{
    public class LifeBarWidjet : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private ProgressBarWidget _lifeBar;
        [SerializeField] private HealthComponent _hp;

        private PoolItem _poolItem;
        private int _maxHp;

        private void Awake()
        {
            _poolItem = GetComponentInParent<PoolItem>();
        }

        private void Start()
        {
            if (_hp == null)
            {
                _hp = GetComponentInParent<HealthComponent>();
            }

            _maxHp = _hp.Health;

            _poolItem.OnRestart += OnRestart;
            _hp.OnChangeHealth += OnChangeHealth;
            _hp.OnDie += OnDie;
        }

        private void OnRestart()
        {
            _maxHp = _hp.Health;
            OnChangeHealth(_maxHp);

            gameObject.SetActive(true);
        }

        private void OnDie()
        {
            gameObject.SetActive(false);
        }

        private void OnChangeHealth(int hp)
        {
            var progress = (float)hp / _maxHp;
            _lifeBar.SetProgress(progress);
        }

        private void OnDestroy()
        {
            _poolItem.OnRestart -= OnRestart;
            _hp.OnChangeHealth -= OnChangeHealth;
            _hp.OnDie -= OnDie;
        }
    }
}