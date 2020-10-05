using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryZone : SquareZone
{
    bool m_delivered = true;
    bool m_pickup = false;

    [SerializeField] Transform m_deliveryPoint = null;

    public override void Activate(bool pickup = false)
    {
        DeliveryManager.inst.NewDeliveryPoint();
        m_pickup = pickup;
        m_delivered = false;

        base.Activate(pickup);
    }
    void Update()
    {
        if (!m_delivered && Inside(PlayerPosition.inst.frontWheel))
        {
            Debug.Log("Inside");

            if (m_pickup)
            {
                DeliveryManager.inst.FindDeliveryForPickup();
                SoundManager.inst.PlayPickUpPaper();
                DeliveryManager.inst.DeliveryCompleted();
            }
            else
            {
                SoundManager.inst.PlayFlyPaper();
                DeliveryManager.inst.DeliveryCompleted(m_deliveryPoint.position);
            }
            gameObject.SetActive(false);
        }
    }
}