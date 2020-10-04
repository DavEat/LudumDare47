using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScreen : MonoBehaviour
{
    [SerializeField] GameObject[] m_stars = null;
    [SerializeField] GameObject[] m_stars_info = null;
    [SerializeField] TextMeshProUGUI m_distance = null;
    [SerializeField] TextMeshProUGUI m_time = null;
    [SerializeField] TextMeshProUGUI m_intera = null;

    [SerializeField] GameObject m_endButton = null;

    [SerializeField] 

    string m_dst_base = "Distance: ";
    string m_tim_base = "Time: ";
    string m_int_base = "Interactions: ";

    public void Display(float distance, float time, int interactions, byte starCount, bool end)
    {
        m_distance.text = m_dst_base + distance.ToString("F2").Replace(',', '.');
        m_time.text =     m_tim_base + time.ToString("F2").Replace(',', '.');
        m_intera.text =   m_int_base + interactions;

        int numStart = 0;
        if (1 == ((starCount >> 0) & 1))
        {
            numStart++;
            m_stars_info[0].SetActive(true);
        }
        if (1 == ((starCount >> 1) & 1))
        {
            numStart++;
            m_stars_info[1].SetActive(true);
        }
        if (1 == ((starCount >> 2) & 1))
        {
            numStart++;
            m_stars_info[2].SetActive(true);
        }

        for (int i = 0; i < m_stars.Length; i++)
            m_stars[i].SetActive(i > numStart-1);

        if (end)
        {
            m_endButton.SetActive(true);
        }

        Image[] img = transform.GetComponentsInChildren<Image>();
        TextMeshProUGUI[] text = transform.GetComponentsInChildren<TextMeshProUGUI>();

        CanvasManager.FadeStart(0, img, text);
        gameObject.SetActive(true);
        StartCoroutine(CanvasManager.Fade(false, img, text, .01f));
    }
    public void Replay()
    {
        GameManager.inst.Replay();
    }
    public void Next()
    {
        GameManager.inst.NextDay();
    }
    public void End()
    {
        CanvasManager.inst.Display_EndGameScreen();
    }
}