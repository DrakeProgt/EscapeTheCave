using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterZone4: MonsterZone {

	protected override void Init()
	{
		CameraSequenceDuration = 2;
		sequences = new int[2][];
		sequences[0] = new[] {0, 1, 2, 3, 4};
		sequences[1] = new[] {4, 3, 2, 1, 0};

		GameManager.monsterZones[3] = this;
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
			case 4: return 3;
			case 3: return 2;
			case 2: return 1;
			case 1: return 0;
			case 0: return -1;
			default: return -1;
		}
	}
}
