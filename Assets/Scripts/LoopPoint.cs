using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopPoint : MonoBehaviour
{
    Transform m_transform = null;

    void Awake()
    {
        Init();
    }
    public void Init()
    {
        m_transform = GetComponent<Transform>();
    }
    public LoopPoint Init(Vector3 position, Transform parent)
    {
        m_transform = GetComponent<Transform>();
        m_transform.position = position;
        m_transform.parent = parent;
        return this;
    }
    public Vector3 GetPosition()
    {
        return m_transform.position;
    }
    public Transform GetTransform()
    {
        return m_transform;
    }
    public void SetPosition(Vector3 position)
    {
        if (m_transform == null)
            return;
        m_transform.position = position;
    }
    public void Remove()
    {
        //Destroy(this);
        Destroy(gameObject);
    }
}
