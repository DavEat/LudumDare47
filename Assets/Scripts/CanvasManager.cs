using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] GameObject canvas = null;

    public static RectTransform loopRect = null;

    void Start()
    {
        canvas.SetActive(true);
        loopRect = canvas.transform.GetChild(0).GetComponent<RectTransform>();
    }
}
