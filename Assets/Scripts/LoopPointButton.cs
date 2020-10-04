using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopPointButton : MonoBehaviour
{
    LoopPoint m_loopPoint = null;

    RectTransform m_rect = null;
    bool m_addbutton = false;

    bool m_down = false;
    Vector2 m_downAtPosition = Vector2.one * -1f;
    float m_moveTolerance = .1f;

    float m_defaultSize = 128;
    float m_defaultAnchorArrow = 128;

    LoopPointButton m_target = null;

    public void Init(LoopPoint point, Vector3 position, bool addbutton = false, LoopPointButton target = null)
    {
        m_addbutton = addbutton;

        m_loopPoint = point;

        m_rect = GetComponent<RectTransform>();

        m_defaultSize = m_rect.sizeDelta.x;
        if (!m_addbutton)
        {
            m_defaultAnchorArrow = m_rect.GetChild(0).GetComponent<RectTransform>().anchoredPosition.y;
            LoopEditor.inst.pointMove += UpdateAngle;
        }
        else
        {
            m_target = target;
            LoopEditor.inst.pointMove += FindMiddleFromTarget;
        }

        SetPosition(position);
    }
    void FindMiddleFromTarget()
    {
        SetPosition((m_target.m_loopPoint.GetPosition() - m_loopPoint.GetPosition()) * .5f + m_loopPoint.GetPosition());
    }
    void SetPosition(Vector3 position)
    {
        Vector2 screenPoint = Camera.main.WorldToScreenPoint(position);
        m_rect.position = screenPoint;
        m_rect.sizeDelta = (m_defaultSize - (Camera.main.transform.position - position).magnitude) * Vector2.one;
    }
    public void SetAngle(LoopPointButton target)
    {
        m_target = target;
        UpdateAngle();
    }
    public void UpdateAngle()
    {
        float angle = -Vector2.SignedAngle(m_target.m_rect.position - m_rect.position, Vector2.up);
        m_rect.eulerAngles = Vector3.forward * angle;
        float ratio = m_defaultSize / m_rect.sizeDelta.x;
        RectTransform arrow = m_rect.GetChild(0).GetComponent<RectTransform>();
        arrow.anchoredPosition = (m_defaultAnchorArrow / ratio) * Vector2.up;

        UpdateSize();
    }
    public void UpdateSize()
    {
        RectTransform line = m_rect.GetChild(1).GetComponent<RectTransform>();
        line.sizeDelta = new Vector2(line.sizeDelta.x, (m_target.m_rect.position - line.position).magnitude - (line.position - m_rect.position).magnitude * .4f);
    }
    void Update()
    {
        Vector2 mouse = Input.mousePosition;

        if (Input.GetMouseButtonUp(0))
        {
            if (!RectTransformUtility.RectangleContainsScreenPoint(m_rect, mouse))
            {
                ResetButton();
                return;
            }

            if (m_down && (mouse - m_downAtPosition).sqrMagnitude > m_moveTolerance)
            {
                //End drag
                Debug.Log("EndDrag");
                if (LoopEditor.inst.endMove != null) LoopEditor.inst.endMove.Invoke();
            }
            else
            {
                //Click
                Debug.Log("Click");
                if (m_addbutton)
                {
                    RaycastHit hit;
                    Ray ray = Camera.main.ScreenPointToRay(m_rect.position);
                    if (Physics.Raycast(ray, out hit, 1000, GameManager.inst.GroundLayer))
                    {
                        PlayerLoop.inst.AddPoint(m_loopPoint, hit.point);
                        //if (LoopEditor.inst.endMove != null) LoopEditor.inst.endMove.Invoke();
                    }
                }
                else
                {
                    gameObject.SetActive(false);
                    ResetButton();
                    PlayerLoop.inst.RemovePoint(m_loopPoint);
                }
                if (LoopEditor.inst.endMove != null) LoopEditor.inst.endMove.Invoke();
                LoopEditor.inst.Recreate();
            }
        }
        else if (Input.GetMouseButtonDown(0))
        {
            if (!RectTransformUtility.RectangleContainsScreenPoint(m_rect, mouse))
            {
                ResetButton();
                return;
            }

            m_down = true;
            m_downAtPosition = mouse;
        }
        else if (Input.GetMouseButton(0))
        {
            if (m_down && (mouse - m_downAtPosition).sqrMagnitude > .1f)
            {
                if (m_addbutton) return;

                //Start drag
                Debug.Log("StartDrag");
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(mouse);
                if (Physics.Raycast(ray, out hit, 1000, GameManager.inst.GroundLayer))
                {
                    m_loopPoint.SetPosition(hit.point);
                    SetPosition(hit.point);

                    if (LoopEditor.inst.pointMove != null) LoopEditor.inst.pointMove.Invoke();
                }
            }
        }
    }

    void ResetButton()
    {
        m_down = false;
        m_downAtPosition = Vector2.one * -1f;
    }
}