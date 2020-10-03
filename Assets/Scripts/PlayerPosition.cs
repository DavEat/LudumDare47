using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPosition : Singleton<PlayerPosition>
{
    Transform m_transform = null;
    public Vector3 position { get { return m_transform.position; } }

    void Awake()
    {
        m_transform = transform;
    }
}