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
    private static Vector3 size = new Vector3(-1, -1, -1);
    private float currentEmissionLevel = 0.0f;
    private bool pressed = false;
    private float currentPressedLevel = 0.0f;

    public void setup(GameObject init, char letter, int row, int column)
    {
        prefabPosition = init.GetComponent<Transform>().position;
        prefabRotation = init.GetComponent<Transform>().eulerAngles.y;
        // set letter texture
        GetComponent<Renderer>().material.SetTexture("_BumpMap", loadTextFor(letter));
        // set texture offset for uniqueness
        GetComponent<Renderer>().material.SetTextureOffset("_DetailAlbedoMap", new Vector2(Random.Range(0f, 1f), Random.Range(0f, 1f)));

        this.letter = letter;

        if (!(column == 0 && row == 0))
        {
            setOriginPosition(row, column);
        }
    }

    void Start () {
		if (-1 == size.x) size = GetComponent<MeshFilter>().mesh.bounds.size;
    }
	
	void Update () {
        highlight();
        move();
    }

    public void press()
    {
        if (' ' != letter) pressed = true;
    } 

    void move()
    {
        if (pressed && 0.03f > currentPressedLevel)
        {
            currentPressedLevel += Time.deltaTime * 0.2f;
        } else if (!pressed && 0.0f < currentPressedLevel)
        {
            currentPressedLevel -= Time.deltaTime * 0.2f;
        }
        GetComponent<Transform>().position = originPosition + new Vector3(0.0f, 0.0f, -currentPressedLevel);
    }

    void highlight()
    {
        bool focused = this.gameObject == Interact.focused && !pressed;
        if (focused && letter != ' ' && 0.1f > currentEmissionLevel)
        {
            currentEmissionLevel += Time.deltaTime * 0.4f;
        } else if (!focused && 0.0f < currentEmissionLevel)
        {
            currentEmissionLevel -= Time.deltaTime * 0.7f;
        }
        GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.yellow * Mathf.LinearToGammaSpace(currentEmissionLevel));
    }

    void setOriginPosition(int row, int column)
    {
        // ATTENTION! maybe x or z axe may change
        Vector3 offset = Quaternion.AngleAxis(prefabRotation + 90.0f, new Vector3(0, 1, 0)) * new Vector3(0, (size.y + deltaHeight) * -row, (size.z + deltaWidth) * -column);
        originPosition = prefabPosition + offset;
        setPosition(originPosition);
    }

    void setPosition(Vector3 target)
    {
        GetComponent<Transform>().position = target;
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
