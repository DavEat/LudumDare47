using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] GameObject canvas = null;

    void Start()
    {
        canvas.SetActive(true);
    }
}
