using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiaryHandler : MonoBehaviour
{

	public GameObject Book;
	public GameObject LeftPage;
	public GameObject RightPage;
	public GameObject TravellingPage;

	// delay for opening
	public float PageAppearDelay = 1500.0f;

	// delay for closing
	public float BookCloseDelay = 500.0f;
	public float PageDisappearDelay = 1500.0f;
	
	public int pageCount = 14;
	private Texture2D[] pageTextures;
	
	private Animator BookAnimator;
	private Animator LeftPageAnimator;
	private Animator TravellingPageAnimator;
	private AnimatorClipInfo[] LeftPageClipInfo;

	//states
	private bool opened = false;
	private bool running = false;
	private bool opening = false;
	private bool closing = false;
	private bool switchingForward = false;
	private bool switchingBackward = false;
	
	public int  currentLeftPage = 1;
	private float time = -1.0f;
	
	private Tweeny upMovement;
	private Tweeny downMovement;
	private Vector3 targetPosition;
	public Vector3 downPositionOffset = new Vector3(0,-0.5f, 0);

    // Use this for initialization
    void Start () {
		BookAnimator = Book.GetComponent<Animator>();
		LeftPageAnimator = LeftPage.GetComponent<Animator>();
		TravellingPageAnimator = TravellingPage.GetComponent<Animator>();
		// diary is hidden on start
		Book.SetActive(false);
		RightPage.SetActive(false);
		LeftPage.SetActive(false);
		TravellingPage.SetActive(false);

		targetPosition = GetComponent<Transform>().localPosition;
		upMovement = new Tweeny(targetPosition + downPositionOffset, targetPosition, 2000, "default");
		downMovement = new Tweeny(targetPosition, targetPosition+ downPositionOffset, 2000, "default");

		pageTextures = new Texture2D[pageCount+1];
		for (int i = 0; i <= pageCount; i++)
		{
			pageTextures[i] = Resources.Load("Diary/Pages/" + i) as Texture2D;
		}
    }
	
	// Update is called once per frame
	void Update () {
		
		
		if (!running)
		{
			// toggle diary init
			if(GameManager.pressedR2Key)
			{
				opened = !BookAnimator.GetBool("DiaryOpened");
				if(opened) Book.SetActive(true);
			
				upMovement.reset();
				downMovement.reset();
				if (opened)
				{
					opening = true;
                    SoundSystem.PlaySound("Audio/Diary/Tagebuch1-Öffnen-Schließen", 1, 1, 10, 0, null, 0);
                    SoundSystem.PlaySound("Audio/Diary/Tagebuch2-Blättern", 2, 1, 10, 0, null, 0);
                    GetComponent<Transform>().localPosition = targetPosition + downPositionOffset;
				}
				else
				{
					closing = true;
                    SoundSystem.PlaySound("Audio/Diary/Tagebuch1-Öffnen-Schließen", 0, 1, 10, 0, null, 0);
                    SoundSystem.PlaySound("Audio/Diary/Tagebuch2-Blättern", 0, 1, 10, 0, null, 0);
                }
				running = true;
				time = Time.time * 1000;
				setLeftPage(currentLeftPage);
				setRightPage(currentLeftPage + 1);
				return;
			}
			
			// switching forward
			if (opened && GameManager.pressedR1Key && currentLeftPage + 2 < pageCount)
			{
                SoundSystem.PlaySound("Audio/Diary/Tagebuch2-Blättern", .5f, 1, 10, 0, null, 0);

                setTravellingPage(true, currentLeftPage + 1);
				setTravellingPage(false, currentLeftPage + 2);
				currentLeftPage += 2;
				TravellingPageAnimator.SetBool("left", false);
				TravellingPageAnimator.SetBool("animated", false);
				TravellingPage.SetActive(true);
				running = true;
				switchingForward = true;
				time = Time.time * 1000;
				return;
			}
			
			// switching backward
			if (opened && GameManager.pressedL1Key && currentLeftPage > 2)
			{
                SoundSystem.PlaySound("Audio/Diary/Tagebuch2-Blättern", .5f, 1, 10, 0, null, 0);

                setTravellingPage(true, currentLeftPage - 1);
				setTravellingPage(false, currentLeftPage);
				currentLeftPage -= 2;
				TravellingPage.SetActive(true);
				TravellingPageAnimator.SetBool("left", true);
				TravellingPageAnimator.SetBool("animated", false);
				running = true;
				switchingBackward = true;
				time = Time.time * 1000;
				return;
			}
			
		} else // run animations
		{
			if (opening)
			{
				// book up movement
				if (!upMovement.finished) GetComponent<Transform>().localPosition = upMovement.nextVector();
				
				// page and book opening animation
				if (stateEquals(BookAnimator, "Closed")) BookAnimator.SetBool("DiaryOpened", opened);
				
				// delay appearance of the pages
				if (opened && (time + PageAppearDelay) < (Time.time * 1000))
				{
					// delay opening animation start
					LeftPage.SetActive(true);
					RightPage.SetActive(true);
					if (LeftPageAnimator.isInitialized && stateEquals(LeftPageAnimator, "Right")) LeftPageAnimator.SetBool("left", true);
				}
				
				// check if running complete
				if (stateEquals(LeftPageAnimator, "Left") && upMovement.finished)
				{
					running = false;
					opening = false;
					time = -1;
				}
			}
			
			if (closing)
			{
				// book down movement
				if (!downMovement.finished) GetComponent<Transform>().localPosition = downMovement.nextVector();

				// page and book opening animation
				if (stateEquals(LeftPageAnimator, "Left")) LeftPageAnimator.SetBool("left", false);
				
				// delay closing of book
				if ((time + BookCloseDelay) < (Time.time * 1000))
				{
					if (stateEquals(BookAnimator, "Opened")) BookAnimator.SetBool("DiaryOpened", false);
				}
				
				// delay disappearance of the pages
				if ((time + PageDisappearDelay) < (Time.time * 1000))
				{
					// delay opening animation start
					LeftPage.SetActive(false);
					RightPage.SetActive(false);
				}
				
				// check if running complete
				if (stateEquals(BookAnimator, "Closed") && downMovement.finished)
				{
					Book.SetActive(false);
					running = false;
					closing = false;
					time = -1;
				}
			}

			if (switchingForward)
			{
				Debug.Log("switchingForward");
				if (stateEquals(TravellingPageAnimator, "Right"))
				{
					TravellingPageAnimator.SetBool("animated", true);
					TravellingPageAnimator.SetBool("left", true);
					setRightPage(currentLeftPage + 1);
				}
				
				// check if running complete
				if (stateEquals(TravellingPageAnimator, "Left"))
				{
					setLeftPage(currentLeftPage);
					TravellingPage.SetActive(false);
					running = false;
					switchingForward = false;
					time = -1;
				}
			}
			
			if (switchingBackward)
			{
				Debug.Log("switchingBack");
				if (stateEquals(TravellingPageAnimator, "Left"))
				{
					Debug.Log("switchingBack 1");
					TravellingPageAnimator.SetBool("animated", true);
					TravellingPageAnimator.SetBool("left", false);
					setLeftPage(currentLeftPage);
				}
				
				// check if running complete
				if (stateEquals(TravellingPageAnimator, "Right") && TravellingPageAnimator.GetBool("animated"))
				{
					Debug.Log("switchingBack 2");
					setRightPage(currentLeftPage + 1);
					TravellingPage.SetActive(false);
					running = false;
					switchingBackward = false;
					time = -1;
				}
			}
		}
	}

	void setTravellingPage(bool leftSide, int number)
	{
		int material = leftSide ? 1 : 2;
		if (number > pageCount) number = 0;
		TravellingPage.GetComponent<Transform>().Find("Page").GetComponent<Renderer>().materials[material].SetTexture("_DetailAlbedoMap", pageTextures[number]);
	}
	
	void setLeftPage(int number)
	{
		if (number > pageCount) number = 0;
		LeftPage.GetComponent<Transform>().Find("Page").GetComponent<Renderer>().materials[2].SetTexture("_DetailAlbedoMap", pageTextures[number]);
	}
	
	void setRightPage(int number)
	{
		if (number > pageCount) number = 0;
		RightPage.GetComponent<Transform>().Find("Page").GetComponent<Renderer>().materials[1].SetTexture("_DetailAlbedoMap", pageTextures[number]);
	}

	
	bool stateEquals(Animator animator, String name)
	{
		return animator.GetCurrentAnimatorStateInfo(0).IsName(name);
	}
}
