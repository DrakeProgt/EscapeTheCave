using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterZone2 : MonsterZone {

	protected override void Init()
	{
		CameraSequenceDuration = 4;
		sequences = new int[2][];
		sequences[0] = new []{ 0, 1, 2, 3};
		sequences[1] = new []{ 3, 2, 1, 0};

		GameManager.monsterZones[1] = this;
	}

	protected override int NextSequence()
	{
		switch (currentSequence)
		{
			case 0: return 1;
			case 1: return 0;
		}

		return 0;
	}

	protected override int returnSequence()
	{
		switch (currentPoint)
		{
			case 3: return 2;
			case 2: return 1;
			case 1: return 0;
			case 0: return -1;
			default: return -1;
		}
	}
}
