using Scripts.ObjectPool;
using System.Collections;
using UnityEngine;

namespace Scripts
{
    public class RandomSpawner : MonoBehaviour
    {
        [SerializeField] private float _spawnDelay;
        [SerializeField] public int _countOfObjects;
        [SerializeField] private GameObject _prefab;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private Vector3 _volume;
        [SerializeField] private Vector3 _sizeCollider;

        private Collider[] _colliders;
        private GameObject obj;
        private bool _checkCollision;

        private void Awake()
        {
            StartCoroutine(SpawnField());
        }

        private IEnumerator SpawnField()
        {
            int i = 0;

            while (i < _countOfObjects)
            {
                var position = new Vector3(Random.Range(
                    _spawnPoint.position.x - _volume.x,
                    _spawnPoint.position.x + _volume.x),
                    _spawnPoint.position.y,
                Random.Range(
                    _spawnPoint.position.z - _volume.z,
                    _spawnPoint.position.z + _volume.z));

                _checkCollision = CheckSpawnPoint(position);

                if (_checkCollision)
                {
                    obj = Pool.Instanse.Get(_prefab, position);
                    ObjectsList.Objects.Add(obj);

                    i++;

                    yield return new WaitForSeconds(_spawnDelay);
                }
                else
                {
                    yield return null;
                }
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