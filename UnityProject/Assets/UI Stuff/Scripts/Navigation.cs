using System;
using UnityEngine;

namespace FaceHorn.UI {

    [Serializable]
    public struct Navigation : IEquatable<Navigation> {

        [SerializeField] private Mode m_Mode;

        [SerializeField] private Selectable m_SelectOnUp;
        [SerializeField] private Selectable m_SelectOnDown;
        [SerializeField] private Selectable m_SelectOnLeft;
        [SerializeField] private Selectable m_SelectOnRight;

        
        public Selectable selectOnUp {
            get {
                return m_SelectOnUp;
            }
            set {
                m_SelectOnUp = value;
            }
        }
        public Selectable selectOnDown
        {
            get {
                return m_SelectOnDown;
            }
            set {
                m_SelectOnDown = value;
            }
        }
        public Selectable selectOnLeft
        {
            get {
                return m_SelectOnLeft;
            }
            set {
                m_SelectOnUp = value;
            }
        }
        public Selectable selectOnRight
        {
            get {
                return m_SelectOnRight;
            }
            set {
                m_SelectOnRight = value;
            }
        }
        public Mode mode {
            get {
                return m_Mode;
            }
            set {
                m_Mode = value;
            }
        }
        public static Navigation defaultNavigation
        {
            get
            {
                return new Navigation()
                {
                    m_SelectOnUp = null,
                    m_SelectOnDown = null,
                    m_SelectOnLeft = null,
                    m_SelectOnRight = null,
                    m_Mode = Mode.Automatic
                };
            }
        }



        public enum Direction {
            SelectOnUp,
            SelectOnDown,
            SelectOnLeft,
            SelectOnRight
        }

        public enum Mode {
            None,
            Horizontal,
            Vertical,
            Automatic,
            Explicit
        }


        public override bool Equals(object obj) {
            if (!(obj is Navigation))
                return false;
            return this.Equals((Navigation)obj);
        }

        public bool Equals(Navigation other) {
            return this.mode == other.mode && this.m_SelectOnUp == other.selectOnUp && this.m_SelectOnDown == other.selectOnDown && this.m_SelectOnLeft == other.selectOnLeft && this.m_SelectOnRight == other.selectOnRight;
        }

        public static bool operator ==(Navigation point1, Navigation point2) {
            return point1.Equals(point2);
        }

        public static bool operator !=(Navigation point1, Navigation point2) {
            return !point1.Equals(point2);
        }

        public override int GetHashCode() {
            return base.GetHashCode();
        }
    }
}

