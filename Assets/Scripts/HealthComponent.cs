using UnityEngine;

namespace Scripts
{
    public class HealthComponent : MonoBehaviour
    {
        [SerializeField] private int _health;
        [SerializeField] private ObjectsList _list;

        public void ModifyHealth(int healthDelta)
        {
            _health -= healthDelta;

            if (_health <= 0)
            {
                _list.RemoveFromList(gameObject);
                Destroy(gameObject);
            }
        }
    }
}