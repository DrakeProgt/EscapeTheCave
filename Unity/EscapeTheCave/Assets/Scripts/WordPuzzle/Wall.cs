using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour {

    public GameObject CubePrefab;
    public GameObject EmptyCubePrefab;
    public GameObject WallController;
    public GameObject StarLightPrefab;
    public GameObject StarSign;
    public string text = "Test es testet Test testet set test teste";
    public int lineLength = 5;
    public int[] rightKeys = { 14, 36, 41, 110, 164, 184, 204, 255, 272, 329, 342 };
    public int correctCount = 0;
    private bool solved = false;
    static List<GameObject> list = new List<GameObject>();
    private StarSignLineSystem starSignScript;


    void Start () {
        GameObject firstCube = Instantiate(CubePrefab, WallController.GetComponent<Transform>());
        list.Add(firstCube);

        for (int index = 1; index < text.Length; index++)
        {
            GameObject newCube = null;
            if (' ' == text[index])
            {
                newCube = Instantiate(EmptyCubePrefab, WallController.GetComponent<Transform>());
            } else
            {
                newCube = Instantiate(CubePrefab, WallController.GetComponent<Transform>());
            }
            
            list.Add(newCube);
            newCube.GetComponent<Cube>().Setup(firstCube, this, index, isIndexCorrect(index + 1), text[index], (int)index / lineLength, index % lineLength);   
        }
        firstCube.GetComponent<Cube>().Setup(firstCube, this, 0, isIndexCorrect(1), text[0], 0, 0);

        starSignScript = StarSign.GetComponent<StarSignLineSystem>();
    }

    private void Update()
    {
        if (!GameManager.isWordPuzzleSolved)
        {
            if (!solved)
            {
                if (null != GameManager.focused && GameManager.pressedInteractKey && "pressable" == GameManager.focused.tag && ("WordPuzzleButton" == GameManager.focused.name || "WordPuzzleButton(Clone)" == GameManager.focused.name))
                {
                    GameManager.focused.GetComponent<Cube>().Press();
                }
                solved = isFinished();
            } else
            {
                GameManager.isWordPuzzleSolved = isEndAnimationFinished();
            }
        }
        
    }

    private bool isEndAnimationFinished()
    {
        return starSignScript.buildUp();
    }

    private bool isFinished()
    {
        if (correctCount == rightKeys.Length)
        {
            foreach (GameObject cube in list)
            {
                cube.GetComponent<Cube>().Finish();
            }
            Debug.Log("WordPuzzleSolved");
            return true;
        }
        return false;

    }

    public void PressedKey(bool correctKey)
    {
        if (correctKey)
        {
            correctCount++;
        } else
        {
            correctCount = 0;
            foreach (GameObject cube in list)
            {
                cube.GetComponent<Cube>().Unpress();
            }
        }
    }

    bool isIndexCorrect(int index)
    {
        if (-1 != Array.IndexOf(rightKeys, index))
        {
            return true;
        }
        return false;
    }
    
}