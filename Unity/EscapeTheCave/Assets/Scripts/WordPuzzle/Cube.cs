using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour {

    public readonly float deltaWidth = 0.09657f;
    public readonly float deltaHeight = 0.0961f;

    private int index = 0;
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
    private GameObject light;

    public void Setup(GameObject init, Wall wall, int index, bool correctKey, char letter, int row, int column)
    {
        if (-1 == size.x) size = GetComponent<MeshFilter>().mesh.bounds.size;
        prefabPosition = init.GetComponent<Transform>().position;
        prefabRotation = init.GetComponent<Transform>().eulerAngles.y;
        if (' ' != letter)
        {
            // set letter normal
            GetComponent<Renderer>().material.SetTexture("_BumpMap", LoadNormalTextFor(letter));
            // set letter notmal
            GetComponent<Renderer>().material.SetTexture("_MainTex", LoadTextFor(letter));
        }

        // set texture offset for uniqueness
        GetComponent<Renderer>().material.SetTextureOffset("_DetailAlbedoMap", new Vector2(Random.Range(0f, 1f), Random.Range(0f, 1f)));
        if (' ' == letter)
        {
            GetComponent<Renderer>().material.SetTextureOffset("_MainTex", new Vector2(Random.Range(0f, 1f), Random.Range(0f, 1f)));
        }
        this.letter = letter;
        this.index = index;
    
        SetOriginPosition(row, column);

        wallInstance = wall;

        this.correctKey = correctKey;

        if (correctKey)
        {
            light = Instantiate(wallInstance.StarLightPrefab, GetComponent<Transform>());
            light.SetActive(false);
        }

        ready = true;
    }
	
	void Update () {
        if (ready)
        {
            Move();
            if (!moving && pressed && !signalSent)
            {
                Debug.Log(correctKey);
                signalSent = true;
                wallInstance.PressedKey(correctKey);
                if (correctKey) light.SetActive(true);
            }
            Highlight();
        }
    }

    public void Finish()
    {
        ready = false;
        SetEmmission(0.0f);
    }

    public void Unpress()
    {
        if (' ' != letter)
        {
            pressed = false;
            moving = true;
            signalSent = false;
            if (correctKey) light.SetActive(false);
        }
    }

    public void Press()
    {
        //Debug.Log(index);
        if (!pressed && !moving && !signalSent && ' ' != letter)
        {
            pressed = true;
            moving = true;
        }
    } 

    void Move()
    {
        moving = false;
        if (pressed && 0.03f > currentPressedLevel)
        {
            moving = true;
            currentPressedLevel += Time.deltaTime * 0.2f;
            SetLocalPosition(originLocalPosition + new Vector3(0.0f, currentPressedLevel, 0.0f));
        } else if (!pressed && 0.0f < currentPressedLevel)
        {
            moving = true;
            currentPressedLevel -= Time.deltaTime * 0.2f;
            SetLocalPosition(originLocalPosition + new Vector3(0.0f, currentPressedLevel, 0.0f));
        }
    }

    void Highlight()
    {
        bool focused = this.gameObject == GameManager.focused && !pressed;
        if (focused && letter != ' ' && 0.1f > currentEmissionLevel)
        {
            currentEmissionLevel += Time.deltaTime * 0.4f;
            SetEmmission(currentEmissionLevel);
        } else if (!focused && 0.0f < currentEmissionLevel)
        {
            currentEmissionLevel -= Time.deltaTime * 0.7f;
            SetEmmission(currentEmissionLevel);
        }
        
    }

    void SetOriginPosition(int row, int column)
    {
        float offsetFromWall = 0.0616f - 0.04709972f;
        if (' ' != letter) offsetFromWall = (Random.Range(0f, 100f) - 50.0f) / 6000.0f;
        // ATTENTION! maybe x or z axe may change
        float width = (size.z + deltaWidth) * -column;
        float height = (size.y + deltaHeight) * -row;
        Vector3 offset = Quaternion.AngleAxis(prefabRotation + 90.0f, new Vector3(0, 1, 0)) * new Vector3(offsetFromWall, height, width);
        originPosition = prefabPosition + offset;
        SetPosition(originPosition);
        originLocalPosition = GetComponent<Transform>().localPosition;
    }

    void SetEmmission(float level)
    {
        GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.yellow * Mathf.LinearToGammaSpace(level));
    }

    void SetPosition(Vector3 target)
    {
        GetComponent<Transform>().position = target;
    }

    void SetLocalPosition(Vector3 target)
    {
        GetComponent<Transform>().localPosition = target;
    }

    public static Texture2D LoadNormalTextFor(char letter)
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

    public static Texture2D LoadTextFor(char letter)
    {
        if (char.IsUpper(letter))
        {
            return Resources.Load("WordPuzzle/LettersTextures/" + letter) as Texture2D;
        }
        else
        {
            return Resources.Load("WordPuzzle/LettersTextures/" + letter) as Texture2D;
        }
    }
}
