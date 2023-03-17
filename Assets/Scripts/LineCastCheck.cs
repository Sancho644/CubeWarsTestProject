using System.Linq;
using UnityEngine;

namespace Scripts
{
    public class LineCastCheck : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private ApplyDamage _damage;
        [SerializeField] private float _radius;
        [SerializeField] private LayerMask _layer;
        [SerializeField] private string[] _tag;

        public void Check()
        {
            var size = Physics.OverlapSphere(_target.position, _radius, _layer);

            for (int i = 0; i < size.Length; i++)
            {
                var isInTags = _tag.Any(tag => size[i].CompareTag(tag));
                if (isInTags)
                {
                    _damage.Apply(size[i].gameObject);
                }
            }
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(_target.position, _radius);
        }
#endif
    }
}