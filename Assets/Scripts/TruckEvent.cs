using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckEvent : MonoBehaviour
{
    [SerializeField] GameObject[] trucks = null;

    void Start()
    {
        if (GameManager.inst.currentDay > 1)
        {
            int index = Random.Range(0, 3);
            if (index > 1) return;

            trucks[index].SetActive(true);
        }
    }
}
