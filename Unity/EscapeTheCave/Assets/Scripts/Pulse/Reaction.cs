using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Reaction
{
    public abstract void ReactionLowIntensity(float currentPulse);
    public abstract void ReactionMediumIntensity(float currentPulse);
    public abstract void ReactionHighIntensity(float currentPulse);
}
