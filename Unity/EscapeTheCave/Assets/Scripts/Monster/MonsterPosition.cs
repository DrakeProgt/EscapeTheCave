using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPosition : MonoBehaviour {

	[SerializeField] MonsterBehaviour behaviour;

	// Use this for initialization
	void Start () {
		behaviour = MonsterBehaviour.Sleep;
	}
	
	// Update is called once per frame
	void Update () {

		if( behaviour == MonsterBehaviour.Sleep )
			GetComponentInChildren<AudioSource>().enabled = false;

		if( behaviour == MonsterBehaviour.Follow )//monster follows player 
		{ 
			transform.position = FollowPlayer();
			GetComponentInChildren<AudioSource>().enabled = true;
		}

		GameManager.monsterPosition = transform.position; //update position of monster in game manager (for dimming light)
	}

	Vector3 FollowPlayer()
	{
		return new Vector3( GameManager.playerPosition.x + 10.0f, GameManager.playerPosition.y, GameManager.playerPosition.z - 2.0f );
	}
}

enum MonsterBehaviour //state of monster
{
	Sleep = 0,
	Follow = 1
}