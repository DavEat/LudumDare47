using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryZone : MonoBehaviour
{
    [SerializeField] Vector3 m_corner0 = new Vector3( .32f, 0,  .32f);
    [SerializeField] Vector3 m_corner1 = new Vector3(-.32f, 0, -.32f);

    [SerializeField] SpriteRenderer sprite = null;

    Color m_defaultColor = new Color(0xFF / (float)0xFF, 0xD4 / (float)0xFF, 0x00 / (float)0xFF);
    Color m_pickUp =       new Color(0x68 / (float)0xFF, 0xFF / (float)0xFF, 0x00 / (float)0xFF);

    Transform m_transform = null;

    bool m_delivered = false;
    bool m_pickup = false;

    public void Activate(bool pickup = false)
    {
        m_pickup = pickup;

        sprite.color = m_pickup ? m_pickUp : m_defaultColor;

        DeliveryManager.inst.NewDeliveryPoint();
        gameObject.SetActive(true);
    }
    void Start()
    {
        m_transform = GetComponent<Transform>();

        m_corner0 = new Vector3(m_corner0.x * m_transform.localScale.x + m_transform.position.x, 0, m_corner0.z * m_transform.localScale.z + m_transform.position.z);
        m_corner1 = new Vector3(m_corner1.x * m_transform.localScale.x + m_transform.position.x, 0, m_corner1.z * m_transform.localScale.z + m_transform.position.z);

        gameObject.SetActive(false);
    }
    void Update()
    {
        if (!m_delivered && Inside(PlayerPosition.inst.position))
        {
            Debug.Log("Inside");
            DeliveryManager.inst.DeliveryCompleted();
            gameObject.SetActive(false);

            if (m_pickup)
            {
                DeliveryManager.inst.FindDeliveryForPickup();
            }
        }
    }
    bool Inside(Vector3 position)
    {
        return m_corner0.x > position.x && m_corner1.x < position.x && m_corner0.z > position.z && m_corner1.z < position.z;
    }
    #if UNITY_EDITOR
    void OnDrawGizmos()
    {
        Gizmos.DrawCube(m_corner0, transform.localScale * .1f);
        Gizmos.DrawCube(m_corner1, transform.localScale * .1f);
    }
    #endif
}