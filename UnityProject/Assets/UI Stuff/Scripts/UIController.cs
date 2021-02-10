using UnityEngine;

namespace FaceHorn.UI {
    public class UIController : MonoBehaviour {

        public static UIController UIC;

        [SerializeField] private bool updateGame = false;

        [SerializeField] private string player1HorizontalAxis = "Player1MoveHorizontal";
        [SerializeField] private string player1VerticalAxis = "Player1MoveVertical";
        [SerializeField] private string player1SubmitButton = "Player1Submit";
        //[SerializeField] private string player1CancelButton = "Player 1 Cancel";

        [SerializeField] private string player2HorizontalAxis = "Player2MoveHorizontal";
        [SerializeField] private string player2VerticalAxis = "Player2MoveVertical";
        [SerializeField] private string player2SubmitButton = "Player2Submit";
        //[SerializeField] private string player2CancelButton = "Player 2 Cancel";

        [SerializeField] private Selectable player1FirstSelected;
        [SerializeField] private Selectable player2FirstSelected;

        [SerializeField] private float inputActionsPerSecond = 10;
        [SerializeField] private float repeatDelay = 0.5f;

        private Direction player1Direction; // Un Serialize
        private Direction player1OldDirection; // Un Serialize
        private Direction player2Direction; // Un Serialize
        private Direction player2OldDirection; // Un Serialize

        private float player1NextSelectTime = 0; // Un Serialize
        private float player2NextSelectTime = 0; // Un Serialize

        private Selectable player1CurrentSelected; // Un Serialize
        private Selectable player2CurrentSelected; // Un Serialize


        private Selectable eventSelected; // Un Serialize

        private bool player1SubmitButtonDown = false; // Un Serialize
        private bool player2SubmitButtonDown = false; // Un Serialize




        private void OnEnable() {
            
            player1CurrentSelected = player1FirstSelected;
            player2CurrentSelected = player2FirstSelected;

            if (player1CurrentSelected == player2CurrentSelected) {
                player1CurrentSelected.selectionState = Selectable.SelectionState.BothPlayersHighlighted;
            }
            else {
                player1CurrentSelected.selectionState = Selectable.SelectionState.Player1Highlighted;
                player2CurrentSelected.selectionState = Selectable.SelectionState.Player2Highlighted;
            }

            if (UIC == null) {
                UIC = this;
            }
            else {
                Destroy(gameObject);
            }
        }

        private void OnDisable() {
            if (player1CurrentSelected != null) {
                player1CurrentSelected.selectionState = Selectable.SelectionState.Normal;
            }
            if (player2CurrentSelected != null) {
                player2CurrentSelected.selectionState = Selectable.SelectionState.Normal;
            }

            if (UIC == this) {
                UIC = null;
            }
        }

        public void Revert() {

            if (player1CurrentSelected != null) {
                player1CurrentSelected.selectionState = Selectable.SelectionState.Normal;
            }
            if (player2CurrentSelected != null) {
                player2CurrentSelected.selectionState = Selectable.SelectionState.Normal;
            }

            if (UIC == this) {
                UIC = null;
            }
            
            player1CurrentSelected = player1FirstSelected;
            player2CurrentSelected = player2FirstSelected;

            if (player1CurrentSelected == player2CurrentSelected) {
                player1CurrentSelected.selectionState = Selectable.SelectionState.BothPlayersHighlighted;
            }
            else {
                player1CurrentSelected.selectionState = Selectable.SelectionState.Player1Highlighted;
                player2CurrentSelected.selectionState = Selectable.SelectionState.Player2Highlighted;
            }

            if (UIC == null) {
                UIC = this;
            }
            else {
                Destroy(gameObject);
            }

        }


        private void Update() {
            if (updateGame) {
                Game.UpdateMe();
            }
            TestPlayerInput(player1HorizontalAxis, player1VerticalAxis, player1SubmitButton, ref player1NextSelectTime, ref player1Direction, ref player1OldDirection, ref player1CurrentSelected, player2CurrentSelected, ref player1SubmitButtonDown, 1);
            TestPlayerInput(player2HorizontalAxis, player2VerticalAxis, player2SubmitButton, ref player2NextSelectTime, ref player2Direction, ref player2OldDirection, ref player2CurrentSelected, player1CurrentSelected, ref player2SubmitButtonDown, 2);
            
        }

        private void TestPlayerInput(string horizontal, string vertical, string submit, ref float nextSelectTime, ref Direction playerDir, ref Direction playerOldDir, ref Selectable currentSelectable, Selectable otherCurrentSelectable, ref bool submitButtonDown, int playerNum) {
            float h = Input.GetAxisRaw(horizontal);
            float v = Input.GetAxisRaw(vertical);

            bool delay = true;

            if ((h >= 0 && v > h) || (h <= 0 && v > -h)) {
                playerDir =  Direction.Up;
            }
            else if ((h <= 0 && v < h) || (h >= 0 && v < -h)) {
                playerDir = Direction.Down;
            }
            else if ((v >= 0 && h > v) || (v <= 0 && h > -v)) {
                playerDir = Direction.Right;
            }
            else if ((v <= 0 && h < v) || (v >= 0 && h < -v)) {
                playerDir = Direction.Left;
            }
            else {
                playerDir = Direction.None;
                delay = false;
            }

            FindSelectable(playerDir, nextSelectTime, ref currentSelectable, otherCurrentSelectable, h, v, playerNum);

            if (playerDir != playerOldDir || !delay) {
                nextSelectTime = -1;
            }

            if (delay) {
                if (nextSelectTime == -1) {
                    nextSelectTime = Game.gameRunTime + repeatDelay;
                }
                else if (nextSelectTime <= Game.gameRunTime) {
                    nextSelectTime += 1 / inputActionsPerSecond;
                }
            }
            playerOldDir = playerDir;

            if (Input.GetAxisRaw(submit) != 0) {
                if (!submitButtonDown) {
                    Submit(currentSelectable, playerNum);
                    submitButtonDown = true;
                }
            }
            else if (submitButtonDown) {
                submitButtonDown = false;
            }

        }

        private void FindSelectable(Direction direction, float endTime, ref Selectable currentSelectable, Selectable otherCurrentSelectable, float h, float v, int playerNum) {

            Vector3 dir = new Vector3(h, v, 0).normalized;
            

            if ((dir == Vector3.left || dir == Vector3.right) && currentSelectable as Slider != null && endTime <= Game.gameRunTime) {
                Slider thisSlider = currentSelectable as Slider;

                if ((dir == Vector3.left && thisSlider.value > thisSlider.minValue) || (dir == Vector3.right && thisSlider.value < thisSlider.maxValue)) {
                    thisSlider.value += thisSlider.changeByValue * dir.x;
                    thisSlider.UpdateSlider();
                    return;
                }
            }
            
            if (direction != Direction.None && endTime <= Game.gameRunTime && currentSelectable.FindSelectable(new Vector3(h, v, 0), playerNum) != null) {
                
                DeselectSelectable(ref currentSelectable, ref otherCurrentSelectable, playerNum);
                currentSelectable = currentSelectable.FindSelectable(new Vector3(h, v, 0), playerNum);
                SelectSelectable(ref currentSelectable, ref otherCurrentSelectable, playerNum);
            }
        }
        
        private void Submit(Selectable currentSelectable, int playerNum) {

            if (currentSelectable.GetComponent<Button>() != null) {
                currentSelectable.GetComponent<Button>().onClick.Invoke();
            }
            else if (currentSelectable.GetComponent<Dropdown>() != null) {
                currentSelectable.GetComponent<Dropdown>().Show(playerNum);
            }
            else if (currentSelectable.GetComponent<Toggle>() != null) {
                currentSelectable.GetComponent<Toggle>().Submit();
            }
        }
        
        public void P1Select(Selectable newSelectable) {
            DeselectSelectable(ref player1CurrentSelected, ref player2CurrentSelected, 1);
            player1CurrentSelected = newSelectable;
            SelectSelectable(ref player1CurrentSelected, ref player2CurrentSelected, 1);
        }

        public void P2Select(Selectable newSelectable) {
            DeselectSelectable(ref player2CurrentSelected, ref player1CurrentSelected, 2);
            player2CurrentSelected = newSelectable;
            SelectSelectable(ref player2CurrentSelected, ref player1CurrentSelected, 2);
        }

        private void SelectSelectable(ref Selectable thisPlayerCurrentSelectable , ref Selectable otherPlayerCurrentSelectable, int playerNum) {
            if (thisPlayerCurrentSelectable == otherPlayerCurrentSelectable) {
                thisPlayerCurrentSelectable.selectionState = Selectable.SelectionState.BothPlayersHighlighted;
            }
            else {
                if (playerNum == 1) {
                    thisPlayerCurrentSelectable.selectionState = Selectable.SelectionState.Player1Highlighted;
                }
                else {
                    thisPlayerCurrentSelectable.selectionState = Selectable.SelectionState.Player2Highlighted;
                }
            }
        }

        private void DeselectSelectable(ref Selectable thisPlayerCurrentSelectable, ref Selectable otherPlayerCurrentSelectable, int playerNum) {
            if (thisPlayerCurrentSelectable == otherPlayerCurrentSelectable) {
                if (playerNum == 1) {
                    thisPlayerCurrentSelectable.selectionState = Selectable.SelectionState.Player2Highlighted;
                }
                else {
                    thisPlayerCurrentSelectable.selectionState = Selectable.SelectionState.Player1Highlighted;
                }
            }
            else {
                thisPlayerCurrentSelectable.selectionState = Selectable.SelectionState.Normal;
            }
        }

        protected enum Direction {
            Up,
            Down,
            Left,
            Right,
            None
        }
    }
}
