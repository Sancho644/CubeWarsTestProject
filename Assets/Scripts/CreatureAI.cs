using UnityEngine;
using UnityEngine.AI;

namespace Scripts
{
    public class CreatureAI : MonoBehaviour
    {
        [SerializeField] private ObjectsList _list;
        [SerializeField] private LineCastCheck _check;
        [SerializeField] private float _closeDistance = 1.5f;
        [SerializeField] private float _attackCooldown = 1f;

        private NavMeshAgent _agent;
        private GameObject _object;
        private float _distance;
        private float _lastAttack;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
        }

        private void Start()
        {
            StartSearchEnemy();
        }

        private void Update()
        {
            if (_object == null)
            {
                StartSearchEnemy();
                return;
            }

            _distance = Vector3.Distance(transform.position, _object.transform.position);

            if (_distance > _closeDistance)
            {
                _agent.enabled = true;
                _agent.destination = _object.transform.position;
            }

            if (_distance <= _closeDistance) Attack();
        }

        private void Attack()
        {
            if (Time.time - _lastAttack < _attackCooldown) return;

            _lastAttack = Time.time;
            _agent.enabled = false;

            if (_object == null)
            {
                StartSearchEnemy();
                return;
            }

            transform.LookAt(_object.transform);
            _check.Check();
        }

        public void StartSearchEnemy()
        {
            for (int i = 0; i < _list.Objects.Count; i++)
            {
                var rand = Random.Range(0, _list.Objects.Count);
                var id = _list.Objects[rand].GetInstanceID();

                if (gameObject.GetInstanceID() == id) continue;

                Debug.Log(_list.Objects[rand]);
                _object = _list.Objects[rand];
                break;
            }
        }
    }
}