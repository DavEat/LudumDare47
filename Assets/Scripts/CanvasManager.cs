using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Rendering.HybridV2;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : Singleton<CanvasManager>
{
    [SerializeField] GameObject canvas = null;
    [SerializeField] ScoreScreen m_scoreScreen = null;
    [SerializeField] EndGameScreen m_endGameScreen = null;

    public static RectTransform loopRect = null;

    [SerializeField] Image m_startDayBackground = null;
    [SerializeField] TextMeshProUGUI m_startDayText = null;

    void Start()
    {
        canvas.SetActive(true);
        loopRect = canvas.transform.GetChild(0).GetComponent<RectTransform>();


        m_startDayText.text = "Day " + GameManager.inst.currentDay;
        StartCoroutine(Fade(true,
                            new Image[] { m_startDayBackground },
                            new TextMeshProUGUI[] { m_startDayText },
                            .01f));
    }

    public void Display_ScoreScreen(float distance, float time, int interactions, byte starCount, bool end)
    {
        m_scoreScreen.Display(distance, time, interactions, starCount, end);
    }
    public void Display_EndGameScreen()
    {
        m_endGameScreen.Display();
    }

    public static void FadeStart(float start, Image[] img, TextMeshProUGUI[] text)
    {
        for (int i = 0; i < img.Length; i++)
        {
            Color c = img[i].color;
            c.a = start;
            img[i].color = c;
        }
        for (int i = 0; i < text.Length; i++)
        {
            Color c = text[i].color;
            c.a = start;
            text[i].color = c;
        }
    }
    public static IEnumerator Fade(bool isOut, Image[] img, TextMeshProUGUI[] text, float fadeSpeed = .01f)
    {
        float start = isOut ? 1 : 0;
        float end = isOut ? 0 : 1;

        FadeStart(start, img, text);

        yield return new WaitForSeconds(.3f);

        float lerpAmount = 0;
        while (lerpAmount < 1)
        {
            float a = Mathf.Lerp(start, end, lerpAmount);
            for (int i = 0; i < img.Length; i++)
            {
                Color c = img[i].color;
                c.a = a;
                img[i].color = c;
            }
            for (int i = 0; i < text.Length; i++)
            {
                Color c = text[i].color;
                c.a = a;
                text[i].color = c;
            }
            lerpAmount += fadeSpeed;
            yield return null;
        }
        FadeStart(end, img, text);
    }
}