using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Test
{

    [ExecuteInEditMode]
    public class LookAtPoint : MonoBehaviour
    {
        public Vector3 point = Vector3.zero;

        public void Update()
        {
            transform.LookAt(point);
        }
    }
}