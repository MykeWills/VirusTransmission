using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace FaceHorn.UI {
    [AddComponentMenu("FaceHorn/UI/Button", 0)]
    [RequireComponent(typeof(RectTransform))]
    public class Button : Selectable {

        [FormerlySerializedAs("onClick")]
        [SerializeField]
        private Button.ButtonClickedEvent m_OnClick = new Button.ButtonClickedEvent();
        
        public Button.ButtonClickedEvent onClick {
            get {
                return this.m_OnClick;
            }
            set {
                this.m_OnClick = value;
            }
        }


        [Serializable]
        public class ButtonClickedEvent : UnityEvent {
        }
    }
}
