using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Countdown for how long the player can jump higher than normal
/// </summary>
public class JumpCountdown : MonoBehaviour {

    private int countdownJumpDuration;
    private float countdownJumpStartTime;
    /// <summary>
    /// To begin the timer for higher jumping
    /// </summary>
    /// <param name="seconds">an integer of the number of seconds allowed to jump high</param>
    public void SetTimer(int seconds)
    {
        countdownJumpStartTime = Time.time;
        countdownJumpDuration = seconds;
    }

    /// <summary>
    /// to return the number of seconds remaining to be able to update the image used as a "timer"
    /// </summary>
    /// <returns>seconds left for jumping high</returns>
    public int GetSecondsRemaining()
    {
        int elapsedSeconds = (int)(Time.time - countdownJumpStartTime);
        int secondsLeft = (countdownJumpDuration - elapsedSeconds);
        return secondsLeft;
    }
}
