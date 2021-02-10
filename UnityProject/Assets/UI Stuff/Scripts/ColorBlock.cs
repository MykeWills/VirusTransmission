using System;
using UnityEngine;
using UnityEngine.Serialization;


namespace FaceHorn.UI {

    [Serializable]
    public struct ColorBlock : IEquatable<ColorBlock> {

        [SerializeField] private Color m_NormalColor;
        [SerializeField] private Color m_Player1HighlightedColor;
        [SerializeField] private Color m_Player2HighlightedColor;
        [SerializeField] private Color m_BothPlayersHighlightedColor;
        [SerializeField] private Color m_PressedColor;
        [SerializeField] private Color m_DisabledColor;
        [Range(1f, 5f)] [SerializeField] private float m_ColorMultiplier;
        [SerializeField] private float m_FadeDuration;


        public Color normalColor
        {
            get
            {
                return this.m_NormalColor;
            }
            set
            {
                this.m_NormalColor = value;
            }
        }
        
        public Color player1HighlightedColor
        {
            get
            {
                return this.m_Player1HighlightedColor;
            }
            set
            {
                this.m_Player1HighlightedColor = value;
            }
        }
        public Color player2HighlightedColor
        {
            get
            {
                return this.m_Player2HighlightedColor;
            }
            set
            {
                this.m_Player2HighlightedColor = value;
            }
        }
        public Color bothPlayersHighlightedColor
        {
            get
            {
                return this.m_BothPlayersHighlightedColor;
            }
            set
            {
                this.m_BothPlayersHighlightedColor = value;
            }
        }
        
        public Color pressedColor
        {
            get
            {
                return this.m_PressedColor;
            }
            set
            {
                this.m_PressedColor = value;
            }
        }
        
        public Color disabledColor
        {
            get
            {
                return this.m_DisabledColor;
            }
            set
            {
                this.m_DisabledColor = value;
            }
        }
        
        public float colorMultiplier
        {
            get
            {
                return this.m_ColorMultiplier;
            }
            set
            {
                this.m_ColorMultiplier = value;
            }
        }

        public float fadeDuration
        {
            get
            {
                return this.m_FadeDuration;
            }
            set
            {
                this.m_FadeDuration = value;
            }
        }

        public static ColorBlock defaultColorBlock
        {
            get
            {
                return new ColorBlock()
                {
                    m_NormalColor = (Color)new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue),
                    m_Player1HighlightedColor = (Color)new Color32((byte)200, (byte)200, (byte)0, byte.MaxValue),
                    m_Player2HighlightedColor = (Color)new Color32((byte)200, (byte)200, (byte)0, byte.MaxValue),
                    m_BothPlayersHighlightedColor = (Color)new Color32((byte)50, (byte)50, (byte)0, byte.MaxValue),
                    m_PressedColor = (Color)new Color32((byte)100, (byte)100, (byte)100, byte.MaxValue),
                    m_DisabledColor = (Color)new Color32((byte)200, (byte)200, (byte)200, (byte)128),
                    colorMultiplier = 1f,
                    fadeDuration = 0.1f
                };
            }
        }

        public override bool Equals(object obj)
        {
            if (!(obj is ColorBlock))
                return false;
            return this.Equals((ColorBlock)obj);
        }

        public bool Equals(ColorBlock other)
        {
            return this.normalColor == other.normalColor && this.player1HighlightedColor == other.player1HighlightedColor && this.player2HighlightedColor == other.player2HighlightedColor && this.bothPlayersHighlightedColor == other.bothPlayersHighlightedColor && (this.pressedColor == other.pressedColor && this.disabledColor == other.disabledColor) && (double)this.colorMultiplier == (double)other.colorMultiplier && (double)this.fadeDuration == (double)other.fadeDuration;
        }

        public static bool operator ==(ColorBlock point1, ColorBlock point2)
        {
            return point1.Equals(point2);
        }

        public static bool operator !=(ColorBlock point1, ColorBlock point2)
        {
            return !point1.Equals(point2);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
