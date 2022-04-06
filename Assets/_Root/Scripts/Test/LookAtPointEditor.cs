using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Test
{
    [CustomEditor(typeof(LookAtPoint))]
    [CanEditMultipleObjects]
    public class LookAtPointEditor : Editor
    {
        SerializedProperty lookAtPoint;
        SerializedObject so;

        void OnEnable()
        {
            lookAtPoint = serializedObject.FindProperty("point");
            Debug.Log("OnEnable in Editor.");
            
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(lookAtPoint);
            if(lookAtPoint.vector3Value.y > (target as LookAtPoint).transform.position.y)
            {
                EditorGUILayout.LabelField("(Above this object)");
            }
            else
            {
                EditorGUILayout.LabelField("(Below this object)");
            }


            serializedObject.ApplyModifiedProperties();
        }

        public void OnSceneGUI()
        {
            var t = (target as LookAtPoint);

            EditorGUI.BeginChangeCheck();
            Vector3 pos = Handles.PositionHandle(t.point, Quaternion.identity);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, "Move point");
                t.point = pos;
                t.Update();
                Debug.Log("OnScenGUI EditorGUI.EndChandeCheck");
            }
        }

    }
}