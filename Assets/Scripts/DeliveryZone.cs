using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryZone : SquareZone
{
    bool m_delivered = false;
    bool m_pickup = false;

    public override void Activate(bool pickup = false)
    {
        DeliveryManager.inst.NewDeliveryPoint();
        m_pickup = pickup;

        base.Activate(pickup);
    }
    void Update()
    {
        if (!m_delivered && Inside(PlayerPosition.inst.position))
        {
            Debug.Log("Inside");

            if (m_pickup)
            {
                DeliveryManager.inst.FindDeliveryForPickup();
            }

            DeliveryManager.inst.DeliveryCompleted();
            gameObject.SetActive(false);
        }
    }
}