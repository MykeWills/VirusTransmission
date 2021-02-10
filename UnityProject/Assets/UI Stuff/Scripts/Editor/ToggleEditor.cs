using UnityEditor;
using UnityEngine;

namespace FaceHornEditor.UI {

    [CustomEditor(typeof(FaceHorn.UI.Toggle), true)]
    [CanEditMultipleObjects]
    public class ToggleEditor : SelectableEditor {

        private SerializedProperty m_IsOn;

        private SerializedProperty m_OnValueChanged;
        private SerializedProperty m_Graphic;

        protected override void OnEnable() {
            base.OnEnable();
            m_IsOn = base.serializedObject.FindProperty("m_IsOn");
            m_Graphic = base.serializedObject.FindProperty("m_Graphic");
            m_OnValueChanged = base.serializedObject.FindProperty("m_OnValueChanged");
        }

        public override void OnInspectorGUI() {
            base.OnInspectorGUI();

            EditorGUILayout.PropertyField(m_IsOn);
            EditorGUILayout.PropertyField(m_Graphic);
            EditorGUILayout.PropertyField(m_OnValueChanged);


            this.serializedObject.ApplyModifiedProperties();
        }



    }
}
