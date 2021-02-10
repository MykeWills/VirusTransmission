using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Game : MonoBehaviour {
    


    public static float gamePauseTime = 0;

    public static float gameRunTime = 0;

    public static bool paused = false;

    private static float startPauseTime = -1;

    public static void UpdateMe() {
        float currentTime = Time.time;
        if (!Game.paused) {
            if (startPauseTime != -1) {
                gamePauseTime += currentTime - startPauseTime;
                startPauseTime = -1;
            }

            gameRunTime = currentTime - gamePauseTime;
        }
        else {
            if (startPauseTime == -1) {
                startPauseTime = currentTime;
            }
        }
    }
}
