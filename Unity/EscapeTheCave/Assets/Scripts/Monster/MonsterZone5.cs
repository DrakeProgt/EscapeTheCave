using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterZone5 : MonsterZone {

	protected override void Init()
	{
		CameraSequenceDuration = 4;
		sequences = new int[3][];
		sequences[0] = new []{ 0, 1, 2, 3, 4, 5};
		sequences[1] = new []{ 5, 4, 3, 2, 1, 0, 6, 7, 8};
		sequences[2] = new []{ 8, 7, 6, 0, 1, 2, 3, 4, 5};

		GameManager.monsterZones[4] = this;
	}

	protected override int NextSequence()
	{
		switch (currentSequence)
		{
			case 0: return 1;
			case 1: return 2;
			case 2: return 1;
		}

		return 0;
	}

	protected override int returnSequence()
	{
		switch (currentPoint)
		{
			case 8: return 7;
			case 7: return 5;
			case 6: return 5;
			case 5: return 7;
			case 4: return 3;
			case 3: return 2;
			case 2: return 1;
			case 1: return 0;
			case 0: return -1;
			default: return -1;
		}
	}
}
