using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class ScoreManager : MonoBehaviour {

    public static ScoreManager SM;

    public Text player1ScoreText;
    public Text player2ScoreText;

    public Text player1WinText;
    public Text player2WinText;

    public int winningScore = 3;

    [NonSerialized] public int player1Score = 0;
    [NonSerialized] public int player2Score = 0;


    private float resetTime = -1;
    private bool reset = false;

    private void Start() {
        SM = this;
    }

    private void Update() {
        if (reset && resetTime <= Time.timeSinceLevelLoad) {
            Resetplayer1();
            Resetplayer2();
            player1WinText.text = "";
            player2WinText.text = "";
            reset = false;
        }
    }

    public void Player1Dead() {

        player2Score++;

        if (player2Score >= winningScore) {
            player1Score = 0;
            player2Score = 0;

            resetTime = Time.timeSinceLevelLoad + 5;
            reset = true;

            player1WinText.text = "loser";
            player2WinText.text = "winner";
        }
        else {
            P1FirstPersonController.P1FPC.ResetPlayer();
        }



        player2ScoreText.text = "player 2 score: " + player2Score;
    }

    public void Player2Dead() {
        player1Score++;

        if (player1Score >= winningScore) {
            player1Score = 0;
            player2Score = 0;

            resetTime = Time.timeSinceLevelLoad + 5;
            reset = true;
            player2WinText.text = "loser";
            player1WinText.text = "winner";
        }
        else {
            P2FirstPersonController.P2FPC.ResetPlayer();
        }

        player1ScoreText.text = "player 1 score: " + player1Score;
    }

    private void Resetplayer1() {
        P1FirstPersonController.P1FPC.ResetPlayer();
    }
    private void Resetplayer2() {
        P2FirstPersonController.P2FPC.ResetPlayer();
    }
}
