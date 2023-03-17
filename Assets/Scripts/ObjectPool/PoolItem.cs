using System;
using UnityEngine;

namespace Scripts.ObjectPool
{
    public class PoolItem : MonoBehaviour
    {
        public event Action OnRestart;

        private int _id;
        private Pool _pool;

        public void Restart()
        {
            OnRestart?.Invoke();
        }

        public void Relese()
        {
            _pool.Relese(_id, this);
        }

        public void Retain(int id, Pool pool)
        {
            _id = id;
            _pool = pool;
        }
    }
}