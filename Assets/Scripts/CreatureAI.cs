using Scripts.ObjectPool;
using System;
using UnityEngine;
using UnityEngine.AI;

namespace Scripts
{
    public class CreatureAI : MonoBehaviour
    {
        [SerializeField] private LineCastCheck _check;
        [SerializeField] private float _attackCooldown = 1f;

        private PoolItem _poolItem;
        private NavMeshAgent _agent;
        private GameObject _object;
        private float _distance;
        private float _lastAttack;
        private int _score;
        private int _defaultScore;

        public event Action<int> OnChangeScore;

        private void Awake()
        {
            _poolItem = GetComponent<PoolItem>();
            _agent = GetComponent<NavMeshAgent>();
        }

        private void Start()
        {
            _defaultScore = _score;

            _poolItem.OnRestart += OnRestart;
        }

        private void Update()
        {
            if (_object == null || !_object.activeInHierarchy)
            {
                StartSearchEnemy();
                return;
            }

            _distance = Vector3.Distance(transform.position, _object.transform.position);

            if (_distance > _agent.stoppingDistance)
            {
                _agent.destination = _object.transform.position;
            }

            if (_distance <= _agent.stoppingDistance) Attack();
        }

        private void OnRestart()
        {
            _score = _defaultScore;
        }

        public void ModifyScore()
        {
            _score++;
            _object = null;
            OnChangeScore?.Invoke(_score);
        }

        private void Attack()
        {
            if (Time.time - _lastAttack < _attackCooldown) return;

            _lastAttack = Time.time;

            transform.LookAt(_object.transform);
            _check.Check();
        }

        public void StartSearchEnemy()
        {
            var listCount = ObjectsList.Objects.Count;

            for (int i = 0; i < listCount; i++)
            {
                var rand = UnityEngine.Random.Range(0, listCount);
                var cube = ObjectsList.Objects[rand];

                if (gameObject == cube) break;

                _object = ObjectsList.Objects[rand];
            }
        }
    }
}