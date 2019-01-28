using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class FakePulse : MonoBehaviour
{
    private static FakePulse instance;

    private float pulse;
    private Operation operation;

    public static FakePulse GetInstance()
    {
        if (instance == null)
        {
            instance = new FakePulse();
        }

        return instance;
    }

    public IEnumerator PulseLoop()
    {
        while (true)
        {
            if (pulse == 60)
                operation = new Operation(Inc);
            else if (pulse == 180)
                operation = new Operation(Dec);

            operation(ref pulse);
            //print(pulse);

            yield return new WaitForSecondsRealtime(0.2f);
        }
    }
    
    private float getLowestMonsterDistance()
    {
        float minimalDistance = Vector3.Distance(GameManager.Player.transform.position, GameManager.monsterPosition);
        foreach (var monsterZone in GameManager.monsterZones)
        {
            if (monsterZone.isActive)
            {
                minimalDistance = 
                    Mathf.Min(Vector3.Distance(GameManager.Player.transform.position, monsterZone.Monster.transform.position));
            }
            
        }
        return minimalDistance;
    }
    
    public IEnumerator PulseAdvancedRandom()
    {
        float minDistance;
        bool rising = true;
        while (true)
        {
            if (!GameManager.secondCaveReached)
            {
                // in the first cave, the pulse will bounce between 60 and 110
                if (rising)
                {
                    pulse += Random.Range(-1.0f, 2.0f);
                }
                else
                {
                    pulse += Random.Range(-2.0f, 1.0f);
                }

                if (pulse < 60)
                {
                    pulse = 60;
                    rising = true;
                }
                
                if (pulse > 110)
                {
                    pulse = 110;
                    rising = false;
                }

            }
            else
            {
                // in the second cave, the pulse will relay on the distance to the monsters
                minDistance = getLowestMonsterDistance();
                if (minDistance > 50)
                {
                    pulse += Random.Range(-2.0f, 2.0f);
                }
                else
                {
                    pulse += Random.Range(-2.0f + (minDistance / 10.0f), 2.0f + (minDistance / 10.0f));
                }
                
                if (pulse < 60) pulse = 60;
                if (pulse > 180) pulse = 180;
            }
            
            print(pulse);

            yield return new WaitForSecondsRealtime(0.3f);
        }
    }

    private delegate void Operation(ref float val);

    private void Inc(ref float val)
    {
        val++;
    }

    private void Dec(ref float val)
    {
        val--;
    }

    public float GetLivePulse()
    {
        return pulse;
    }

    public void Init()
    {
        pulse = 60;
        operation = new Operation(Inc);
    }
}
