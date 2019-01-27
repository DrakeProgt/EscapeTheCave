using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterZone3 : MonsterZone
{
    protected override void Init()
    {
        CameraSequenceDuration = 5;
        sequences = new int[2][];
        sequences[0] = new[] {0, 1, 2, 3, 4, 5};
        sequences[1] = new[] {5, 4, 3, 2, 1, 0};

        GameManager.monsterZones[2] = this;
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
            case 5: return 4;
            case 4: return 3;
            case 3: return 2;
            case 2: return 1;
            case 1: return 0;
            case 0: return -1;
            default: return -1;
        }
    }
}