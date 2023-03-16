using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class ObjectsList : MonoBehaviour
    {
        public List<GameObject> Objects = new List<GameObject>();

        public void RemoveFromList(GameObject go)
        {
            Objects.Remove(go);
        }
    }
}