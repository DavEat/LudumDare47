using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameScreen : MonoBehaviour
{
    public void Display()
    {
        gameObject.SetActive(true);
    }
    public void Restart()
    {
        GameManager.inst.NextDay();
    }
}
