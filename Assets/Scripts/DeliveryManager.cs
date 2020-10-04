using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : Singleton<DeliveryManager>
{
    int m_numberOfDelivery = 0;
    int m_deliveryCompleted = 0;

    [SerializeField] DeliveryZone[] deliveryZones = null;

    int m_pickupGenerated = 0;

    public void InitADay()
    {
        m_pickupGenerated = 0;

        List<int> indexs_delivery = FindRandIndex(new List<int>(), GameManager.inst.numDeliveryZone);

        List<int> indexs_pickups = FindRandIndex(indexs_delivery, GameManager.inst.numPickupZone);

        foreach (int i in indexs_delivery)
        {
            deliveryZones[i].Activate();
        }

        foreach (int i in indexs_pickups)
        {
            deliveryZones[i].Activate(true);
        }
    }

    public List<int> FindRandIndex(List<int> previous, int need)
    {
        List<int> indexs = new List<int>();
        for (int i = 0; i < need; i++)
        {
            int length = deliveryZones.Length - (indexs.Count + previous.Count);
            int index = Random.Range(0, length);
            while (indexs.Contains(index) || previous.Contains(index) || deliveryZones[index].gameObject.activeSelf)
            {
                index++;
                if (index >= deliveryZones.Length)
                    index = 0;
            }
            indexs.Add(index);
        }
        return indexs;
    }
    public void FindDeliveryForPickup()
    {
        if (++m_pickupGenerated > GameManager.inst.numPickupZone) return;
        List<int> index = FindRandIndex(new List<int>(), 1);
        foreach (int i in index)
        {
            deliveryZones[i].Activate();
        }
    }

    public void NewDeliveryPoint()
    {
        m_numberOfDelivery++;
    }
    public void DeliveryCompleted()
    {
        m_deliveryCompleted++;

        if (m_deliveryCompleted >= m_numberOfDelivery)
        {
            Debug.Log("deliveries competed");

            GameManager.inst.EndDay();
        }
    }
}