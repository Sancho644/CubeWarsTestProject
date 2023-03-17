using System;
using UnityEngine;

namespace Scripts
{
    public class HealthComponent : MonoBehaviour
    {
        [SerializeField] private int _health;
        [SerializeField] private CreatureDef _defHealth;
        [SerializeField] private ObjectsList _list;

        public event Action<int> OnChangeHealth;
        public event Action OnDie;
        public int Health => _health;

        private void Awake()
        {
            _health = _defHealth.Health;
        }

        public void ModifyHealth(int healthDelta, GameObject go)
        {
            _health -= healthDelta;
            OnChangeHealth?.Invoke(_health);

            if (_health <= 0)
            {
                _list.RemoveFromList(gameObject);
                OnDie?.Invoke();
                go.GetComponentInParent<CreatureAI>().ModifyScore();
                Destroy(gameObject);
            }
        }
    }
}