using System.Collections;
using UnityEngine;

namespace Scripts
{
    public class RandomSpawner : MonoBehaviour
    {
        [SerializeField] private float _spawnDelay;
        [SerializeField] private float _destroyDelay;
        [SerializeField] private float _startSpawnDelay;
        [SerializeField] public int _countOfObjects;
        [SerializeField] private bool _destroyObject;
        [SerializeField] private GameObject[] _prefabs;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private Vector3 _volume;
        [SerializeField] private Vector3 _sizeCollider;
        [SerializeField] private ObjectsList _list;

        private Collider[] _colliders;
        private GameObject obj;
        private bool _checkCollision;

        private void Awake()
        {
            StartCoroutine(SpawnField());
        }

        private IEnumerator SpawnField()
        {
            yield return new WaitForSeconds(_startSpawnDelay);

            int i = 0;

            while (i < _countOfObjects)
            {
                var position = new Vector3(Random.Range(_spawnPoint.position.x - _volume.x, _spawnPoint.position.x + _volume.x),
                _spawnPoint.position.y,
                Random.Range(_spawnPoint.position.z - _volume.z, _spawnPoint.position.z + _volume.z));

                _checkCollision = CheckSpawnPoint(position);

                if (_checkCollision)
                {
                    int rand = Random.Range(0, _prefabs.Length);
                    var rotation = _prefabs[rand].transform.rotation;

                    obj = Instantiate(_prefabs[rand], position, rotation);

                    _list.Objects.Add(obj);

                    if (_destroyObject)
                    {
                        Destroy(obj, _destroyDelay);
                    }

                    SetTarget();
                    i++;

                    yield return new WaitForSeconds(_spawnDelay);
                }
                else
                {
                    yield return null;
                }
            }
        }

        private void SetTarget()
        {
            foreach (var target in _list.Objects)
            {
                target.GetComponent<CreatureAI>().StartSearchEnemy();
            }
        }

        private bool CheckSpawnPoint(Vector3 position)
        {
            _colliders = Physics.OverlapBox(position, _sizeCollider);

            if (_colliders.Length > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}