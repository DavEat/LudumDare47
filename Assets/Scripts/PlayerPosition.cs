using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPosition : Singleton<PlayerPosition>
{
    Transform m_transform = null;
    Transform m_frontWheel = null;
    Transform m_trowFrom = null;
    public Vector3 position { get { return m_transform.position; } }
    public Vector3 frontWheel { get { return m_frontWheel.position; } }
    public Vector3 trowFrom { get { return m_trowFrom.position; } }

    void Awake()
    {
        m_transform = GetComponent<Transform>();
        m_frontWheel = m_transform.GetChild(0);
        m_trowFrom = m_transform.GetChild(1);
    }
}