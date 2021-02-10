using System;
using UnityEngine;

namespace FaceHorn.UI {

    [Serializable]
    public struct SpriteState : IEquatable<SpriteState> {
        [SerializeField]
        private Sprite m_NormalSprite;
        [SerializeField]
        private Sprite m_Player1HighlightedSprite;
        [SerializeField]
        private Sprite m_Player2HighlightedSprite;
        [SerializeField]
        private Sprite m_BothPlayersHighlightedSprite;
        [SerializeField]
        private Sprite m_PressedSprite;
        [SerializeField]
        private Sprite m_DisabledSprite;

        public Sprite normalSprite {
            get {
                return m_NormalSprite;
            }
            set {
                m_NormalSprite = value;
            }
        }
        
        public Sprite player1HighlightedSprite {
            get {
                return this.m_Player1HighlightedSprite;
            }
            set {
                this.m_Player1HighlightedSprite = value;
            }
        }

        public Sprite player2HighlightedSprite {
            get {
                return this.m_Player2HighlightedSprite;
            }
            set {
                this.m_Player2HighlightedSprite = value;
            }
        }

        public Sprite bothPlayersHighlightedSprite {
            get {
                return this.m_BothPlayersHighlightedSprite;
            }
            set {
                this.m_BothPlayersHighlightedSprite = value;
            }
        }


        public Sprite pressedSprite {
            get {
                return this.m_PressedSprite;
            }
            set {
                this.m_PressedSprite = value;
            }
        }


        public Sprite disabledSprite {
            get {
                return this.m_DisabledSprite;
            }
            set {
                this.m_DisabledSprite = value;
            }
        }

        public bool Equals(SpriteState other) {
            return this.m_NormalSprite == other.normalSprite && this.m_Player1HighlightedSprite == other.player1HighlightedSprite && this.m_Player2HighlightedSprite == other.player2HighlightedSprite && this.m_BothPlayersHighlightedSprite == other.bothPlayersHighlightedSprite && this.m_PressedSprite == other.pressedSprite && this.m_DisabledSprite == other.disabledSprite;
        }
    }
}
