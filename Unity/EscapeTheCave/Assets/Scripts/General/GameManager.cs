using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public static class GameManager
{

    public static GameObject focused;
    public static GameObject Player;
    public static bool secondCaveReached = false;
    public static bool pressedInteractKey = false;
    public static bool pressedL1Key = false;
    public static bool pressedR1Key = false;
    public static bool pressedL2Key = false;
    public static bool pressedR2Key = false;
    public static string hoverMessage = "This is the maximum length of the message; This is the maximum length of the message; This is the maximum length of the message; This is the maximum length of the message";

    [SerializeField] public static bool isWordPuzzleSolved = false;
    public static bool isLightPuzzleSolved = false;

    public static bool isGamePaused = false;

	public static Vector3 monsterPosition;

    public static void UnpressAllKeys()
    {
        pressedInteractKey = false;
        pressedL1Key = false;
        pressedR1Key = false;
        pressedL2Key = false;
        pressedR2Key = false;
    }

    public static bool IsGamepadConnected
    {
        get
        {
            bool isConnected = Input.GetJoystickNames().Length > 0;
            if (isConnected)
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            return isConnected;
        }
    }

    public static void resetSecondCave()
    {
        Player.transform.position = new Vector3(-24.1f, -0.56f, 1.32f);
        Player.transform.eulerAngles = new Vector3(0.0f, 253.74f, 0.0f);
        Player.GetComponent<FirstPersonController>().resetRotation = true;
        JumpStone.reset = true;
    }

    public static void die()
    {
        resetSecondCave();
    }
}
