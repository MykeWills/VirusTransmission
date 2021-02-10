using UnityEditor;
using UnityEngine;

namespace FaceHornEditor.UI {

    [CustomEditor(typeof(FaceHorn.UI.Slider), true)]
    [CanEditMultipleObjects]
    public class SliderEditor : SelectableEditor {
        
        private SerializedProperty m_MinValue;
        private SerializedProperty m_MaxValue;
        private SerializedProperty m_ChangeByValue;
        private SerializedProperty m_WholeNumbers;
        private SerializedProperty m_Value;
        private SerializedProperty m_OnValueChanged;
        private SerializedProperty m_FillRect;
        private SerializedProperty m_HandleRect;


        protected override void OnEnable() {

            base.OnEnable();
            
            m_MinValue = base.serializedObject.FindProperty("m_MinValue");
            m_MaxValue = base.serializedObject.FindProperty("m_MaxValue");
            m_ChangeByValue = base.serializedObject.FindProperty("m_ChangeByValue");
            m_WholeNumbers = base.serializedObject.FindProperty("m_WholeNumbers");
            m_Value = base.serializedObject.FindProperty("m_Value");
            m_OnValueChanged = base.serializedObject.FindProperty("m_OnValueChanged");
            m_FillRect = base.serializedObject.FindProperty("m_FillRect");
            m_HandleRect = base.serializedObject.FindProperty("m_HandleRect");

        }

        public override void OnInspectorGUI() {

            base.OnInspectorGUI();

            if (m_Value.floatValue > m_MaxValue.floatValue) {
                m_Value.floatValue = m_MaxValue.floatValue;
            }
            else if (m_Value.floatValue < m_MinValue.floatValue) {
                m_Value.floatValue = m_MinValue.floatValue;
            }

            if (m_WholeNumbers.boolValue) {
                m_Value.floatValue = Mathf.Round(m_Value.floatValue);
            }

            
            EditorGUILayout.PropertyField(this.m_FillRect);
            EditorGUILayout.PropertyField(this.m_HandleRect);

            //EditorGUILayout.PropertyField(this.m_Direction);  // doesn't do anything yet
            EditorGUILayout.PropertyField(this.m_MinValue);
            EditorGUILayout.PropertyField(this.m_MaxValue);
            EditorGUILayout.PropertyField(this.m_ChangeByValue);
            EditorGUILayout.PropertyField(this.m_WholeNumbers);


            EditorGUILayout.Slider(this.m_Value, this.m_MinValue.floatValue, this.m_MaxValue.floatValue);

            float valueRatio = (m_Value.floatValue - m_MinValue.floatValue) / (m_MaxValue.floatValue - m_MinValue.floatValue);

            //Debug.Log(m_FillRect.name);
            (m_FillRect.objectReferenceValue as RectTransform).anchorMax = new Vector2(valueRatio, 1);
            (m_HandleRect.objectReferenceValue as RectTransform).anchorMin = new Vector2(valueRatio, 0);
            (m_HandleRect.objectReferenceValue as RectTransform).anchorMax = new Vector2(valueRatio, 1);

            EditorGUILayout.Space();
            EditorGUILayout.Space();

            EditorGUILayout.PropertyField(this.m_OnValueChanged);

            this.serializedObject.ApplyModifiedProperties();
        }
    }
}
