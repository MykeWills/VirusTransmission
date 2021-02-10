using FaceHorn.UI;
using UnityEditor;
using UnityEditor.AnimatedValues;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Events;

namespace FaceHornEditor.UI {

    [CustomEditor(typeof(FaceHorn.UI.Selectable), true)]
    [CanEditMultipleObjects]
    public class SelectableEditor : Editor {

        private SerializedProperty m_PlayersAccessible;
        private SerializedProperty m_Interactable;
        private SerializedProperty m_Transition;
        private SerializedProperty m_TargetGraphic;

        // If Transition is on Color Tint

        private SerializedProperty m_ColorBlockNormalColor;
        private SerializedProperty m_ColorBlockPlayer1HighlightedColor;
        private SerializedProperty m_ColorBlockPlayer2HighlightedColor;
        private SerializedProperty m_ColorBlockBothPlayersHighlightedColor;
        private SerializedProperty m_ColorBlockPressedColor;
        private SerializedProperty m_ColorBlockDisabledColor;
        private SerializedProperty m_ColorBlockColorMultiplier;
        private SerializedProperty m_ColorBlockFadeDuration;

        // If Transition is on Sprite Swap
        private SerializedProperty m_NormalSprite;
        private SerializedProperty m_Player1HighlightedSprite;
        private SerializedProperty m_Player2HighlightedSprite;
        private SerializedProperty m_BothPlayersHighlightedSprite;
        private SerializedProperty m_PressedSprite;
        private SerializedProperty m_DisabledSprite;

        // If Transition is on Animation

        private SerializedProperty m_AnimTriggerNormal;
        private SerializedProperty m_AnimTriggerPlayer1Highlighted;
        private SerializedProperty m_AnimTriggerPlayer2Highlighted;
        private SerializedProperty m_AnimTriggerBothPlayersHighlighted;
        private SerializedProperty m_AnimTriggerPressed;
        private SerializedProperty m_AnimTriggerDisabled;

        // Navigation Stuff

        private SerializedProperty m_NavigationModeSelect;
        private SerializedProperty m_NavigationSelectOnUp;
        private SerializedProperty m_NavigationSelectOnDown;
        private SerializedProperty m_NavigationSelectOnLeft;
        private SerializedProperty m_NavigationSelectOnRight;
        

        protected virtual void OnEnable() {
            

            m_PlayersAccessible = base.serializedObject.FindProperty("m_PlayersAccessible");


            m_Interactable = base.serializedObject.FindProperty("m_Interactable");
            m_Transition = base.serializedObject.FindProperty("m_Transition");
            m_TargetGraphic = base.serializedObject.FindProperty("m_TargetGraphic");

            m_ColorBlockNormalColor = base.serializedObject.FindProperty("m_Colors.m_NormalColor");
            m_ColorBlockPlayer1HighlightedColor = base.serializedObject.FindProperty("m_Colors.m_Player1HighlightedColor");
            m_ColorBlockPlayer2HighlightedColor = base.serializedObject.FindProperty("m_Colors.m_Player2HighlightedColor");
            m_ColorBlockBothPlayersHighlightedColor = base.serializedObject.FindProperty("m_Colors.m_BothPlayersHighlightedColor");
            m_ColorBlockPressedColor = base.serializedObject.FindProperty("m_Colors.m_PressedColor");
            m_ColorBlockDisabledColor = base.serializedObject.FindProperty("m_Colors.m_DisabledColor");
            m_ColorBlockColorMultiplier = base.serializedObject.FindProperty("m_Colors.m_ColorMultiplier");
            m_ColorBlockFadeDuration = base.serializedObject.FindProperty("m_Colors.m_FadeDuration");

            m_NormalSprite = base.serializedObject.FindProperty("m_SpriteStates.m_NormalSprite");
            m_Player1HighlightedSprite = base.serializedObject.FindProperty("m_SpriteStates.m_Player1HighlightedSprite");
            m_Player2HighlightedSprite = base.serializedObject.FindProperty("m_SpriteStates.m_Player2HighlightedSprite");
            m_BothPlayersHighlightedSprite = base.serializedObject.FindProperty("m_SpriteStates.m_BothPlayersHighlightedSprite");
            m_PressedSprite = base.serializedObject.FindProperty("m_SpriteStates.m_PressedSprite");
            m_DisabledSprite = base.serializedObject.FindProperty("m_SpriteStates.m_DisabledSprite");

            m_AnimTriggerNormal = base.serializedObject.FindProperty("m_AnimationTriggers.m_NormalTrigger");
            m_AnimTriggerPlayer1Highlighted = base.serializedObject.FindProperty("m_AnimationTriggers.m_Player1HighlightedTrigger");
            m_AnimTriggerPlayer2Highlighted = base.serializedObject.FindProperty("m_AnimationTriggers.m_Player2HighlightedTrigger");
            m_AnimTriggerBothPlayersHighlighted = base.serializedObject.FindProperty("m_AnimationTriggers.m_BothPlayersHighlightedTrigger");
            m_AnimTriggerPressed = base.serializedObject.FindProperty("m_AnimationTriggers.m_PressedTrigger");
            m_AnimTriggerDisabled = base.serializedObject.FindProperty("m_AnimationTriggers.m_DisabledTrigger");
            
            m_NavigationModeSelect = base.serializedObject.FindProperty("m_Navigation.m_Mode");
            m_NavigationSelectOnUp = base.serializedObject.FindProperty("m_Navigation.m_SelectOnUp");
            m_NavigationSelectOnDown = base.serializedObject.FindProperty("m_Navigation.m_SelectOnDown");
            m_NavigationSelectOnLeft = base.serializedObject.FindProperty("m_Navigation.m_SelectOnLeft");
            m_NavigationSelectOnRight = base.serializedObject.FindProperty("m_Navigation.m_SelectOnRight");


            
    }

        public override void OnInspectorGUI() {

            serializedObject.Update();
            
            Animator behavior = (target as Selectable).GetComponent<Animator>();

            EditorGUILayout.PropertyField(m_PlayersAccessible);
            EditorGUILayout.PropertyField(m_Interactable);
            EditorGUILayout.PropertyField(m_Transition);
            

            if (m_Transition.enumValueIndex == Selectable.Transition.ColorTint.GetHashCode() || m_Transition.enumValueIndex == Selectable.Transition.SpriteSwap.GetHashCode()) {
                EditorGUILayout.PropertyField(m_TargetGraphic);
            }

            if (m_Transition.enumValueIndex == Selectable.Transition.ColorTint.GetHashCode()) {
                EditorGUILayout.PropertyField(m_ColorBlockNormalColor);
                EditorGUILayout.PropertyField(m_ColorBlockPlayer1HighlightedColor);
                EditorGUILayout.PropertyField(m_ColorBlockPlayer2HighlightedColor);
                EditorGUILayout.PropertyField(m_ColorBlockBothPlayersHighlightedColor);
                EditorGUILayout.PropertyField(m_ColorBlockPressedColor);
                EditorGUILayout.PropertyField(m_ColorBlockDisabledColor);
                EditorGUILayout.PropertyField(m_ColorBlockColorMultiplier);
                EditorGUILayout.PropertyField(m_ColorBlockFadeDuration);
            }
            else if (m_Transition.enumValueIndex == Selectable.Transition.SpriteSwap.GetHashCode()) {
                EditorGUILayout.PropertyField(m_NormalSprite);
                EditorGUILayout.PropertyField(m_Player1HighlightedSprite);
                EditorGUILayout.PropertyField(m_Player2HighlightedSprite);
                EditorGUILayout.PropertyField(m_BothPlayersHighlightedSprite);
                EditorGUILayout.PropertyField(m_PressedSprite);
                EditorGUILayout.PropertyField(m_DisabledSprite);
            }
            else if (m_Transition.enumValueIndex == Selectable.Transition.Animation.GetHashCode()) {

                EditorGUILayout.PropertyField(m_AnimTriggerNormal);
                EditorGUILayout.PropertyField(m_AnimTriggerPlayer1Highlighted);
                EditorGUILayout.PropertyField(m_AnimTriggerPlayer2Highlighted);
                EditorGUILayout.PropertyField(m_AnimTriggerBothPlayersHighlighted);
                EditorGUILayout.PropertyField(m_AnimTriggerPressed);
                EditorGUILayout.PropertyField(m_AnimTriggerDisabled);


                if ((Object)behavior == (Object) null || (Object)behavior.runtimeAnimatorController == (Object)null) {

                    Rect controlRect = EditorGUILayout.GetControlRect();
                    controlRect.xMin += EditorGUIUtility.labelWidth;

                    if (GUI.Button(controlRect, "AutoGererate Animation", EditorStyles.miniButton)) {
                        AnimatorController animatorController = SelectableEditor.GenerateSelectableAnimatorController((this.target as Selectable).animationTriggers, this.target as Selectable);

                        if ((Object)animatorController != (Object)null) {
                            if ((Object)behavior == (Object)null) {
                                behavior = (this.target as Selectable).gameObject.AddComponent<Animator>();
                            }
                            AnimatorController.SetAnimatorController(behavior, animatorController);
                        }
                    }
                }
            }


            EditorGUILayout.PropertyField(m_NavigationModeSelect);
            if (m_NavigationModeSelect.enumValueIndex == Navigation.Mode.Explicit.GetHashCode()) {
                EditorGUILayout.PropertyField(m_NavigationSelectOnUp);
                EditorGUILayout.PropertyField(m_NavigationSelectOnDown);
                EditorGUILayout.PropertyField(m_NavigationSelectOnLeft);
                EditorGUILayout.PropertyField(m_NavigationSelectOnRight);
            }

            


            this.serializedObject.ApplyModifiedProperties();
        }


        private static AnimatorController GenerateSelectableAnimatorController(AnimationTriggers animationTriggers, Selectable target) {
            if ((Object)target == (Object)null) {
                return (AnimatorController)null;
            }
            string saveControllerPath = SelectableEditor.GetSaveControllerPath(target);
            if (string.IsNullOrEmpty(saveControllerPath)) {
                return (AnimatorController)null;
            }
            string name1 = !string.IsNullOrEmpty(animationTriggers.normalTrigger) ? animationTriggers.normalTrigger : "Normal";
            string name2 = !string.IsNullOrEmpty(animationTriggers.player1HighlightedTrigger) ? animationTriggers.player1HighlightedTrigger : "Player1Highlighted";
            string name3 = !string.IsNullOrEmpty(animationTriggers.player2HighlightedTrigger) ? animationTriggers.player2HighlightedTrigger : "Player2Highlighted";
            string name4 = !string.IsNullOrEmpty(animationTriggers.bothPlayersHighlightedTrigger) ? animationTriggers.bothPlayersHighlightedTrigger : "BothPlayersHighlighted";
            string name5 = !string.IsNullOrEmpty(animationTriggers.pressedTrigger) ? animationTriggers.pressedTrigger : "Pressed";
            string name6 = !string.IsNullOrEmpty(animationTriggers.disabledTrigger) ? animationTriggers.disabledTrigger : "Disabled";

            AnimatorController controllerAtPath = AnimatorController.CreateAnimatorControllerAtPath(saveControllerPath);

            SelectableEditor.GenerateTriggerableTransition(name1, controllerAtPath);
            SelectableEditor.GenerateTriggerableTransition(name2, controllerAtPath);
            SelectableEditor.GenerateTriggerableTransition(name3, controllerAtPath);
            SelectableEditor.GenerateTriggerableTransition(name4, controllerAtPath);
            SelectableEditor.GenerateTriggerableTransition(name5, controllerAtPath);
            SelectableEditor.GenerateTriggerableTransition(name6, controllerAtPath);
            AssetDatabase.ImportAsset(saveControllerPath);
            return controllerAtPath;
        }

        private static string GetSaveControllerPath(Selectable target) {
            string name = target.gameObject.name;
            string message = string.Format("Create a new animator for the game object '{0}':", (object)name);
            return EditorUtility.SaveFilePanelInProject("New Animation Contoller", name, "controller", message);

        }
        private static AnimationClip GenerateTriggerableTransition(string name, AnimatorController controller) {
            AnimationClip animationClip = AnimatorController.AllocateAnimatorClip(name);
            AssetDatabase.AddObjectToAsset((Object)animationClip, (Object)controller);
            AnimatorState destinationState = controller.AddMotion((Motion)animationClip);
            controller.AddParameter(name, AnimatorControllerParameterType.Trigger);
            controller.layers[0].stateMachine.AddAnyStateTransition(destinationState).AddCondition(AnimatorConditionMode.If, 0.0f, name);
            return animationClip;
        }

    }
}