using UnityEditor;
using UnityEditor.UI;
using UnityEngine;


namespace DOTween
{
    [CustomEditor(typeof(DOTweenButtonView))]
    public class DOTweenButtonEditor : ButtonEditor
    {

        private SerializedProperty _buttonRect;
        private SerializedProperty _window;

        protected override void OnEnable()
        {
            base.OnEnable();
            _buttonRect = serializedObject.FindProperty(DOTweenButtonView.ButtonRectField);
            _window = serializedObject.FindProperty(DOTweenButtonView.Window);
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
            EditorGUILayout.PropertyField(_buttonRect);
            GUILayout.Space(10);
            EditorGUILayout.PropertyField(_window);
            GUILayout.Space(20);
            EditorGUI.BeginChangeCheck();
            serializedObject.ApplyModifiedProperties();
        }

    }
}