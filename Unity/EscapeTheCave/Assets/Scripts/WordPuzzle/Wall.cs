using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour {

    public GameObject firstBrick;
    public string text = "Test es testet Test testet set test teste";
    public int lineLength = 5;

    void Start () {
        WallManager.firstBrick = firstBrick;
        WallManager.text = text;
        WallManager.lineLength = lineLength;
        WallManager.size = firstBrick.GetComponent<MeshFilter>().mesh.bounds.size;
        
        WallManager.setup();


        //for (int index = 0; index < 2000; index++)
        //{
          //  Debug.Log();
        //}
	}
	
    public static class WallManager
    {
        public static GameObject firstBrick;
        public static string text;
        public static int lineLength;
        static List<Cube> list = new List<Cube>();

        public static Vector3 size;

        public static void setup()
        {
            list.Add(new Cube(firstBrick, text[0]));
    

            for (int index = 1; index < text.Length; index++)
            {
                list.Add(
                    new Cube(firstBrick, text[index], (int)index / lineLength, index % lineLength)
                );
            }
        }

    }

    private class Cube
    {
        private GameObject cubeObject;
        private Vector3 prefabPosition;
        private float prefabRotation;
        private Vector3 originPosition;

        public static Texture2D loadTextFor(char letter)
        {
            if (char.IsUpper(letter))
            {
                return Resources.Load("WordPuzzle/letters/cap" + letter) as Texture2D;
            }
            else
            {
                return Resources.Load("WordPuzzle/letters/" + letter) as Texture2D;
            }
        }

        void setup(GameObject init, char letter)
        {
            prefabPosition = init.GetComponent<Transform>().position;
            prefabRotation = init.GetComponent<Transform>().eulerAngles.y;
            // set letter texture
            cubeObject.GetComponent<Renderer>().material.SetTexture("_BumpMap", loadTextFor(letter));
        }

        public Cube(GameObject init, char letter, int row, int column)
        {
            cubeObject = Instantiate(init);
            setup(init, letter);
            setOriginPosition(row, column);
        }

        // constructor for first button
        public Cube(GameObject init, char letter)
        {
            cubeObject = init;
            setup(init, letter);
        }

        void setOriginPosition(int row, int column)
        {
            // ATTENTION! maybe x or z axe may change
            Vector3 offset = Quaternion.AngleAxis(prefabRotation + 90.0f, new Vector3(0, 1, 0)) * new Vector3(0, WallManager.size.y * -row, WallManager.size.z * -column);
            originPosition = prefabPosition + offset;
            setPosition(originPosition);
        }

        void setPosition(Vector3 target)
        {
            cubeObject.GetComponent<Transform>().position = target;
        }

    }

    
}
