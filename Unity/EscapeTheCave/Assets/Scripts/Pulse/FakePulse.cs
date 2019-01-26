using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
