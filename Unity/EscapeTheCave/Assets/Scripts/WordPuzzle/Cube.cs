using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour {

    public readonly float deltaWidth = 0.01023f;
    public readonly float deltaHeight = 0.00240f;

    private char letter;
    private Vector3 prefabPosition;
    private float prefabRotation;
    private Vector3 originPosition;
    private Vector3 originLocalPosition;
    private static Vector3 size = new Vector3(-1, -1, -1);
    private float currentEmissionLevel = 0.0f;
    private bool pressed = false;
    private float currentPressedLevel = 0.0f;
    private bool ready = false;
    private bool moving = false;
    private Wall wallInstance;
    public bool correctKey;
    public bool signalSent = false;

    public void setup(GameObject init, Wall wall, bool correctKey, char letter, int row, int column)
    {
        prefabPosition = init.GetComponent<Transform>().position;
        prefabRotation = init.GetComponent<Transform>().eulerAngles.y;
        // set letter texture
        GetComponent<Renderer>().material.SetTexture("_BumpMap", loadTextFor(letter));
        // set texture offset for uniqueness
        GetComponent<Renderer>().material.SetTextureOffset("_DetailAlbedoMap", new Vector2(Random.Range(0f, 1f), Random.Range(0f, 1f)));

        this.letter = letter;
    
        setOriginPosition(row, column);

        wallInstance = wall;

        this.correctKey = correctKey;

        ready = true;
    }

    void Start () {
		if (-1 == size.x) size = GetComponent<MeshFilter>().mesh.bounds.size;
    }
	
	void Update () {
        if (ready)
        {
            if (!moving && pressed && !signalSent)
            {
                Debug.Log(correctKey);
                wallInstance.pressedKey(correctKey);
                signalSent = true;
            }
            highlight();
            move();
        }
    }

    public void finish()
    {
        ready = false;
        setEmmission(0.0f);
    }

    public void unpress()
    {
        if (' ' != letter)
        {
            pressed = false;
            moving = true;
            signalSent = false;
        }
    }

    public void press()
    {
        if (' ' != letter)
        {
            pressed = true;
            moving = true;
        }
    } 

    void move()
    {
        moving = false;
        if (pressed && 0.03f > currentPressedLevel)
        {
            moving = true;
            currentPressedLevel += Time.deltaTime * 0.2f;
            setLocalPosition(originLocalPosition + new Vector3(0.0f, 0.0f, currentPressedLevel));
        } else if (!pressed && 0.0f < currentPressedLevel)
        {
            moving = true;
            currentPressedLevel -= Time.deltaTime * 0.2f;
            setLocalPosition(originLocalPosition + new Vector3(0.0f, 0.0f, currentPressedLevel));
        }
    }

    void highlight()
    {
        bool focused = this.gameObject == Interact.focused && !pressed;
        if (focused && letter != ' ' && 0.1f > currentEmissionLevel)
        {
            currentEmissionLevel += Time.deltaTime * 0.4f;
            setEmmission(currentEmissionLevel);
        } else if (!focused && 0.0f < currentEmissionLevel)
        {
            currentEmissionLevel -= Time.deltaTime * 0.7f;
            setEmmission(currentEmissionLevel);
        }
        
    }

    void setOriginPosition(int row, int column)
    {
        // ATTENTION! maybe x or z axe may change
        Vector3 offset = Quaternion.AngleAxis(prefabRotation + 90.0f, new Vector3(0, 1, 0)) * new Vector3((Random.Range(0f, 100f) -50.0f) / 6000.0f, (size.y + deltaHeight) * -row, (size.z + deltaWidth) * -column);
        originPosition = prefabPosition + offset;
        setPosition(originPosition);
        originLocalPosition = GetComponent<Transform>().localPosition;
    }

    void setEmmission(float level)
    {
        GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.yellow * Mathf.LinearToGammaSpace(level));
    }

    void setPosition(Vector3 target)
    {
        GetComponent<Transform>().position = target;
    }

    void setLocalPosition(Vector3 target)
    {
        GetComponent<Transform>().localPosition = target;
    }

    public static Texture2D loadTextFor(char letter)
    {
        if (char.IsUpper(letter))
        {
            return Resources.Load("WordPuzzle/letters/" + letter) as Texture2D;
        }
        else
        {
            return Resources.Load("WordPuzzle/letters/" + letter) as Texture2D;
        }
    }
}
