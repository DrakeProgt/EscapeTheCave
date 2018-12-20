using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameManager
{

    public static GameObject focused;
    public static bool pressedInteractKey = false;

    [SerializeField] public static bool isWordPuzzleSolved = false;
    public static bool isLightPuzzleSolved = false;

    public static bool isGamePaused = false;

	public static Vector3 monsterPosition;
	public static Vector3 playerPosition;

	public static void UnpressAllKeys()
    {
        pressedInteractKey = false;
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
}
