using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using FaceHorn.UI;

namespace FaceHornEditor.UI {
    [CustomEditor(typeof(FaceHorn.UI.Dropdown), true)]
    [CanEditMultipleObjects]
    public class DropdownEditor : SelectableEditor {

        private SerializedProperty m_Template;
        private SerializedProperty m_CaptionText;
        private SerializedProperty m_CaptionImage;
        private SerializedProperty m_ItemText;
        private SerializedProperty m_ItemImage;
        private SerializedProperty m_MaxDropdownSize;
        private SerializedProperty m_SizeOfEachItem;
        private SerializedProperty m_SpaceInbetweenItems;
        private SerializedProperty m_SpaceBetweenVerticalEdgesToItems;
        private SerializedProperty m_Value;
        private SerializedProperty m_Options;
        private SerializedProperty m_OnSelectionChanged;


        protected override void OnEnable() {
            base.OnEnable();

            this.m_Template = this.serializedObject.FindProperty("m_Template");
            this.m_CaptionText = this.serializedObject.FindProperty("m_CaptionText");
            this.m_CaptionImage = this.serializedObject.FindProperty("m_CaptionImage");
            this.m_ItemText = this.serializedObject.FindProperty("m_ItemText");
            this.m_ItemImage = this.serializedObject.FindProperty("m_ItemImage");
            this.m_MaxDropdownSize = this.serializedObject.FindProperty("m_MaxDropdownSize");
            this.m_SizeOfEachItem = this.serializedObject.FindProperty("m_SizeOfEachItem");
            this.m_SpaceInbetweenItems = this.serializedObject.FindProperty("m_SpaceInbetweenItems");
            this.m_SpaceBetweenVerticalEdgesToItems = this.serializedObject.FindProperty("m_SpaceBetweenVerticalEdgesToItems");
            this.m_Value = this.serializedObject.FindProperty("m_Value");
            this.m_Options = this.serializedObject.FindProperty("m_Options");
            this.m_OnSelectionChanged = this.serializedObject.FindProperty("m_OnValueChanged");

        }

        public override void OnInspectorGUI() {
            base.OnInspectorGUI();

            EditorGUILayout.PropertyField(this.m_Template);
            EditorGUILayout.PropertyField(this.m_CaptionText);
            EditorGUILayout.PropertyField(this.m_CaptionImage);
            EditorGUILayout.PropertyField(this.m_ItemText);
            EditorGUILayout.PropertyField(this.m_ItemImage);
            EditorGUILayout.PropertyField(this.m_MaxDropdownSize);
            EditorGUILayout.PropertyField(this.m_SizeOfEachItem);
            EditorGUILayout.PropertyField(this.m_SpaceInbetweenItems);
            EditorGUILayout.PropertyField(this.m_SpaceBetweenVerticalEdgesToItems);
            EditorGUILayout.PropertyField(this.m_Value);
            EditorGUILayout.PropertyField(this.m_Options);
            EditorGUILayout.PropertyField(this.m_OnSelectionChanged);




            this.serializedObject.ApplyModifiedProperties();
        }



    }
}

