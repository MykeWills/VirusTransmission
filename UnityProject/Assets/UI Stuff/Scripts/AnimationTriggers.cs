using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace FaceHorn.UI {

    [Serializable]
    public class AnimationTriggers {

        private const string kDefaultNormalAnimName = "Normal";
        private const string kDefaultP1SelectedAnimName = "Player1Highlighted";
        private const string kDefaultP2SelectedAnimName = "Player2Highlighted";
        private const string kDefaultBPSelectedAnimName = "BothPlayersHighlighted";
        private const string kDefaultPressedAnimName = "Pressed";
        private const string kDefaultDisabledAnimName = "Disabled";

        [SerializeField]
        private string m_NormalTrigger;
        [SerializeField]
        private string m_Player1HighlightedTrigger;
        [SerializeField]
        private string m_Player2HighlightedTrigger;
        [SerializeField]
        private string m_BothPlayersHighlightedTrigger;
        [SerializeField]
        private string m_PressedTrigger;
        [SerializeField]
        private string m_DisabledTrigger;

        public AnimationTriggers() {
            this.m_NormalTrigger = "Normal";
            this.m_Player1HighlightedTrigger = "Player1Highlighted";
            this.m_Player2HighlightedTrigger = "Player2Highlighted";
            this.m_BothPlayersHighlightedTrigger = "BothPlayersHighlighted";
            this.m_PressedTrigger = "Pressed";
            this.m_DisabledTrigger = "Disabled";
        }


        public string normalTrigger {
            get {
                return this.m_NormalTrigger;
            }
            set {
                this.m_NormalTrigger = value;
            }
        }


        public string player1HighlightedTrigger {
            get {
                return this.m_Player1HighlightedTrigger;
            }
            set {
                this.m_Player1HighlightedTrigger = value;
            }
        }

        public string player2HighlightedTrigger {
            get {
                return this.m_Player2HighlightedTrigger;
            }
            set {
                this.m_Player2HighlightedTrigger = value;
            }
        }
        public string bothPlayersHighlightedTrigger {
            get {
                return this.m_BothPlayersHighlightedTrigger;
            }
            set {
                this.m_BothPlayersHighlightedTrigger = value;
            }
        }


        public string pressedTrigger {
            get {
                return this.m_PressedTrigger;
            }
            set {
                this.m_PressedTrigger = value;
            }
        }


        public string disabledTrigger {
            get {
                return this.m_DisabledTrigger;
            }
            set {
                this.m_DisabledTrigger = value;
            }
        }
    }
}
