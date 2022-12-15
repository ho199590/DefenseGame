using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace MText
{
    [CustomEditor(typeof(CircularLayoutGroup))]
    public class CircularLayoutGroupEditor : Editor
    {
        CircularLayoutGroup myTarget;
        SerializedObject soTarget;

        SerializedProperty autoItemSize;
        SerializedProperty direction;
        SerializedProperty style;

        SerializedProperty spread;
        SerializedProperty radius;
        SerializedProperty radiusDecreaseRate;

        void OnEnable()
        {
            myTarget = (CircularLayoutGroup)target;
            soTarget = new SerializedObject(target);

            FindProperties();
        }

        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(style, GUIContent.none);
            EditorGUILayout.PropertyField(direction, GUIContent.none);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.PropertyField(spread);
            EditorGUILayout.PropertyField(radius);
            EditorGUILayout.PropertyField(radiusDecreaseRate);

            DrawAutoItemSize();

            if (EditorGUI.EndChangeCheck())
            {
                if (soTarget.ApplyModifiedProperties())
                {
                    if (myTarget.GetComponent<Modular3DText>())
                        myTarget.GetComponent<Modular3DText>().CleanUpdateText();
                }
                EditorUtility.SetDirty(myTarget);
            }
        }

        private void DrawAutoItemSize()
        {
            if (!myTarget.GetComponent<Modular3DText>())
            {
                EditorGUILayout.PropertyField(autoItemSize);
            }
        }

        void FindProperties()
        {
            autoItemSize = soTarget.FindProperty("autoItemSize");
            direction = soTarget.FindProperty("direction");
            style = soTarget.FindProperty("style");

            spread = soTarget.FindProperty("spread");
            radius = soTarget.FindProperty("radius");
            radiusDecreaseRate = soTarget.FindProperty("radiusDecreaseRate");
        }
    }
}