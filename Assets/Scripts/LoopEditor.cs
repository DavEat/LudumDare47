using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoopEditor : Singleton<LoopEditor>
{
    public delegate void PointMove();
    public PointMove pointMove;
    public PointMove endMove;

    [SerializeField] LoopPointButton m_prefab_loopPointButton = null;
    [SerializeField] LoopPointButton m_prefab_addLoopPointButton = null;
    [SerializeField] RectTransform m_LoopPointsButtonContainer = null;
    [SerializeField] RectTransform m_pinTargetPos = null;

    int m_maxLoopPoint = 4;

    Transform m_transform = null;

    [HideInInspector] public LoopPointButton PointSelected = null;

    void Start()
    {
        m_transform = GetComponent<Transform>();
        SpawnAllButtons();
    }
    void SpawnAllButtons()
    {
        LoopPoint lastPoint = null;
        LoopPointButton firstButton = null;
        LoopPointButton lastButton = null;
        int index = 0;

        foreach (LoopPoint lp in PlayerLoop.inst.GetLoopPoints())
        {
            LoopPointButton b = Instantiate(m_prefab_loopPointButton, m_LoopPointsButtonContainer);
            b.Init(lp, lp.GetPosition());
            
            if (lastPoint != null && PlayerLoop.inst.GetSize() < m_maxLoopPoint)
            {
                Vector3 position = (lastPoint.GetPosition() - lp.GetPosition()) * .5f + lp.GetPosition();
                Instantiate(m_prefab_addLoopPointButton, m_LoopPointsButtonContainer).Init(lastPoint, position, true, b);
            }

            if (lastButton != null) lastButton.SetAngle(b);
            else firstButton = b;

            lastPoint = lp;
            lastButton = b;
            index++;
        }
        if (PlayerLoop.inst.GetSize() < m_maxLoopPoint)
        {
            Vector3 position0 = (lastPoint.GetPosition() - PlayerLoop.inst.GetPointAt(0).GetPosition()) * .5f + PlayerLoop.inst.GetPointAt(0).GetPosition();
            Instantiate(m_prefab_addLoopPointButton, m_LoopPointsButtonContainer).Init(lastPoint, position0, true, firstButton);
        }
        lastButton.SetAngle(firstButton);
    }
    public void SetPinPosition(Vector3 position)
    {
        Vector2 screenPoint = Camera.main.WorldToScreenPoint(position);
        m_pinTargetPos.position = screenPoint;
        m_pinTargetPos.sizeDelta = (64 - (Camera.main.transform.position - position).magnitude) * Vector2.one;
    }
    public void Recreate()
    {
        for (int i = m_LoopPointsButtonContainer.childCount - 1; i >= 0; i--)
        {
            Destroy(m_LoopPointsButtonContainer.GetChild(i).gameObject);
        }
        pointMove = null;

        SpawnAllButtons();
    }
}