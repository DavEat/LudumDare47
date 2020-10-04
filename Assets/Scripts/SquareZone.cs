using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareZone : MonoBehaviour
{
    protected Vector3 m_corner0;
    protected Vector3 m_corner1;

    [SerializeField] protected SpriteRenderer sprite = null;

    protected Color m_defaultColor = new Color(0xFF / (float)0xFF, 0xD4 / (float)0xFF, 0x00 / (float)0xFF);
    protected Color m_pickUp = new Color(0x68 / (float)0xFF, 0xFF / (float)0xFF, 0x00 / (float)0xFF);
    
    protected Transform m_transform = null;

    public virtual void Activate(bool pickup = false)
    {
        sprite.color = pickup ? m_pickUp : m_defaultColor;
        gameObject.SetActive(true);
    }
    protected virtual void Start()
    {
        m_transform = GetComponent<Transform>();

        m_corner0 = transform.GetChild(1).position;
        m_corner1 = transform.GetChild(0).position;

        gameObject.SetActive(false);
    }
    protected virtual bool Inside(Vector3 position)
    {
        return m_corner0.x > position.x && m_corner1.x < position.x && m_corner0.z > position.z && m_corner1.z < position.z 
            || m_corner0.x < position.x && m_corner1.x > position.x && m_corner0.z > position.z && m_corner1.z < position.z
            || m_corner0.x > position.x && m_corner1.x < position.x && m_corner0.z < position.z && m_corner1.z > position.z
            || m_corner0.x < position.x && m_corner1.x > position.x && m_corner0.z < position.z && m_corner1.z > position.z;
    }
    #if UNITY_EDITOR
    protected virtual void OnDrawGizmos()
    {
        if (m_transform == null) return;

        Gizmos.DrawCube(new Vector3(m_corner0.x, 0, m_corner0.z), transform.localScale * .1f);
        Gizmos.DrawCube(new Vector3(m_corner1.x, 0, m_corner1.z), transform.localScale * .1f);
    }
    #endif
}