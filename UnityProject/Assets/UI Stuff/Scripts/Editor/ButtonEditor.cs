using UnityEditor;
using UnityEngine;

namespace FaceHornEditor.UI
{
    [CustomEditor(typeof(FaceHorn.UI.Button), true)]
    [CanEditMultipleObjects]
    public class ButtonEditor : SelectableEditor {

        private SerializedProperty m_OnClick;


        protected override void OnEnable() {
            base.OnEnable();
            m_OnClick = base.serializedObject.FindProperty("m_OnClick");
        }

        public override void OnInspectorGUI() {
            base.OnInspectorGUI();

            EditorGUILayout.PropertyField(m_OnClick);

            this.serializedObject.ApplyModifiedProperties();
        }

    }
}
