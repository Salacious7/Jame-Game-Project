using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuffle<T>
{
    public static void StartShuffle(T[] a)
    {
        for (int i = a.Length - 1; i > 0; i--)
        {
            int random = Random.Range(0, i);

            T temp = a[i];

            a[i] = a[random];
            a[random] = temp;
        }
    }
}
