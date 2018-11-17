using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour {

    public GameObject firstBrick;
    public GameObject WallController;
    public string text = "Test es testet Test testet set test teste";
    public int lineLength = 5;
    public int[] rightKeys = {1, 5};
    public int correctCount = 0;
    static List<GameObject> list = new List<GameObject>();


    void Start () {
        list.Add(firstBrick);

        for (int index = 1; index < text.Length; index++)
        {           
            GameObject newCube = Instantiate(firstBrick, WallController.GetComponent<Transform>());
            list.Add(newCube);
            newCube.GetComponent<Cube>().setup(firstBrick, this, indexCorrect(index + 1), text[index], (int)index / lineLength, index % lineLength);   
        }
        firstBrick.GetComponent<Cube>().setup(firstBrick, this, indexCorrect(1), text[0], 0, 0);
    }

    private void Update()
    {
        if (null != Interact.focused && Interact.pressedE && "pressable" == Interact.focused.tag && ("WordPuzzleButton" == Interact.focused.name || "WordPuzzleButton(Clone)" == Interact.focused.name))
        {
            Interact.focused.GetComponent<Cube>().press();
        }
        finished();
    }

    private bool finished()
    {
        if (correctCount == rightKeys.Length)
        {
            foreach (GameObject cube in list)
            {
                cube.GetComponent<Cube>().finish();
            }
            Debug.Log("WordPuzzleSolved");
            return true;
        }
        return false;

    }

    public void pressedKey(bool correctKey)
    {
        if (correctKey)
        {
            correctCount++;
        } else
        {
            correctCount = 0;
            foreach (GameObject cube in list)
            {
                cube.GetComponent<Cube>().unpress();
            }
        }
    }

    bool indexCorrect(int index)
    {
        if (-1 != Array.IndexOf(rightKeys, index))
        {
            return true;
        }
        return false;
    }
    
}