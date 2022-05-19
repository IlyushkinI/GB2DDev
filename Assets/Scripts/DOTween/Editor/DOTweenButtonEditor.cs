using UnityEditor;
using UnityEditor.UI;
using UnityEngine;


namespace DOTween
{
    [CustomEditor(typeof(DOTweenButtonView))]
    public class DOTweenButtonEditor : ButtonEditor
    {

        private SerializedProperty m_InteractableProperty;

        protected override void OnEnable()
        {
            base.OnEnable();
            m_InteractableProperty = serializedObject.FindProperty(DOTweenButtonView.ButtonRectField);
        }

        public override void OnInspectorGUI()
        {
            Addon();
            base.OnInspectorGUI();
        }

        public void Addon()
        {
            serializedObject.Update();

            GUILayout.Space(20);
            EditorGUILayout.PropertyField(m_InteractableProperty);
            GUILayout.Space(20);

            EditorGUI.BeginChangeCheck();
            serializedObject.ApplyModifiedProperties();
        }

    }
}