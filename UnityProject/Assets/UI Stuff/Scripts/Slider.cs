using System;
using UnityEngine;
using UnityEngine.Events;

namespace FaceHorn.UI {
    [AddComponentMenu("FaceHorn/UI/Slider", 2)]
    [RequireComponent(typeof(RectTransform))]
    public class Slider : Selectable {

        [SerializeField] private Direction m_Direction = Direction.LeftToRight;
        [SerializeField] private float m_MinValue = 0;
        [SerializeField] private float m_MaxValue = 1;
        [SerializeField] private float m_ChangeByValue = 0.1f;
        [SerializeField] private bool m_WholeNumbers = false;
        [SerializeField] private RectTransform m_FillRect;
        [SerializeField] private RectTransform m_HandleRect;

        [SerializeField] protected float m_Value;
        private Vector2 m_Offset;
        [SerializeField] private MoveDirections moveDirection; // Un Serialize
        [SerializeField] private SliderEvent m_OnValueChanged = new Slider.SliderEvent(); // Un Serialize

        //public static float Slider(float value, float leftValue, float rightValue, params GUILayoutOption[] options);

        protected Slider() {
        }
        
        public RectTransform FillRect {
            get {
                return m_FillRect;
            }
            set {
                m_FillRect = value;
            }
        }
        public RectTransform HandleRect {
            get {
                return m_HandleRect;
            }
            set {
                m_HandleRect = value;
            }
        }

        public Slider.SliderEvent onValueChanged {
            get {
                return this.m_OnValueChanged;
            }
            set {
                this.m_OnValueChanged = value;
            }
        }

        public float minValue {
            get {
                return m_MinValue;
            }
            set {
                m_MinValue = value;
            }
        }

        public float maxValue {
            get {
                return m_MaxValue;
            }
            set {
                m_MaxValue = value;
            }
        }

        public float changeByValue {
            get {
                return m_ChangeByValue;
            }
            set {
                m_ChangeByValue = value;
            }
        }

        public bool wholeNumbers {
            get {
                return m_WholeNumbers;
            }
            set {
                m_WholeNumbers = value;
            }
        }

        public float value {
            get {
                if (this.wholeNumbers)
                    return Mathf.Round(this.m_Value);
                return this.m_Value;
            }
            set {
                m_Value = value;
                this.Set(this.m_Value);
            }
        }

        public Direction direction {
            get {
                return this.m_Direction;
            }
            set {
                m_Direction = value;
            }
        }
#if UNITY_EDITOR
        protected override void OnValidate() {
            base.OnValidate();
            if (wholeNumbers) {
                m_MinValue = Mathf.Round(m_MinValue);
                m_MaxValue = Mathf.Round(m_MaxValue);
                m_ChangeByValue = Mathf.Round(m_ChangeByValue);
            }

            UpdateSlider();
        }
#endif
        

        public void UpdateSlider() {

            float valueRatio = (m_Value - m_MinValue) / (m_MaxValue - m_MinValue);
            
            (m_FillRect as RectTransform).anchorMax = new Vector2(valueRatio, 1);
            (m_HandleRect as RectTransform).anchorMin = new Vector2(valueRatio, 0);
            (m_HandleRect as RectTransform).anchorMax = new Vector2(valueRatio, 1);


        }
        

        private float ClampValue(float input) {
            float f = Mathf.Clamp(input, this.minValue, this.maxValue);
            if (this.wholeNumbers)
                f = Mathf.Round(f);
            return f;

        }

        private void Set(float input) {
            this.Set(input, true);
        }

        protected virtual void Set(float input, bool sendCallback) {

            float num = this.ClampValue(input);
            if (this.m_Value == num)
                return;
            this.m_Value = num;
            if (!sendCallback)
                return;
            UISystemProfilerApi.AddMarker("Slider.value", (UnityEngine.Object) this);
            
        }

        
        public enum Direction {
            LeftToRight,
            RightToLeft,
            BottomToTop,
            TopToBottom,
        }

        protected enum MoveDirections {
            Up,
            Down,
            Left,
            Right,
            None
        }

        [Serializable]
        public class SliderEvent : UnityEvent<float> {
        }
    }
}
