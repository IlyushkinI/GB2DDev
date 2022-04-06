using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UI;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using RaceMobile.Tweens;

namespace RaceMobile.Editor
{
    [CustomEditor(typeof(CustomButton))]
    public class CustomButtonEditor : ButtonEditor
    {
        public override VisualElement CreateInspectorGUI()
        {
            var root = new VisualElement();

            PropertyField transition = new PropertyField(serializedObject.FindProperty(CustomButton.TransitionFieldName));
            PropertyField easing = new PropertyField(serializedObject.FindProperty(CustomButton.EasingFieldName));
            PropertyField duration = new PropertyField(serializedObject.FindProperty(CustomButton.DurationFieldName));
            PropertyField power = new PropertyField(serializedObject.FindProperty(CustomButton.PowerFieldName));
            Label  lable = new Label("Tween settings");

            root.Add(new IMGUIContainer(OnInspectorGUI));
            root.Add(transition);
            root.Add(easing);
            root.Add(duration);
            root.Add(power);
            root.Add(lable);

            return root;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            serializedObject.Update();
            EditorGUI.BeginChangeCheck();
            serializedObject.ApplyModifiedProperties();
        }
    }
}