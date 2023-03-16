using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public static class ObjectsList
    {
        public static List<GameObject> Objects = new List<GameObject>();

        public static void RemoveFromList(GameObject go)
        {
            Objects.Remove(go);
        }
    }
}