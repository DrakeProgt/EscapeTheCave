using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameManager {

    public static GameObject focused;
    public static bool pressedInteractKey = false;

    public static bool isWordPuzzleSolved = false;

    public static void UnpressAllKeys()
    {
        pressedInteractKey = false;
    }
}
