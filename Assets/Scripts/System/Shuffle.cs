using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuffle<T>
{
    public static void StartShuffleArray(T[] a)
    {
        for (int i = a.Length - 1; i > 0; i--)
        {
            int random = Random.Range(0, i);

            T temp = a[i];

            a[i] = a[random];
            a[random] = temp;
        }
    }

    public static void StartShuffleList(List<T> a)
    {
        for (int i = a.Count - 1; i > 0; i--)
        {
            int random = Random.Range(0, i);

            T temp = a[i];

            a[i] = a[random];
            a[random] = temp;
        }
    }
}
