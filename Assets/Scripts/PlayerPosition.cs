using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPosition : Singleton<PlayerPosition>
{
    Transform m_transform = null;
    Transform m_frontWheel = null;
    public Vector3 position { get { return m_transform.position; } }
    public Vector3 frontWheel { get { return m_frontWheel.position; } }

    void Awake()
    {
        m_transform = GetComponent<Transform>();
        m_frontWheel = m_transform.GetChild(0);
    }
}