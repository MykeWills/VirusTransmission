using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace FaceHorn.UI {
    [AddComponentMenu("FaceHorn/UI/Toggle", 1)]
    [RequireComponent(typeof(RectTransform))]
    public class Toggle : Selectable {

        [SerializeField] private bool m_IsOn;
        [SerializeField] private Graphic m_Graphic;

        [FormerlySerializedAs("onValueChange")]
        [SerializeField]
        private Toggle.ToggleEvent m_OnValueChanged = new Toggle.ToggleEvent();

        public Graphic graphics {
            get {
                return m_Graphic;
            }
            set {
                m_Graphic = value;
            }
        }

        public bool isOn {
            get {
                return m_IsOn;
            }
            set {
                m_IsOn = value;
                RefreshShown();
            }
        }
#if UNITY_EDITOR
        protected override void OnValidate() {
            base.OnValidate();
            RefreshShown();
        }
#endif

        public void Submit() {
            ChangeValue();
            RefreshShown();
            m_OnValueChanged.Invoke(m_IsOn);
        }

        private void ChangeValue() {
            m_IsOn = !m_IsOn;
        }

        private void RefreshShown() {

            m_Graphic.gameObject.SetActive(m_IsOn);
        }


        public Toggle.ToggleEvent onValueChanged {
            get {
                return this.m_OnValueChanged;
            }
            set {
                this.m_OnValueChanged = value;
            }
        }

        [Serializable]
        public class ToggleEvent : UnityEvent<bool> {
        }
    }
}
