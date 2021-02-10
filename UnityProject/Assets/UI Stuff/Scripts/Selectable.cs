using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace FaceHorn.UI {
    [AddComponentMenu("FaceHorn/UI/Selectable", 700)]
    [ExecuteInEditMode]
    [SelectionBase]
    [DisallowMultipleComponent]
    public class Selectable : UIBehaviour {
        
        [SerializeField]
        private PlayersAccessible m_PlayersAccessible;

        [SerializeField]
        private bool m_Interactable = true;

        [SerializeField]
        private Transition m_Transition = Transition.ColorTint;

        [SerializeField]
        private Graphic m_TargetGraphic;

        [SerializeField]
        private AnimationTriggers m_AnimationTriggers;

        [SerializeField]
        private SpriteState m_SpriteStates;

        [SerializeField]
        private ColorBlock m_Colors = ColorBlock.defaultColorBlock;

        [SerializeField]
        private Navigation m_Navigation = Navigation.defaultNavigation;

        private SelectionState m_selectionState;

        private static List<Selectable> s_P1List = new List<Selectable>();

        private static List<Selectable> s_P2List = new List<Selectable>();

        private static List<Selectable> s_ActiveList = new List<Selectable>();

        public static List<Selectable> p1Selectables {
            get {
                return Selectable.s_P1List;
            }
        }
        
        public static List<Selectable> p2Selectables {
            get {
                return Selectable.s_P2List;
            }
        }

        public static List<Selectable> ActiveSelectables {
            get {
                return Selectable.s_ActiveList;
            }
        }

        public SelectionState selectionState {
            get {
                return m_selectionState;
            }
            set {
                m_selectionState = value;
                ChangeTransition();
            }
        }

        public ColorBlock Colors {
            get {
                return m_Colors;
            }
            set {
                m_Colors = value;
            }
        }
        
        
        public Graphic TargetGraphic {
            get {
                return this.m_TargetGraphic;
            }
            set {
                m_TargetGraphic = value;
            }
        }

        public Image image {
            get {
                return this.m_TargetGraphic as Image;
            }
            set {
                this.m_TargetGraphic = (Graphic) value;
            }
        }

        public Selectable.Transition transition {
            get {
                return this.m_Transition;
            }
            set {
                this.m_Transition = value;
            }
        }

        public bool interactable {
            get {
                return m_Interactable;
            }
            set {
                m_Interactable = value;
                ChangeTransition();

            }
        }

        public AnimationTriggers animationTriggers {
            get {
                return m_AnimationTriggers;
            }
            set {
                m_AnimationTriggers = value;
            }
        }

        public SpriteState spriteState {
            get {
                return m_SpriteStates;
            }
            set {
                m_SpriteStates = value;
            }
        }
        
        public Navigation navigation {
            get {
                return this.m_Navigation;
            }
            set {

                m_Navigation = value;
                this.OnSetProperty();
            }
        }

        public Navigation.Mode navigationMode {
            get {
                return m_Navigation.mode;
            }
            set {
                m_Navigation.mode = value;
            }
        }

        public void SetNavigationExplicitSelectable(Navigation.Direction dir, Selectable selectable) {

            if (dir == Navigation.Direction.SelectOnUp) {
                m_Navigation.selectOnUp = selectable;
            }
            else if (dir == Navigation.Direction.SelectOnDown) {
                m_Navigation.selectOnDown = selectable;
            }
            else if (dir == Navigation.Direction.SelectOnLeft) {
                m_Navigation.selectOnLeft = selectable;
            }
            else if (dir == Navigation.Direction.SelectOnRight) {
                m_Navigation.selectOnRight = selectable;
            }

        }


        protected override void OnEnable()
        {
            if (m_PlayersAccessible == PlayersAccessible.Player1 || m_PlayersAccessible == PlayersAccessible.Both) {
                Selectable.s_P1List.Add(this);
            }
            if (m_PlayersAccessible == PlayersAccessible.Player2 || m_PlayersAccessible == PlayersAccessible.Both) {
                Selectable.s_P2List.Add(this);
            }
            Selectable.s_ActiveList.Add(this);
            
        }
        protected override void OnDisable() {
            if (m_PlayersAccessible == PlayersAccessible.Player1 || m_PlayersAccessible == PlayersAccessible.Both) {
                Selectable.s_P1List.Remove(this);
            }
            if (m_PlayersAccessible == PlayersAccessible.Player2 || m_PlayersAccessible == PlayersAccessible.Both) {
                Selectable.s_P2List.Remove(this);
            }
            Selectable.s_ActiveList.Remove(this);
        }

        
        public void P1Select() {
            UIController.UIC.P1Select(this);
        }

        public void P2Select() {
            UIController.UIC.P2Select(this);
        }



        public virtual bool IsInteractable() {
            return this.m_Interactable;
        }

#if UNITY_EDITOR
        protected override void OnValidate() {
            base.OnValidate();
            ChangeTransition();

        }
#endif




        private void ChangeTransition() {
            
            if (!m_Interactable) {
                m_selectionState = SelectionState.Disabled;
            }
            else if (m_selectionState == SelectionState.Disabled) {
                m_selectionState = SelectionState.Normal;
            }

            Color newColor = new Color();
            Sprite newSprite = new Sprite();
            string triggerName = "";

            switch (m_selectionState) {
                case SelectionState.Normal:
                    newColor = Colors.normalColor;
                    newSprite = spriteState.normalSprite;
                    triggerName = animationTriggers.normalTrigger;
                    break;
                case SelectionState.Player1Highlighted:
                    newColor = Colors.player1HighlightedColor;
                    newSprite = spriteState.player1HighlightedSprite;
                    triggerName = animationTriggers.player1HighlightedTrigger;
                    break;
                case SelectionState.Player2Highlighted:
                    newColor = Colors.player2HighlightedColor;
                    newSprite = spriteState.player2HighlightedSprite;
                    triggerName = animationTriggers.player2HighlightedTrigger;
                    break;
                case SelectionState.BothPlayersHighlighted:
                    newColor = Colors.bothPlayersHighlightedColor;
                    newSprite = spriteState.bothPlayersHighlightedSprite;
                    triggerName = animationTriggers.bothPlayersHighlightedTrigger;
                    break;
                case SelectionState.Pressed:
                    newColor = Colors.pressedColor;
                    newSprite = spriteState.pressedSprite;
                    triggerName = animationTriggers.pressedTrigger;
                    break;
                case SelectionState.Disabled:
                    newColor = Colors.disabledColor;
                    newSprite = spriteState.disabledSprite;
                    triggerName = animationTriggers.disabledTrigger;
                    break;
            }

            switch (m_Transition) {
                case Transition.ColorTint:
                    m_TargetGraphic.color = newColor;
                    break;
                case Transition.SpriteSwap:
                    this.image.overrideSprite = newSprite;
                    break;
                case Transition.Animation:
                    
                    break;
                case Transition.None:
                    break;

            }


        }



        public Selectable FindSelectable(Vector3 dir, int playerNum) {
            
            Selectable selectable1 = (Selectable)null;

            if (navigation.mode == Navigation.Mode.Explicit) {
                dir = dir.normalized;
                if (dir == Vector3.up) {
                    selectable1 = navigation.selectOnUp;
                }
                else if (dir == Vector3.down) {
                    selectable1 = navigation.selectOnDown;
                }
                else if (dir == Vector3.right) {
                    selectable1 = navigation.selectOnRight;
                }
                else if (dir == Vector3.left) {
                    selectable1 = navigation.selectOnLeft;
                }
            }


            else { 

                if (navigation.mode == Navigation.Mode.Horizontal) {
                    dir = new Vector3(dir.x, 0, 0);
                }
                else if (navigation.mode == Navigation.Mode.Vertical) {
                    dir = new Vector3(0, dir.y, 0);
                }
                dir = dir.normalized;
                Vector3 vector3 = this.transform.TransformPoint(Selectable.GetPointOnRectEdge(this.transform as RectTransform, (Vector2)(Quaternion.Inverse(this.transform.rotation) * dir)));
                float num1 = float.NegativeInfinity;

                for (int index = 0; ((index < Selectable.s_P1List.Count && playerNum == 1) || (index < Selectable.s_P2List.Count && playerNum == 2)); ++index) {
                    Selectable selectable2;
                    if (playerNum == 1) {
                        selectable2 = Selectable.s_P1List[index];
                    }
                    else {
                        selectable2 = Selectable.s_P2List[index];
                    }

                    if (!((Object)selectable2 == (Object)this) && !((Object)selectable2 == (Object)null) && selectable2.IsInteractable() && selectable2.navigation.mode != Navigation.Mode.None) {
                        RectTransform transform = selectable2.transform as RectTransform;
                        Vector3 position = !((Object)transform != (Object)null) ? Vector3.zero : (Vector3)transform.rect.center;
                        Vector3 rhs = selectable2.transform.TransformPoint(position) - vector3;
                        float num2 = Vector3.Dot(dir, rhs);
                        if ((double)num2 > 0.0) {
                            float num3 = num2 / rhs.sqrMagnitude;
                            if ((double)num3 > (double)num1) {
                                num1 = num3;
                                selectable1 = selectable2;

                            }
                        }
                    }
                }
            } 
            return selectable1;
        }




        private static Vector3 GetPointOnRectEdge(RectTransform rect, Vector2 dir)
        {
            if ((Object)rect == (Object)null)
                return Vector3.zero;
            if (dir != Vector2.zero)
                dir /= Mathf.Max(Mathf.Abs(dir.x), Mathf.Abs(dir.y));
            dir = rect.rect.center + Vector2.Scale(rect.rect.size, dir * 0.5f);
            return (Vector3)dir;
        }






        protected override void Awake()
        {
            if (!((Object)this.m_TargetGraphic == (Object)null))
                return;
            this.m_TargetGraphic = this.GetComponent<Graphic>();
        }



        private void OnSetProperty() {

        }






        protected enum PlayersAccessible {
            Player1,
            Player2,
            Both,
            None
        }

        public enum Transition {
            None,
            ColorTint,
            SpriteSwap,
            Animation,
        }

        public enum SelectionState {
            Normal,
            Player1Highlighted,
            Player2Highlighted,
            BothPlayersHighlighted,
            Pressed,
            Disabled,
        }

    }
}
