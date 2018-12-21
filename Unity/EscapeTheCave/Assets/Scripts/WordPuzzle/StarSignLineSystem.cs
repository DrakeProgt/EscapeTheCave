using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarSignLineSystem : MonoBehaviour {

    public GameObject Line1;
    public GameObject Line2;
    public GameObject Line3;
    public GameObject Line4;
    public GameObject Line5;
    public GameObject Line6;
    public GameObject Line7;
    public GameObject Line8;
    public GameObject Line9;
    public GameObject Line10;
    
    private StarSignLine lineScript1;
    private StarSignLine lineScript2;
    private StarSignLine lineScript3;
    private StarSignLine lineScript4;
    private StarSignLine lineScript5;
    private StarSignLine lineScript6;
    private StarSignLine lineScript7;
    private StarSignLine lineScript8;
    private StarSignLine lineScript9;
    private StarSignLine lineScript10;

    void Start()
    {
	    // Get the scripts for speed up
	    lineScript1 = Line1.GetComponent<StarSignLine>();
	    lineScript2 = Line2.GetComponent<StarSignLine>();
	    lineScript3 = Line3.GetComponent<StarSignLine>();
	    lineScript4 = Line4.GetComponent<StarSignLine>();
	    lineScript5 = Line5.GetComponent<StarSignLine>();
	    lineScript6 = Line6.GetComponent<StarSignLine>();
	    lineScript7 = Line7.GetComponent<StarSignLine>();
	    lineScript8 = Line8.GetComponent<StarSignLine>();
	    lineScript9 = Line9.GetComponent<StarSignLine>();
	    lineScript10 = Line10.GetComponent<StarSignLine>();
    }
    
	public bool buildUp()
	{
		if (!lineScript1.buildReady)
		{
			lineScript1.buildUp();
			return false;
		}

		if (!lineScript2.buildReady)
		{
			lineScript2.buildUp();
			return false;
		}
		
		if (!lineScript3.buildReady)
		{
			lineScript3.buildUp();
			return false;
		}
		
		// now two at the same time
		if (!lineScript4.buildReady && !lineScript6.buildReady)
		{
			lineScript4.buildUp();
			lineScript6.buildUp();
			return false;
		}
		
		if (!lineScript5.buildReady && !lineScript7.buildReady && !lineScript9.buildReady)
		{
			lineScript5.buildUp();
			lineScript7.buildUp();
			lineScript9.buildUp();
			return false;
		}
		
		
		if (!lineScript8.buildReady && !lineScript10.buildReady)
		{
			lineScript8.buildUp();
			lineScript10.buildUp();
			return false;
		}
		
		return true;
	}

}
