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
    public int[] rightKeys = { 13, 36, 41, 113, 169, 191, 211, 264, 282, 341, 355 };
    public int correctCount = 0;
    private float starSignAlpha = 1.0f;
    private bool solved = false;
    static List<GameObject> list = new List<GameObject>();


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
        StarSign.SetActive(true);
        StarSign.GetComponent<Renderer>().material.SetFloat("_Cutoff", starSignAlpha);
        starSignAlpha -= 0.002f;
        if (starSignAlpha < 0.085f) return true;
        return false;
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