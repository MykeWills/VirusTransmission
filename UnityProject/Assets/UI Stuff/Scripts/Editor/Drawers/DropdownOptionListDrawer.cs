﻿using UnityEditor;
using UnityEditorInternal;
using UnityEngine;


namespace FaceHornEditor.UI {

    [CustomPropertyDrawer(typeof(FaceHorn.UI.Dropdown.OptionDataList), true)]
    internal class DropdownOptionListDrawer : PropertyDrawer
    {
        private ReorderableList m_ReorderableList;

        private void Init(SerializedProperty property) {

            if (m_ReorderableList != null) {
                return;
            }
            SerializedProperty propertyRelative = property.FindPropertyRelative("m_Options");
            this.m_ReorderableList = new ReorderableList(property.serializedObject, propertyRelative);
            this.m_ReorderableList.drawElementCallback = new ReorderableList.ElementCallbackDelegate(this.DrawOptionData);
            this.m_ReorderableList.drawHeaderCallback = new ReorderableList.HeaderCallbackDelegate(this.DrawHeader);
            this.m_ReorderableList.elementHeight += 16f;
        }


        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            this.Init(property);
            this.m_ReorderableList.DoList(position);
        }


        private void DrawHeader(Rect rect) {
            GUI.Label(rect, "Options");
        }


        private void DrawOptionData(Rect rect, int index, bool isActive, bool isFocused) {
            SerializedProperty arrayElementAtIndex = this.m_ReorderableList.serializedProperty.GetArrayElementAtIndex(index);
            SerializedProperty propertyRelative1 = arrayElementAtIndex.FindPropertyRelative("m_Text");
            SerializedProperty propertyRelative2 = arrayElementAtIndex.FindPropertyRelative("m_Image");
            rect = new RectOffset(0, 0, -1, -3).Add(rect);
            rect.height = EditorGUIUtility.singleLineHeight;
            EditorGUI.PropertyField(rect, propertyRelative1, GUIContent.none);
            rect.y += EditorGUIUtility.singleLineHeight;
            EditorGUI.PropertyField(rect, propertyRelative2, GUIContent.none);
        }


        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            this.Init(property);
            return this.m_ReorderableList.GetHeight();
        }
    }
}
