using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotate : MonoBehaviour
{
    void Start()
    {
        int[] angles = new int[] { 0, 90, 180, 270 };
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).eulerAngles = new Vector3(0, angles[Random.Range(0, 4)], 0);
        }
    }
}