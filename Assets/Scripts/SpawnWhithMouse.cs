using Scripts.ObjectPool;
using UnityEngine;

namespace Scripts
{
    public class SpawnWhithMouse : MonoBehaviour
    {
        [SerializeField] private GameObject _spawnObject;
        [SerializeField] private Camera _camera;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    var obj = Pool.Instanse.Get(_spawnObject, hit.point);
                    ObjectsList.Objects.Add(obj);
                }
            }
        }
    }
}