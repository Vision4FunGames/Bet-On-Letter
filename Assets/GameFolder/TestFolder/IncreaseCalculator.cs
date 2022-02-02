using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseCalculator : MonoBehaviour
{
    public float initial;
    public float final;

    private void Start()
    {
        float dividePart = (final - initial) / initial;
        print(dividePart * 100);
    }
}
