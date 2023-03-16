using Scripts.ObjectPool;
using System;
using UnityEngine;

namespace Scripts
{
    public class HealthComponent : MonoBehaviour
    {
        [SerializeField] private int _health;
        [SerializeField] private CreatureDef _defHealth;

        public event Action<int> OnChangeHealth;
        public event Action OnDie;
        public int Health => _health;
        private PoolItem _poolItem;

        private void Awake()
        {
            _poolItem = GetComponent<PoolItem>();

            _poolItem.OnRestart += OnRestart;
            _health = _defHealth.Health;
        }

        private void OnRestart()
        {
            _health = _defHealth.Health;
        }

        public void ModifyHealth(int healthDelta, GameObject go)
        {
            _health -= healthDelta;
            OnChangeHealth?.Invoke(_health);

            if (_health <= 0)
            {
                if (go != null)
                {
                    go.GetComponentInParent<CreatureAI>().ModifyScore();
                    go.GetComponentInParent<ApplyDamage>().OnUpDamage();
                }

                _poolItem.Relese();
                ObjectsList.RemoveFromList(gameObject);
                OnDie?.Invoke();
            }
        }

        private void OnDestroy()
        {
            _poolItem.OnRestart -= OnRestart;
        }
    }
}